using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhichHallAmI : MonoBehaviour
{
    bool ran = false;
    bool third = true, second = true, first = true;
    // Start is called before the first frame update
    void FixedUpdate()
    {
        if (!ran)
        {
           // var check = CheckProbability(1, 2);
            if (CheckProbability(1, 2))
            {
                GameObject.Find("BoxSpawn3").SetActive(false);
                third = false;
                
            }
            else
            {
                GameObject.Find("BoxSpawn2").SetActive(false);
                GameObject.Find("BoxSpawn2var").SetActive(false);
                GameObject.Find("BoxSpawn1").SetActive(false);
                GameObject.Find("BoxSpawn1var").SetActive(true);
            }
            
            if (third == false)
            {

                if(CheckProbability(1, 4))
                {
                    if(CheckProbability(1, 2))
                    {
                        GameObject.Find("BoxSpawn2").SetActive(false);
                        GameObject.Find("BoxSpawn1").SetActive(false);
                        GameObject.Find("BoxSpawn1var").SetActive(false);
                    }
                    else
                    {
                        GameObject.Find("BoxSpawn2var").SetActive(false);
                        GameObject.Find("BoxSpawn1").SetActive(false);
                        GameObject.Find("BoxSpawn1var").SetActive(false);

                    }
                    
                }
                else
                {
                    GameObject.Find("BoxSpawn2").SetActive(false);
                    GameObject.Find("BoxSpawn2var").SetActive(false);
                    second = false;
                    
                }
                
                
                


            }
            else if(second == false && third == false)
            {

                if (CheckProbability(1, 2))
                {
                    GameObject.Find("BoxSpawn1").SetActive(false);

                }
                else
                {
                    GameObject.Find("BoxSpawn1var").SetActive(false);
                }
               

            }
            ran = true;
        }
        
    }

    public bool CheckProbability(int outcome, int totalOutcomes)
    {
        
        if (totalOutcomes <= 1)
            return true;

       
        int randomChance = Random.Range(1, totalOutcomes + 1);

      
        return randomChance == outcome;
    }
}
