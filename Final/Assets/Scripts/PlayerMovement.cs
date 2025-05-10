using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Animation Settings")]
    public Animator animator;

    [Header("Rotation Settings")]
    public float rotationSpeed = 720f;
    public float smoothRotationSpeed = 5f;

    [Header("Physics Settings")]
    [Tooltip("How quickly to bleed off speed when NOT in ball form (units/sec²)")]
    [SerializeField] float decelerationRate = 1f;
    [Tooltip("Impulse added when you curl into ball form")]
    [SerializeField] float ballSpeedBoost = 10f;
    [Tooltip("Gravity scale to use in ball form")]
    [SerializeField] float ballGravityScale = 1f;

    public Rigidbody2D rb;
    private bool isBallForm = false;
    private bool isCurling = false;
    private bool boostApplied = false;
    private Quaternion targetRotation;
    public float CurrentSpeed => rb.velocity.x;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Start out kinematic with zero gravity
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0f;
        Debug.Log("[PlayerMovement] Start: bodyType=Dynamic, gravityScale=0");
    }

    void Update()
    {
        // ——— Handle Curl / Ball Transitions ———
        if (Input.GetKey(KeyCode.Space))
        {
            if (!isCurling)
            {
                animator.SetTrigger("Curl");
                animator.SetBool("IsBall", false);
                isCurling = true;
                boostApplied = false;
                Debug.Log("[PlayerMovement] Started Curling");
            }

            if (isCurling && !isBallForm)
            {
                // switch into ball form
                animator.SetBool("IsBall", true);
                isBallForm = true;
                targetRotation = Quaternion.identity;

                // switch to dynamic physics
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.gravityScale = ballGravityScale;
                Debug.Log($"[PlayerMovement] Entered Ball Form: bodyType=Dynamic, gravityScale={rb.gravityScale}");

                if (!boostApplied)
                {
                    rb.AddForce(transform.right * ballSpeedBoost, ForceMode2D.Impulse);
                    boostApplied = true;
                    Debug.Log($"[PlayerMovement] Applied Ball Speed Boost: {ballSpeedBoost}");
                }
            }
        }
        else
        {
            if (isBallForm)
            {
                animator.SetTrigger("BallToCurl");
                animator.SetBool("IsBall", false);
                isBallForm = false;
                targetRotation = Quaternion.Euler(0f, 0f, 0f);

                // back to kinematic
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.gravityScale = 0f;
                Debug.Log("[PlayerMovement] Exited Ball Form: bodyType=Kinematic, gravityScale=0");
            }
            else if (isCurling)
            {
                animator.SetBool("IsCurling", false);
                animator.SetBool("IsIdle", true);
                isCurling = false;
                Debug.Log("[PlayerMovement] Returned to Idle from Curling");
            }
        }

        // ——— Handle Rotation ———
        if (isBallForm)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothRotationSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        // if we're kinematic (not ball form), bleed off any residual velocity
        if (rb.bodyType == RigidbodyType2D.Kinematic)
        {
            Vector2 v = rb.velocity;
            float newX = Mathf.MoveTowards(v.x, 0f, decelerationRate * Time.fixedDeltaTime);
            rb.velocity = new Vector2(newX, v.y);
            Debug.Log($"[PlayerMovement] Decelerating: velocity.x from {v.x:F2} to {newX:F2}");
        }
        // Dynamic (ball form) physics run naturally
    }
}
