using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class iDamageable : MonoBehaviour
{



    public virtual void DoDamage()
    {

    }

}

public class Player : iDamageable

{
    public int Health = 100;
    

    public float speed = 5;

    public float rotSpeed = 100f;
    public Vector3 moveDirection;
    Rigidbody rb;

    AudioSource swing;
    AudioSource walking;



    public float lineLength = 5f;
    public Color lineColor = Color.red;



    public float moveInput, turnInput;
    public bool slash, hit, death = false;

   
    private bool swingisOnCooldown = false;  

    public float swingTime = 1.5f;
    public float swingSoundTime = .5f;
    public float hitTime = 1f;
    public float deathTime = 5f;
    public float followthroughTime = .05f;
    public float startupswingfollowthroughTime = 1f;

    Attack attack;
    bool canwalk = true;
    public bool canAttack = true;

    // Method to be called when you want to start the cooldown
    public void SwingCooldown()
    {
        if (!swingisOnCooldown)  // If we're not already on cooldown
        {
            StartCoroutine(SwingCooldownCoroutine());
            StartCoroutine(SwingSoundCooldownCoroutine());
            StartCoroutine(SwingHitBoxCoroutine());
        }
    }
    public void StaggerCooldown()
    {

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
        GetComponentInChildren<ModelControl>().transform.localEulerAngles = Vector3.zero;
        swing.Play();


        

    }
    private IEnumerator SwingHitBoxCoroutine()
    {
        yield return new WaitForSeconds(startupswingfollowthroughTime);
        //attack.mr.enabled = true;
        attack.sc.enabled = true;
        yield return new WaitForSeconds(followthroughTime);
        //attack.mr.enabled = false;
        attack.sc.enabled = false;
    }
    private IEnumerator HitCooldownCoroutine()
    {
        canwalk = false;
        yield return new WaitForSeconds(hitTime);
        hit = false;
       
        GetComponentInChildren<ModelControl>().transform.localEulerAngles = Vector3.zero;
        canwalk = true;
        
    }
    private IEnumerator DeathCooldownCoroutine()
    {
        death = true;
        yield return new WaitForSeconds(deathTime);
        death = false;
        //SceneManager.LoadScene("DeathScene");//, LoadSceneMode.Single);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(2,LoadSceneMode.Single);
        
    }
    private void Start()
    {
        attack = GetComponentInChildren<Attack>();
        
        
        swing = GetComponentInChildren<SwingSFX>().GetComponent<AudioSource>(); // GameObject.Find("SwingSFX")
        walking = GetComponentInChildren<WalkSFX>().GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        //rb.freezeRotation = true;
        if (UnityEngine.Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (!death && !hit)
        {
            moveInput = Input.GetAxis("Vertical");
            turnInput = Input.GetAxis("Horizontal");

            if (UnityEngine.Input.GetKeyDown(KeyCode.Space) && canAttack)
            {

                SwingCooldown();

            }
            if (canwalk)
            {
                Vector3 moveDirection = transform.forward * moveInput * speed * Time.deltaTime;
                rb.MovePosition(rb.position + moveDirection);

            }
            if (moveInput != 0 || turnInput != 0)
            {
                walking.volume = .5f;
            }
            else
            {
                walking.volume = 0f;
            }

            //GetComponentInChildren<ModelControl>().transform.localEulerAngles = Vector3.zero;



            float turnAmount = turnInput * rotSpeed * Time.deltaTime;
            Quaternion turnRotation = Quaternion.Euler(0, turnAmount, 0);
            rb.MoveRotation(rb.rotation * turnRotation);



        }




    }
    private void FixedUpdate()
    {
        rb.angularVelocity = Vector3.zero;
        //AmIWalking();
        Debug.DrawLine(transform.position, transform.position + transform.forward * lineLength, lineColor);
        if(Health <= 0)
        {

            StartCoroutine(DeathCooldownCoroutine());

        }
    }
  

    public override void DoDamage()
    {
        hit = true;
        Health -= 10;
        StartCoroutine(HitCooldownCoroutine());

    }
    private void AmIWalking()
    {
        if (moveDirection != Vector3.zero)
        {

            walking.volume = .5f;

        }
        else
        {
            walking.volume = 0;
        }
    }
}
