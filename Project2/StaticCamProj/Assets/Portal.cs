using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] float EnemySpawnTime;
    [SerializeField] GameObject enemy1;

    AudioSource auso;
    void Start()
    {
        auso = GetComponent<AudioSource>();
        auso.pitch += Random.Range(-0.5f, .5f); 
        StartCoroutine(SpawnEnemy());
        
    }
    private IEnumerator SpawnEnemy()
    {


        
        yield return new WaitForSeconds(EnemySpawnTime);
        Instantiate(enemy1, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);




    }
}
