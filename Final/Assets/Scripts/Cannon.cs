using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [Header("Aim Settings")]
    [SerializeField] float offset = 20f;   // art’s default rotation offset
    [SerializeField] float minAngle = -45f;  // clamp downwards
    [SerializeField] float maxAngle = 45f;   // clamp upwards

    [Header("Projectile Settings")]
    [Tooltip("What to launch")]
    [SerializeField] GameObject projectilePrefab;
    [Tooltip("Where on the cannon it should spawn")]
    [SerializeField] Transform barrelEnd;
    [Tooltip("Speed at which it flies (units/sec)")]
    [SerializeField] float launchSpeed = 20f;
    [Tooltip("If true, projectile has no gravity so speed stays constant")]
    [SerializeField] bool zeroGravity = true;

    private bool hasFired = false;  // Flag to check if the cannon has already fired

    private void Start()
    {
        // Initialize flag (not strictly necessary as it's false by default)
        hasFired = false;
    }

    void Update()
    {
        // ——— Aim at mouse ———
        Vector3 mw = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mw.z = transform.position.z;
        Vector3 dir = mw - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float z = Mathf.Clamp(angle - offset, minAngle, maxAngle);
        transform.rotation = Quaternion.Euler(0, 0, z);

        // ——— Fire on left-click ———
        if (Input.GetMouseButtonDown(0) && !hasFired)  // Only allow fire if not already fired
        {
            Fire();
            hasFired = true;  // Set the flag to prevent further firing
        }
    }

    void Fire()
    {
        if (projectilePrefab == null || barrelEnd == null)
        {
            Debug.LogWarning("Assign Projectile Prefab and Barrel End!");
            return;
        }

        // 1) Spawn it at the barrel’s end, with the cannon’s rotation
        GameObject p = Instantiate(projectilePrefab, barrelEnd.position, transform.rotation);

        // 2) Give it a one-time velocity (constant thereafter)
        Rigidbody2D rb = p.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = transform.right * launchSpeed;
            if (zeroGravity) rb.gravityScale = 0f;
            rb.drag = 0f; // no slowdown
        }

        var camFollow = Camera.main.GetComponent<FollowCam>();
        if (camFollow != null)
            camFollow.target = p.transform;
    }
}

