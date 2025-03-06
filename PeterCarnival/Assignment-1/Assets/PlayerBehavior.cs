using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float speed;
    public float jump;
    public bool isGrounded;
    public float xEdgeOfScreen, yEdgeOfScreen, highestPoint;
    public int facingLeft;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<SpriteRenderer>().flipX = true;
            transform.Translate(Vector2.right*Time.deltaTime*speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<SpriteRenderer>().flipX = false;
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
        
        
            
        if (rb2D.velocity.y > 0)
        {
            Physics2D.IgnoreLayerCollision(3, 6, true);
            Debug.Log("Ping");
        }
        else
        {
            Physics2D.IgnoreLayerCollision(3, 6, false);
            Debug.Log("Pong");
        }

        if (transform.position.x < -xEdgeOfScreen)
        {
            transform.position = new Vector2(xEdgeOfScreen * 0.9f, transform.position.y);
        }
        else if (transform.position.x > xEdgeOfScreen)
        {
            transform.position = new Vector2(-xEdgeOfScreen * 0.9f, transform.position.y);
        }
        else if (transform.position.y < yEdgeOfScreen)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (transform.position.y > highestPoint)
        {
            highestPoint = transform.position.y;
            yEdgeOfScreen = highestPoint - 5;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb2D.AddForce(Vector2.up * jump);

        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
