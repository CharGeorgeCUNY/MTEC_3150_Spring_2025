using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    public GameManager ManageGame;
    public GameObject DialoguePrefab;
    

    bool CanTalk;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CanTalk) {
            if (Input.GetKeyDown(KeyCode.E)) {
                CanTalk = true;
            }

            else {
                CanTalk = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Interaction Begins");
        CanTalk = true;
        bool IsDialogue = (collision.gameObject.name == "Player");
        ManageGame.ShowDialogue(IsDialogue);
        ManageGame.DialogueText.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Interaction Ended");
        CanTalk = false;
        // Destroy(ManageGame.DialogueText);
        ManageGame.DialogueText.gameObject.SetActive(false);
    }

    // private void OnCollisionStay2D(Collision2D collision)
    // {

    // }

    // private void OnTriggerStay2D(Collider2D collision)
    // {
    //     // Debug.Log("Merchant Triggered");
    //     //once on frame, it runs once?
    //     if (Input.GetKeyDown(KeyCode.E)) {
    //         Debug.Log("This is an interaction");

    //         bool IsDialogue = (collision.gameObject.name == "Player");
    //         ManageGame.ShowDialogue(IsDialogue);
    //     }
    // }
}
