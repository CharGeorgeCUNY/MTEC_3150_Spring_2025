using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour
{


    [SerializeField] GameObject piece1, piece1var;
    [SerializeField] GameObject piece2, piece2var;
    [SerializeField] GameObject pieceX, specialroom;
    GameObject currentPiece;
    GameObject level;

    [SerializeField] AudioClip song1, song2;
    public int music = 0, count;

    int levelnum = 0;
    public int Health, boxbroke, wrathfulSlain;
    public int tileoffset;
    GameObject player;
    GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        level = GameObject.Find("Display");
        player = GameObject.Find("Player");
        NewGame();
    }

    private void FixedUpdate()
    {
        Health = player.GetComponent<Player>().Health;

        level.GetComponent<LevelDisplay>().SetHealth(Health);
        level.GetComponent<LevelDisplay>().SetTitle(levelnum, wrathfulSlain, boxbroke);

        if (music == 1)
        {

            GetComponent<AudioSource>().clip = song2;

            GetComponent<AudioSource>().Play();
            music = -1;


        }
        else if (music == 0)
        {
            GetComponent<AudioSource>().clip = song1;

            GetComponent<AudioSource>().Play();
            music = -1;
        }

        
    }
    public void ClearChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

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
    public void NewGame()
    {
        //cam.GetComponent<AudioListener>().enabled = false;
        //cam.GetComponent<PostProcessLayer>().enabled = false;
        cam.GetComponent<CinemachineBrain>().enabled = false;
        levelnum++;
       
        level.GetComponent<LevelDisplay>().SetLevel(levelnum);

        player.GetComponent<Player>().canAttack = true;

        //player.SetActive(false);
        player.transform.rotation = Quaternion.Euler(0, 90, 0);


        count = 0;
        int randrot = 1;
        int right = 0, up = 0, left = 0, irotated = 0;
        bool rT = false, uT = false, lT = false, dT = false;
        int currentrot = 90;


        ClearChildren();
       

        while (count <= 5)
        {
            
            count++;

            Vector3 pos = new Vector3(right * tileoffset, 0, up * tileoffset);

            Quaternion rot = Quaternion.Euler(0, currentrot, 0);
            if (count == 1 && CheckProbability(1, 10))
            {
                //cam.GetComponent<AudioListener>().enabled = true;
               // cam.GetComponent<PostProcessLayer>().enabled = true;
                cam.GetComponent<CinemachineBrain>().enabled = true;
                player.transform.position = new Vector3(-3, 6, 0);
                rot = Quaternion.Euler(0, 0, 0);
                pos = new Vector3(100, -103, 0);
                currentPiece = specialroom;
                Instantiate(currentPiece, pos, rot, this.transform);

                player.GetComponent<Player>().canAttack = false;
                
                break;

            }
           
            else if (CheckProbability(1, 5))
            {

                randrot = 2;

            }
            else
            {
                randrot = 1;
            }


           

            if(count == 6) {
                if(GetComponent<AudioSource>().clip == song2)
                {
                    music = 0;
                }
                currentPiece = pieceX;
                Instantiate(currentPiece, pos, rot, this.transform);
                
            }
           


            else if (randrot == 1 || count == 1)
            {
                
                
                if (CheckProbability(1, 2))
                {

                    currentPiece = piece1;

                }
                else
                {
                    currentPiece = piece1var;
                }
                
                Instantiate(currentPiece, pos, rot, this.transform);
                if (uT == true)
                {
                    up++;
                    
                }
                else if(lT == true)
                {
                    
                    right--;
                    
                }
                else if(dT == true)
                {
                    up--;
                }
                else
                {
                    right++;
                }
               

            }
            else if (randrot == 2)
            {
                switch (irotated)
                {
                    case 0: currentrot = 90; break;
                    case 1: currentrot = 0; break;
                    case 2: currentrot = 270; break;
                    case 3: currentrot = 180; break;
                    default: irotated = 0; break;
                }

                rot = Quaternion.Euler(0, currentrot, 0);
                irotated++;
                Debug.Log(irotated);

                
                if (CheckProbability(1, 2))
                {

                    currentPiece = piece2;

                }
                else
                {
                    currentPiece = piece2var;
                }
                

                Instantiate(currentPiece, pos, rot, this.transform);
                if(uT == true)
                {
                    right--;
                 
                    uT = false;
                    lT = true;
                }
                else if (lT == true)
                {
                   
                    up--;
                    lT = false;
                    dT = true;
                }
                else if (dT == true)
                {
                    right++;
                    
                    dT = false;
                    rT = true;
                }
                else
                {
                    up++;
                    uT = true;
                }
                currentrot -= 90;
               



            }


            //player.SetActive(true);

            player.transform.position = new Vector3(0, 6, 0);





        }









    }
}
