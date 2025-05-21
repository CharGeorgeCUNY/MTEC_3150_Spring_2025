using System.Collections;
using Mediapipe.Tasks.Vision.FaceLandmarker;
using Mediapipe.Tasks.Components.Containers; // for Classifications
using UnityEngine;
using UnityEngine.Rendering;

namespace Mediapipe.Unity.Sample.FaceLandmarkDetection
{
    public class FaceLandmarkerRunner : VisionTaskApiRunner<FaceLandmarker>
    {
        [Header("Annotation (optional)")]
        [SerializeField]
        private FaceLandmarkerResultAnnotationController _annotationController;

        [Header("Thresholds (0–1)")]
        [SerializeField, Range(0f, 1f)]
        private float eyeBlinkLeftThreshold = 0.5f;
        [SerializeField, Range(0f, 1f)]
        private float jawOpenThreshold = 0.5f;

        [Header("Avatar Controller")]
        [SerializeField]
        private AvatarBlendshapeController avatarController;

        // State & pending‐event flags
        private bool _prevMouthOpen = false;
        private bool _prevEyeClosed = false;
        private bool _pendingMouthOpen = false;
        private bool _pendingMouthClose = false;
        private bool _pendingEyeClose = false;
        private bool _pendingEyeOpen = false;
        private readonly object _mouthLock = new object();
        private readonly object _eyeLock = new object();

        private Experimental.TextureFramePool _textureFramePool;
        public readonly FaceLandmarkDetectionConfig config = new FaceLandmarkDetectionConfig();

        public override void Stop()
        {
            base.Stop();
            _textureFramePool?.Dispose();
            _textureFramePool = null;
        }

        // Main‐thread: dispatch queued events
        void Update()
        {
            bool doOpen = false, doClose = false;
            lock (_mouthLock)
            {
                doOpen = _pendingMouthOpen;
                doClose = _pendingMouthClose;
                _pendingMouthOpen = _pendingMouthClose = false;
            }
            if (doOpen) avatarController?.TriggerMouthOpen();
            if (doClose) avatarController?.TriggerMouthClose();

            bool doEyeC = false, doEyeO = false;
            lock (_eyeLock)
            {
                doEyeC = _pendingEyeClose;
                doEyeO = _pendingEyeOpen;
                _pendingEyeClose = _pendingEyeOpen = false;
            }
            if (doEyeC) avatarController?.TriggerEyeClosed();
            if (doEyeO) avatarController?.TriggerEyeOpen();
        }

        protected override IEnumerator Run()
        {
            Debug.Log($"Delegate = {config.Delegate}");
            Debug.Log($"Image Read Mode = {config.ImageReadMode}");
            Debug.Log($"Running Mode = {config.RunningMode}");
            Debug.Log($"NumFaces = {config.NumFaces}");
            Debug.Log($"MinFaceDetectionConfidence = {config.MinFaceDetectionConfidence}");
            Debug.Log($"MinFacePresenceConfidence = {config.MinFacePresenceConfidence}");
            Debug.Log($"MinTrackingConfidence = {config.MinTrackingConfidence}");
            Debug.Log($"OutputFaceBlendshapes = {config.OutputFaceBlendshapes}");
            Debug.Log($"OutputFacialTransformationMatrixes = {config.OutputFacialTransformationMatrixes}");

            yield return AssetLoader.PrepareAssetAsync(config.ModelPath);

            var options = config.GetFaceLandmarkerOptions(
              config.RunningMode == Tasks.Vision.Core.RunningMode.LIVE_STREAM
                ? OnLandmarksOutput
                : null
            );
            taskApi = FaceLandmarker.CreateFromOptions(options, GpuManager.GpuResources);
            var imageSource = ImageSourceProvider.ImageSource;

            yield return imageSource.Play();
            if (!imageSource.isPrepared)
            {
                Debug.LogError("Failed to start ImageSource, exiting...");
                yield break;
            }

            _textureFramePool = new Experimental.TextureFramePool(
              imageSource.textureWidth,
              imageSource.textureHeight,
              TextureFormat.RGBA32,
              10
            );

            screen.Initialize(imageSource);
            if (_annotationController != null)
                SetupAnnotationController(_annotationController, imageSource);

            var tfOpts = imageSource.GetTransformationOptions();
            bool flipH = tfOpts.flipHorizontally, flipV = tfOpts.flipVertically;
            var imgOpts = new Tasks.Vision.Core.ImageProcessingOptions(
              rotationDegrees: (int)tfOpts.rotationAngle
            );

            AsyncGPUReadbackRequest req = default;
            var waitReq = new WaitUntil(() => req.done);
            var waitFrame = new WaitForEndOfFrame();
            var result = FaceLandmarkerResult.Alloc(config.NumFaces);

            bool canGpu = SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES3
                          && GpuManager.GpuResources != null;
            using var glCtx = canGpu ? GpuManager.GetGlContext() : null;

            while (true)
            {
                if (isPaused)
                    yield return new WaitWhile(() => isPaused);

                if (!_textureFramePool.TryGetTextureFrame(out var frame))
                {
                    yield return null;
                    continue;
                }

                Image img;
                switch (config.ImageReadMode)
                {
                    case ImageReadMode.GPU:
                        if (!canGpu)
                            throw new System.Exception("ImageReadMode.GPU is not supported");
                        frame.ReadTextureOnGPU(imageSource.GetCurrentTexture(), flipH, flipV);
                        img = frame.BuildGPUImage(glCtx);
                        yield return waitFrame;
                        break;

                    case ImageReadMode.CPU:
                        yield return waitFrame;
                        frame.ReadTextureOnCPU(imageSource.GetCurrentTexture(), flipH, flipV);
                        img = frame.BuildCPUImage();
                        frame.Release();
                        break;

                    default: // CPUAsync
                        req = frame.ReadTextureAsync(imageSource.GetCurrentTexture(), flipH, flipV);
                        yield return waitReq;
                        if (req.hasError)
                        {
                            Debug.LogWarning("Failed to read texture from the image source");
                            continue;
                        }
                        img = frame.BuildCPUImage();
                        frame.Release();
                        break;
                }

                switch (taskApi.runningMode)
                {
                    case Tasks.Vision.Core.RunningMode.IMAGE:
                        if (taskApi.TryDetect(img, imgOpts, ref result))
                            _annotationController?.DrawNow(result);
                        else
                            _annotationController?.DrawNow(default);
                        break;

                    case Tasks.Vision.Core.RunningMode.VIDEO:
                        if (taskApi.TryDetectForVideo(
                              img, GetCurrentTimestampMillisec(), imgOpts, ref result
                            ))
                            _annotationController?.DrawNow(result);
                        else
                            _annotationController?.DrawNow(default);
                        break;

                    default: // LIVE_STREAM
                        taskApi.DetectAsync(img, GetCurrentTimestampMillisec(), imgOpts);
                        break;
                }
            }
        }

        // May run on a worker thread—only queue events here!
        private void OnLandmarksOutput(
          FaceLandmarkerResult result, Image image, long timestamp
        )
        {
            var bs = result.faceBlendshapes;
            if (bs != null && bs.Count > 0)
            {
                float jaw = 0f, eye = 0f;
                foreach (var c in bs[0].categories)
                {
                    if (c.categoryName == "jawOpen") jaw = c.score;
                    else if (c.categoryName == "eyeBlinkLeft") eye = c.score;
                }

                bool mouthOpen = jaw > jawOpenThreshold;
                bool eyeClosed = eye > eyeBlinkLeftThreshold;

                lock (_mouthLock)
                {
                    if (mouthOpen && !_prevMouthOpen) _pendingMouthOpen = true;
                    if (!mouthOpen && _prevMouthOpen) _pendingMouthClose = true;
                }
                _prevMouthOpen = mouthOpen;

                lock (_eyeLock)
                {
                    if (eyeClosed && !_prevEyeClosed) _pendingEyeClose = true;
                    if (!eyeClosed && _prevEyeClosed) _pendingEyeOpen = true;
                }
                _prevEyeClosed = eyeClosed;
            }

            _annotationController?.DrawLater(result);
        }
    }
}
