using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace PhysicsSym
{
    public class CameraScript : MonoBehaviour
    {
        public static Camera mainCamera;
        public static CinemachineCamera followCamera;
        public static CinemachineCamera staticCamera;

        public static CameraScript cameraScript;

        public bool hold = false;


        //camera script singleton
        public static CameraScript GetCameraScript()
        {
            if (cameraScript == null)
            {
                cameraScript = GameObject.FindFirstObjectByType<CameraScript>();
            }
            return cameraScript;
        }

        void Start()
        {
            mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            followCamera = GameObject.Find("Follow Camera").GetComponent<CinemachineCamera>();
            staticCamera = GameObject.Find("Death Camera").GetComponent<CinemachineCamera>();
            
            cameraScript = this;

            followCamera.enabled = true;
            staticCamera.enabled = false;

            hold = false;

            PlayerEvents.currentPlayer.onDeathEvent += OnDeathEvent;
        }

        public void OnDeathEvent()
        {
            Vector3 cameraPos = followCamera.gameObject.transform.position;

            if (staticCamera != null)
            {
                staticCamera.gameObject.transform.position = cameraPos;
                SwitchCameras();
            }

            StartCoroutine("ZoomIn");
            StartCoroutine("DelayedReset");
        }

        public void ResetCamera()
        {
            hold = false;
            SwitchCameras();
            staticCamera.Lens.OrthographicSize = 15;
        }

        private IEnumerator DelayedReset()
        {
            yield return new WaitForSecondsRealtime(GameManager.deathTime);
            ResetCamera();
        }

        private IEnumerator ZoomIn()
        {
            yield return null;
            staticCamera.Lens.OrthographicSize = Mathf.Lerp(15, 5, GameManager.deathTime);
            staticCamera.Lens.OrthographicSize = 15;
        }

        public static void SwitchCameras()
        {
            if (mainCamera.GetComponent<CameraScript>().hold == false)
            {
                if (followCamera.enabled == true && staticCamera.enabled == false)
                {
                    followCamera.enabled = false;
                    // Debug.Log(followCamera.enabled);
                    staticCamera.enabled = true;
                    // Debug.Log(staticCamera.enabled);
                }

                else if (followCamera.enabled == false && staticCamera.enabled == true)
                {
                    followCamera.enabled = true;
                    staticCamera.enabled = false;
                }
            }

        }
    }
}

