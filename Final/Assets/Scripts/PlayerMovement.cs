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

    [Tooltip("Flat speed added when you curl into ball form")]
    [SerializeField] float flatRollSpeedBoost = 10f;

    [Tooltip("Additional speed added per second while rolling")]
    [SerializeField] float rollAcceleration = 2f;

    [Tooltip("Gravity scale to use in ball form")]
    [SerializeField] float ballGravityScale = 1f;

    [Tooltip("Vertical speed when in ball form")]
    [SerializeField] float rollVerticalSpeed = 5f;

    public GameManager gm;
    public Rigidbody2D rb;
    private bool isBallForm = false;
    private bool isCurling = false;
    private bool boostApplied = false;
    private Quaternion targetRotation;

    // tracks W/S input during ball form
    private float verticalInput;

    public float CurrentSpeedX => rb.velocity.x;
    public float CurrentSpeedY => rb.velocity.y;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0f;
    }

    void Update()
    {
        // ——— Curl / Ball transitions ———
        if (Input.GetKey(KeyCode.Space))
        {
            if (!isCurling)
            {
                animator.SetTrigger("Curl");
                animator.SetBool("IsBall", false);
                isCurling = true;
                boostApplied = false;
            }

            if (isCurling && !isBallForm)
            {
                // Enter ball form
                animator.SetBool("IsBall", true);
                isBallForm = true;
                targetRotation = Quaternion.identity;
                rb.gravityScale = ballGravityScale;

                if (!boostApplied)
                {
                    rb.velocity = new Vector2(rb.velocity.x + flatRollSpeedBoost, -rb.velocity.y);
                    boostApplied = true;
                }
            }
        }
        else
        {
            if (isBallForm)
            {
                // Exit ball form
                animator.SetTrigger("BallToCurl");
                animator.SetBool("IsBall", false);
                isBallForm = false;
                targetRotation = Quaternion.identity;
                rb.gravityScale = 0f;
                rb.velocity = new Vector2(rb.velocity.x - flatRollSpeedBoost, rb.velocity.y);
            }
            else if (isCurling)
            {
                animator.SetBool("IsCurling", false);
                animator.SetBool("IsIdle", true);
                isCurling = false;
            }
        }

        // ——— Rotation ———
        if (isBallForm)
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        else
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothRotationSpeed * Time.deltaTime);

        // ——— Vertical input when rolling ———
        if (isBallForm)
        {
            if (Input.GetKey(KeyCode.W)) verticalInput = 5f;
            else if (Input.GetKey(KeyCode.S)) verticalInput = -5f;
            else verticalInput = 0f;
        }
    }

    void FixedUpdate()
    {
        if (isBallForm)
        {
            // accelerate and override vertical movement
            Vector2 v = rb.velocity;
            v.x += rollAcceleration * Time.fixedDeltaTime;
            v.y = verticalInput * rollVerticalSpeed;
            rb.velocity = v;
        }
        else
        {
            // bleed off residual speed normally
            if (rb.bodyType == RigidbodyType2D.Dynamic)
            {
                Vector2 v = rb.velocity;
                float newX = Mathf.MoveTowards(v.x, 0f, decelerationRate * Time.fixedDeltaTime);
                rb.velocity = new Vector2(newX, v.y);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
