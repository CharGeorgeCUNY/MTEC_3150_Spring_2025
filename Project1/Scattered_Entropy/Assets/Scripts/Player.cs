using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 3.0f;
    public Vector2 Velocity;
    public Rigidbody2D rigidBody2D;

    public GameObject EnemySpawn;

    public List<Enemies> Enemies = new List<Enemies>();

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // the velocity is initialized to zero and changes from -1 to 1 
        // depending on the input called from the axis manager (inputs uses vectors)
        Velocity = Vector2.zero;
        Velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //normalizes the velocity's speed to -1 or 1
        Velocity.Normalize();
        
        Velocity *= Speed;
        rigidBody2D.velocity = Velocity;

        if(Input.GetKeyDown(KeyCode.Y))
        {
            Vector2 randPos = new Vector2(Random.Range(1.0f, 5.0f), Random.Range(1.0f, 3.0f));

            //Qu don't change default
            GameObject myEnemy = GameObject.Instantiate(EnemySpawn, randPos, transform.rotation);
            Enemies.Add(myEnemy.GetComponent<Enemies>());
        }
    }

    // IEnumerator SpawnMon () {
    //     float CurrentTime = 1.3f;
    //     float TargetTime = CurrentTime
    //     while (LoopTime < TargetTime) {
    //         yield return null;
    //     }
    // }
}
