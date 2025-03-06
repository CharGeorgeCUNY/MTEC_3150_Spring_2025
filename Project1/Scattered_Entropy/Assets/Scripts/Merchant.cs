using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    public GameManager ManageGame;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == true) {
            bool IsDialogue = (collision.collider);
            ManageGame.StartDialogue();
            ManageGame.DialogueText.gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ManageGame.DialogueText.gameObject.SetActive(false);
    }
}
