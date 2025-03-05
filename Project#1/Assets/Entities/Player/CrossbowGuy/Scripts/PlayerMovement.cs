using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [HideInInspector] public Vector2 movement;
    [HideInInspector] public Vector2 lastMovement;
    [HideInInspector] public Direction currentDirection = Direction.Down;
    Animator animator;

    bool facingLeft = true;
    public CharacterScriptableObject characterData;


    void Start()
    {
        if (lastMovement == null) lastMovement = Vector2.right;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        ProcessInputs();
        Animate();

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * characterData.MoveSpeed * Time.fixedDeltaTime);
    }

    void ProcessInputs()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if ((h == 0 && v == 0) && (movement.x != 0 || movement.y != 0))
        {
            lastMovement = movement;
        }

        movement = new Vector2(h, v).normalized;
    }

    void Animate()
    {
        if (movement.magnitude > 0.01f)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.magnitude);
            lastMovement = movement;
        }
        else
        {
            animator.SetFloat("Horizontal", lastMovement.x);
            animator.SetFloat("Vertical", lastMovement.y);
            animator.SetFloat("Speed", 0f);
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1f;
        transform.localScale = scale;
        facingLeft = !facingLeft;
    }
}