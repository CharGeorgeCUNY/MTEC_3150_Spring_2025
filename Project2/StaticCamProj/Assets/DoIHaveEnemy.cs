using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoIHaveEnemy : MonoBehaviour
{
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject Portal;
    bool ran = false;
    public bool CheckProbability(int outcome, int totalOutcomes)
    {
        // If the total outcomes are less than or equal to 1, always return true (100% chance)
        if (totalOutcomes <= 1)
            return true;

        // Generate a random number between 1 and totalOutcomes (inclusive)
        int randomChance = Random.Range(1, totalOutcomes + 1);

        // If the random number matches the outcome (e.g., 1 in `totalOutcomes` chance)
        return randomChance == outcome;
    }
    

    //private void FixedUpdate()
    //{
    //    if (!ran)
    //    {
    //        if (CheckProbability(1, 2))

    //        {
    //            for(int i = 0; i < Random.Range(1,4); i++)
    //            {
    //                var vec3 = new Vector3(Random.Range(-5f,5f),0,Random.Range(-5f,5f));
    //                Instantiate(enemy1, this.transform.position + Vector3.up * 5 + vec3, Quaternion.identity);
    //            }
                

    //        }
    //        ran = true;
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !ran)
        {
            if (CheckProbability(1, 2))

            {
                for (int i = 0; i < Random.Range(1, 4); i++)
                {
                    var vec3 = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
                    Instantiate(Portal, this.transform.position + Vector3.up * 3 + vec3, Quaternion.identity);
                }


            }
            ran = true;
        }
    }
}
