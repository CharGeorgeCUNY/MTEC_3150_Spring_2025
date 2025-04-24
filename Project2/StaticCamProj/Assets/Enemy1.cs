using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : iDamageable
{
    public float rayDistance = 10f; // How far the rays will shoot
    public float rotationSpeed = 5f; // How fast the object rotates to face the player
    public float moveSpeed = 2f; // Speed at which it moves toward the player
    public string playerTag = "Player"; // Tag for the player
    public float stopDistance = 1f; // The distance at which the enemy stops moving

    public int moveInput, turnInput;
    public int Health = 20;

    public bool slash, hit, death = false;


    private bool swingisOnCooldown = false;

    public float swingTime = 1.5f;
    public float swingSoundTime = .5f;
    public float hitTime = 2f;
    public float deathTime = 5f;
    public float followthroughTime = .05f;
    public float startupswingfollowthroughTime = 1f;


    bool canwalk = true;

    
    GameObject objectA; 
    GameObject objectB;
    public GameObject hitFX;


    SwingSFX swing;
    WalkSFX walking;
    SkeleControl skele;
    Attack attack;
    Rigidbody rb;

    public void SwingCooldown()
    {
        if (!swingisOnCooldown && !death && !hit)  // If we're not already on cooldown
        {
            StartCoroutine(SwingCooldownCoroutine());
            StartCoroutine(SwingSoundCooldownCoroutine());
            StartCoroutine(SwingHitBoxCoroutine());
        }
    }
    private IEnumerator SwingCooldownCoroutine()
    {
        swingisOnCooldown = true;  // Start cooldown

        slash = true;
        canwalk = false;

        // Wait for the cooldown time to pass
        yield return new WaitForSeconds(swingTime);

        slash = false;
        canwalk = true;
        swingisOnCooldown = false;  // End cooldown

    }
    private IEnumerator SwingSoundCooldownCoroutine()
    {


        // Wait for the cooldown time to pass
        yield return new WaitForSeconds(swingSoundTime);

        swing.auso.Play();




    }
    private IEnumerator SwingHitBoxCoroutine()
    {
        yield return new WaitForSeconds(startupswingfollowthroughTime);
        //attack.mr.enabled = true;
        if (!death && !hit)
            attack.sc.enabled = true;
        yield return new WaitForSeconds(followthroughTime);
        //attack.mr.enabled = false;
        attack.sc.enabled = false;
    }
    private void Start()
    {
        attack = GetComponentInChildren<Attack>();
        swing = GetComponentInChildren<SwingSFX>(); // GameObject.Find("SwingSFX")
        walking = GetComponentInChildren<WalkSFX>();
        rb = GetComponent<Rigidbody>();
        skele = GetComponentInChildren<SkeleControl>();
    }
    void Update()
    {
        
        
    }
    private IEnumerator HitCooldownCoroutine()
    {
        canwalk = false;
        yield return new WaitForSeconds(hitTime);
        canwalk = true;
        hit = false;
    }
    private IEnumerator DeathCooldownCoroutine()
    {
        death = true;
       
        yield return new WaitForSeconds(deathTime);
        GameObject.Find("Game Manager").GetComponent<GameManager>().wrathfulSlain++;
        Debug.Log("Im DEAD");

        //direction = GetComponentInParent<Enemy1>().moveInput;

        Destroy(this.gameObject);

    }
    private void FixedUpdate()
    {
        rb.angularVelocity = Vector3.zero;
        // Debug.DrawLine(transform.position, transform.position + transform.forward * lineLength, lineColor);
        if (Health <= 0)
        {
            Collider colliderA = GameObject.Find("Player").GetComponent<Collider>();
            Collider colliderB = this.GetComponent<Collider>();
            Physics.IgnoreCollision(colliderB, colliderA, true);
            StartCoroutine(DeathCooldownCoroutine());
            

        }
        else if (!death && !hit)
        {
            AmIWalking(false);
            // Loop through 360 degrees
            for (float angle = 0f; angle < 360f; angle += 10f)
            {
                // Convert angle to radians for Vector3 direction
                Vector3 direction = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0f, Mathf.Cos(angle * Mathf.Deg2Rad));

                // Shoot the ray
                RaycastHit hit;
                if (Physics.Raycast(transform.position, direction, out hit, rayDistance))
                {

                    // If the ray hits something with the player tag
                    if (hit.collider.CompareTag(playerTag))
                    {
                        Vector3 directionToPlayer = hit.collider.transform.position - transform.position;
                        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
                        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

                        // Calculate the distance to the player
                        float distanceToPlayer = Vector3.Distance(transform.position, hit.collider.transform.position);

                        // If the distance is greater than the stop distance, rotate and move towards the player
                        if (distanceToPlayer > stopDistance && canwalk)
                        {
                            AmIWalking(true);
                            // Rotate to face the player
                            
                            // Move towards the player slowly
                            transform.position = Vector3.MoveTowards(transform.position, hit.collider.transform.position, moveSpeed * Time.deltaTime);
                        }
                        else if(!swingisOnCooldown)
                        {
                            AmIWalking(false);
                            
                            SwingCooldown();
                            
                        }
                    }
                }
            }

        }
    }


    public override void DoDamage()
    {
        Instantiate(hitFX, this.transform.position, Quaternion.identity);
        hit = true;
        Health -= 10;
        StartCoroutine(HitCooldownCoroutine());

    }

    private void AmIWalking(bool t)
    {
        if (t)
        {
            moveInput = 1;
            walking.auso.volume = 1f;

        }
        else
        {
            moveInput = 0;
            walking.auso.volume = 0;
        }
    }
}
