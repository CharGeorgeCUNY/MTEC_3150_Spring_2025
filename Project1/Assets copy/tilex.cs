using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilex : MonoBehaviour
{
    public GameBounds Game;
    private void Start()
    {
        Game = GetComponentInParent<GameBounds>();
        //this.transform.parent = null;

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("IM IN");
            Destroy(this.gameObject);
            Game.NewGame();
        }
    }
}
