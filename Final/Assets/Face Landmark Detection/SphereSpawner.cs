// SphereSpawner.cs
using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    [Header("Reference your avatar’s blendshape controller here")]
    [SerializeField]
    private AvatarBlendshapeController avatarController;

    [Header("Sphere Prefab & Spawn Point")]
    [SerializeField] private GameObject spherePrefab;
    [SerializeField] private Transform spawnPoint;

    [Header("Optional Material & Timing")]
    [SerializeField] private Material sphereMaterial;
    [SerializeField] private float spawnInterval = 1f;

    [Header("How wide open before we fire?")]
    [SerializeField] private float jawOpenThreshold = 10f;

    bool isSpawning;

    void Update()
    {
        if (avatarController == null) return;

        float jawW = avatarController.GetBlendShapeWeight("jawOpen");

        if (jawW > jawOpenThreshold && !isSpawning)
        {
            isSpawning = true;
            InvokeRepeating(nameof(SpawnSphere), 0f, spawnInterval);
            Debug.Log("Mouth opened – start spawning");
        }
        else if (jawW <= jawOpenThreshold && isSpawning)
        {
            isSpawning = false;
            CancelInvoke(nameof(SpawnSphere));
            Debug.Log("Mouth closed – stop spawning");
        }
    }

    void SpawnSphere()
    {
        var s = Instantiate(
          spherePrefab,
          spawnPoint.position,
          Quaternion.identity
        );
        s.transform.localScale = Vector3.one * 0.2f;
        if (sphereMaterial != null)
            s.GetComponent<Renderer>().material = sphereMaterial;
        Debug.Log("Spawned a sphere at " + spawnPoint.position);
    }
}
