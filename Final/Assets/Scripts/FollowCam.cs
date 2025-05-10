using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    [Range(0f, 1f)] public float smoothSpeed = 0.125f;
    public ScrollingBackground scrollingBackground;

    void Awake()
    {
        // optional: auto-assign if you forgot in the Inspector
        if (scrollingBackground == null)
            scrollingBackground = GetComponent<ScrollingBackground>();
    }

    void Start()
    {
        // only disable if we truly have no target yet
        if (target == null && scrollingBackground != null)
            scrollingBackground.enabled = false;
    }

    void Update()
    {
        // try to find the player once
        if (target == null)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                target = p.transform;
        }

        // enable the scrolling as soon as we have a target
        if (target != null && scrollingBackground != null
            && !scrollingBackground.enabled)
        {
            scrollingBackground.enabled = true;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;
        Vector3 desired = target.position + offset;
        Vector3 smoothed = Vector3.Lerp(transform.position, desired, smoothSpeed);
        transform.position = new Vector3(smoothed.x, smoothed.y, transform.position.z);
    }
}
