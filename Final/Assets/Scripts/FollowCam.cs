using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [Tooltip("The player (or object) the camera should follow")]
    public Transform target;

    [Tooltip("Offset from the target's position")]
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    [Tooltip("Higher = snappier follow. 1 = instant, smaller = more laggy")]
    [Range(0f, 1f)]
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        // 1) If we don’t have a target yet, bail out:
        if (target == null) return;
        // 2) Compute desired pos:
        Vector3 desired = target.position + offset;
        // 3) Smooth (or remove Lerp for instant follow):
        Vector3 smoothed = Vector3.Lerp(transform.position, desired, smoothSpeed);
        // 4) Apply, preserving camera’s Z:
        transform.position = new Vector3(smoothed.x, smoothed.y, transform.position.z);
    }

    // Optional: try to find the player once if target is still null
    void Update()
    {
        if (target == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                target = p.transform;
        }
    }
}
