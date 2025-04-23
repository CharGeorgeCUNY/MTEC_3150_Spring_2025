using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{

    //playerControls
    Rigidbody rb;
    Vector3 move;

    float speed = 10.0f;

    public Transform[] changeLane;
    int arrayIndex = 1;

    //jumpProgramming removed temporarily
    //public LayerMask LayersToCheck;
    //bool isGrounded = false;
    //public float jumpHeight = 10.0f;

    //damage and death
    public Material colorChange;

    bool canTakeDamage = true;
    int life = 3;
    public GameObject[] healthBar;

    //hit Audio
    AudioSource playerAudio;

    //gameOver & sceneChangeSequence
    public GameObject gameOverText;
    public GameObject theEndText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        move = Vector3.forward;
        colorChange.color = Color.white;
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //move = Vector3.forward;
        rb.velocity = move * speed;

        //jumpProgramming removed
        //isGrounded = Physics.Raycast(transform.position, Vector3.down, GetComponent<CapsuleCollider>().height / 1.9f, LayersToCheck);
        //Debug.Log("isGrounded = ");
        //Debug.Log(isGrounded);

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Debug.Log("RightArrow");
            arrayIndex += 1;
            arrayIndex = Mathf.Clamp(arrayIndex, 0, 2);
            //Debug.Log(arrayIndex);

            Vector3 moveLeft = new Vector3(changeLane[arrayIndex].position.x, 0, transform.position.z);
            rb.MovePosition(moveLeft);

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Debug.Log("LeftArrow");
            arrayIndex -= 1;
            arrayIndex = Mathf.Clamp(arrayIndex, 0, 2);
            //Debug.Log(arrayIndex);


            Vector3 moveRight = new Vector3(changeLane[arrayIndex].position.x, 0, transform.position.z);
            rb.MovePosition(moveRight);
        }

        //jumpProgramming removed temporarily
        //Debug.Log("Space = ");
        //Debug.Log(Input.GetKeyDown(KeyCode.Space));

        //if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        //{
        //    rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
        //    //if(isGrounded && rb.velocity.y <= 0.0f)
        //    //{
        //    //    rb.velocity = new Vector3(rb.velocity.x,jumpHeight, rb.velocity.z);
        //    //}
        //}

        if( life == 0 )
        {
            canTakeDamage = false;
        }
        if (!canTakeDamage)
        {
            StartCoroutine(gameOver());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "hazard") && canTakeDamage)
        {
            //Debug.Log("hit hazard");
            //Debug.Log(life);
            playerAudio.Play();
            life -= 1;
            healthBar[life].SetActive(false);
            StartCoroutine(BlinkRed());
            
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(theEnd());
    }

    public IEnumerator BlinkRed()
    {
        WaitForSeconds delay = new WaitForSeconds(0.05f);

        colorChange.color = Color.red;
        yield return delay;
        colorChange.color = Color.white;
        yield return delay;
        colorChange.color = Color.red;
        yield return delay;
        colorChange.color = Color.white;
        yield return delay;
        colorChange.color = Color.red;
        yield return delay;
        colorChange.color = Color.white;
        yield return delay;
       
    }

    public IEnumerator gameOver()
    {

        WaitForSeconds delay = new WaitForSeconds(1.3f);
        gameOverText.SetActive(true);
        yield return delay;
        changeScene();

    }

    public IEnumerator theEnd()
    {

        WaitForSeconds delay = new WaitForSeconds(0.8f);
        theEndText.SetActive(true);
        yield return delay;
        changeScene();

    }


    void changeScene()
    {
        SceneManager.LoadScene("mainMenu");
    }

}

