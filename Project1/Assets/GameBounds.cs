using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBounds : MonoBehaviour
{

    [SerializeField] GameObject piece1;
    [SerializeField] GameObject piece2;
    [SerializeField] GameObject piece3;
    [SerializeField] GameObject piece4var;
    [SerializeField] GameObject piece4;
    [SerializeField] GameObject wall;
    [SerializeField] GameObject pieceX;

    [SerializeField] GameObject Enemy1;

    GameObject currentPiece;
    GameObject player;
    public int goobdeaths = 0;
    public int levelnum = 0;
    bool endExists;
    // Start is called before the first frame update
    void Start()
    {
    
        player = GameObject.Find("Player");

        NewGame();

    }
    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    public void ClearChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
        
    public void InsEnemy1(int doihaveEnemy, Vector3 pos, Quaternion rot)
    {
        if (doihaveEnemy + levelnum > 15)
        {
            int randCount = Random.Range(1, 5);
            for (int e = 0; e < randCount * levelnum; e++)
            {
                Instantiate(Enemy1, pos + Vector3.back, rot, this.transform);
            }

        }
    }


    public void NewGame()
    {
        levelnum++;
        var level = GameObject.Find("LevelDisplay");
        level.GetComponent<LevelDisplay>().SetLevel(levelnum);



        player.SetActive(false);

         


        
        
        ClearChildren();
        endExists = false;
        for (int i = -10; i < 10; i++)
        {
            for (int j = -10; j < 10; j++)
            {
                

                

                currentPiece = piece1;
                int randrot = 0;
                int amiRotating = Random.Range(1, 20);
                int amiEnd = Random.Range(1, 25);
                int doihaveEnemy = Random.Range(0, 20);

                Vector3 pos = new Vector3(i * 7, j * 7, 0);
                Quaternion rot = Quaternion.Euler(0, 0, 90 * randrot);

                if (pos == Vector3.zero)
                {
                    currentPiece = piece1;
                }
                


                else if ((i <= -7 || i >= 6) || (j <= -7 || j >= 6))
                {
                    currentPiece = wall;
                }
                else if((i == -6 || i == 5) || (j == -6 || j == 5))
                {
                    currentPiece = piece1;
                    InsEnemy1(doihaveEnemy, pos, rot);
                    
                }
                
                else if(amiEnd == 10 && endExists == false)
                {
                    currentPiece = pieceX;
                    endExists = true;
                    

                }
                else if (amiRotating > 15)
                {
                    randrot = Random.Range(0, 3);
                    currentPiece = piece3;
                    InsEnemy1(doihaveEnemy, pos, rot);
                }
                else if (amiRotating > 10)
                {
                    randrot = Random.Range(0, 3);
                    var randType = Random.Range(0, 4);
                    if(randType <= 2)
                    {
                        currentPiece = piece4;
                    }
                    else
                    {
                        currentPiece = piece4var;
                    }
                   

                }

                else if (amiRotating > 5)
                {
                    randrot = Random.Range(0, 3);
                    currentPiece = piece2;
                    InsEnemy1(doihaveEnemy, pos, rot);
                }
                Instantiate(currentPiece, pos, rot, this.transform);





            }


        }
        player.SetActive(true);
        player.transform.position = new Vector3(0,0,-1);

        if (endExists == false)
        {
            NewGame();
        }
    }
}
