using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player Targeted");

        // assigns the bool of the health function 
        // when the game object called "Enemy" collides
        bool IsHealth = (collision.gameObject.name == "Player");
        GameObject.FindObjectOfType<GameManager>().IncrementHealth(IsHealth);
    }
}
