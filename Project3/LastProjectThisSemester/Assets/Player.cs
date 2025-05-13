using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{

    bool loadsound = true;
   [SerializeField] Sprite neutral, jumping;

    AudioSource Auso;
    Rigidbody2D rb;
    Transform planet;
    public bool LevelComplete;

    public int JumpForce = 150;
    public float playerGrav = 3;
    Transform CurrentGround;
    public GameObject sizz;


    public int movespeed;
    GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponentInParent<GameManager>();
        Auso = GetComponent<AudioSource>();
        LevelComplete = false;
         rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {








        if (this.transform.parent == GetComponentInParent<GameManager>().transform) planet = GameObject.Find("Planet(Clone)").GetComponent<Transform>();
        else planet = GetComponentInParent<Planet>().transform;
    
        

       



        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Space) && grounded())
        {
            rb.AddForce(transform.TransformDirection(Vector2.up) * JumpForce);
        }
        if (grounded())
        {
            if (loadsound) { Auso.pitch = Random.Range(.7f, 1.3f); Auso.Play(); loadsound = false; }
            this.GetComponent<SpriteRenderer>().sprite = neutral;
            this.transform.parent = CurrentGround;
           
        }
        else
        {
            loadsound = true;
            this.GetComponent<SpriteRenderer>().sprite = jumping;
            this.transform.parent = GetComponentInParent<GameManager>().transform;
        }
        //Debug.Log(grounded());
        //float horizontalInput = Input.GetAxis("Horizontal");
        //transform.position += new Vector3(horizontalInput * movespeed * Time.deltaTime, 0f, 0f);
    }
    private void FixedUpdate()
    {
        

        if (GetComponentInParent<GameManager>().killplayerok) Destroy(this.gameObject);
       
        if(rb.angularVelocity != 0) Debug.Log(rb.angularVelocity);
        FollowTargetWitouthRotation(planet, 1, playerGrav);
        if (this.transform.parent != GetComponentInParent<GameManager>().transform) this.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
    private bool grounded()
    {
        LayerMask mask = LayerMask.GetMask("Ground");

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            transform.TransformDirection(Vector2.down),
            2f,
            mask
        );

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down) * 3, Color.blue);

        if (hit.collider != null)
        {
            CurrentGround = hit.collider.transform.parent;
            return true;
        }
        else
        {
            return false;
        }
    }
    void FollowTargetWitouthRotation(Transform target, float distanceToStop, float speed)
    {
        var direction = Vector3.zero;
        if (Vector2.Distance(transform.position, target.position) > distanceToStop)
        {
            direction = target.position - transform.position;

            Vector3 localDirection = transform.InverseTransformDirection(direction.normalized);
            rb.AddRelativeForce(localDirection * speed, ForceMode2D.Force);
            //this.rb.AddRelativeForce(direction.normalized * speed, ForceMode2D.Force);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Planet")
        {
            gm.playerdied = true;
            Instantiate(sizz, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if(collision.tag == "Rocket")
        {
           
            LevelComplete = true;
           
           
        }
    }


}
