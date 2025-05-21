// AvatarBlendshapeController.cs
using System.Collections.Generic;
using UnityEngine;
using Mediapipe.Tasks.Components.Containers;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;

public class AvatarBlendshapeController : MonoBehaviour
{
    [SerializeField] private string secondLevel = "scene2";
    [Header("Drag your head.geo.002 here")]
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

    private Dictionary<string, int> _map;

    [Header("CineMachine Cameras")]
    public CinemachineCamera cam1;
    public CinemachineCamera cam2;

    void Awake()
    {
        if (skinnedMeshRenderer == null)
            skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        if (skinnedMeshRenderer == null)
        {
            Debug.LogError(
              "AvatarBlendshapeController couldn't find a SkinnedMeshRenderer!"
            );
            enabled = false;
            return;
        }
        var mesh = skinnedMeshRenderer.sharedMesh;
        _map = new Dictionary<string, int>(mesh.blendShapeCount);
        for (int i = 0; i < mesh.blendShapeCount; i++)
            _map[mesh.GetBlendShapeName(i)] = i;
    }

    public void TriggerMouthOpen()
    {
        Set("jawOpen", 100f);
        Set("mouthSmileLeft", 50f);
        Set("eyeLookUpLeft", 50f);
        Set("mouthPucker", 0f);
        cam1.Priority = 1; cam2.Priority = 0;
    }

    public void TriggerMouthClose()
    {
        Set("jawOpen", 0f);
        Set("mouthSmileLeft", 0f);
        Set("eyeLookUpLeft", 0f);
        Set("mouthSmileRight", 0f);
        Set("mouthPucker", 100f);
        Set("tongueOut", 100f);
        cam1.Priority = 0; cam2.Priority = 1;
    }

    public void TriggerEyeClosed() { Set("eyeBlinkLeft", 100f); }
    public void TriggerEyeOpen()
    {
        Set("eyeBlinkLeft", 0f);
        SceneManager.LoadScene(secondLevel);
    }

    public void Set(string name, float weight)
    {
        if (_map.TryGetValue(name, out var idx))
            skinnedMeshRenderer.SetBlendShapeWeight(idx, weight);
        else
            Debug.LogWarning($"Blendshape '{name}' not found");
    }

    // ← NEW: read back any blendshape
    public float GetBlendShapeWeight(string name)
    {
        if (_map != null && _map.TryGetValue(name, out var idx))
            return skinnedMeshRenderer.GetBlendShapeWeight(idx);
        return 0f;
    }
}
