using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Platform : MonoBehaviour
{
    //platform controls
    public Rigidbody2D rbPlatform;

    //platform variables
    public float platSpeed;

    //player Controls
    bool playerContact;
    playerMovement player;
    bool hitCollider;
   

    // Start is called before the first frame update
    void Start()
    {
        rbPlatform = GetComponent<Rigidbody2D>();
        playerContact = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.hit.collider.gameObject.tag == "fallingPlatform")
        {
            Debug.Log("Hit object: " + player.hit.collider.gameObject.tag);
            hitCollider = true;
        }

        if (playerContact && (gameObject.tag == "fallingPlatform"))
        {

            Debug.Log("PlayerContact");
            rbPlatform.velocity = new Vector2(0, -1 * platSpeed);
        }
        else if(playerContact && (gameObject.tag == "risingPlatform"))
        {
            Debug.Log("PlayerContact");
            rbPlatform.velocity = new Vector2(0, 1 * platSpeed);
        }
        else
        {
            Debug.Log("NoPlayerContact");
            rbPlatform.velocity = Vector2.zero;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //detect player
        if (collision.gameObject.CompareTag("Player") && hitCollider)
        {
            playerContact = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")`)
        {
            playerContact = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("harzard"))
        {
            Debug.Log("KilledPlatform");
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerContact = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity += rbPlatform.velocity;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerContact = false;
        }
    }
}
