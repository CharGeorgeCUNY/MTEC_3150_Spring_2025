using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]GameObject planet;
    [SerializeField]GameObject player;


    AudioClip splat;
    public bool playerdied;
    public bool LevelComplete = false;
    public int levelTimer = 10;
    public bool killplayerok = false;
    bool isCountingDown;
    public bool PlanetCanMove;
    public int levelnum = 0;
    // Start is called before the first frame update

    public int TransitionTimer;

    AudioSource Auso;

    private void Start()
    {
        Auso = GetComponent<AudioSource>();
        NewGame();
    }
    void FixedUpdate()
    {
        if(GetComponentInChildren<Player>() != null) LevelComplete = GetComponentInChildren<Player>().LevelComplete;
        if (LevelComplete) killplayerok = true;
        else killplayerok = false;

        if (playerdied) PlayerDied();



    }
    private void LateUpdate()
    {
        if (LevelComplete)
        {
            levelnum++;
            NewGame();
        }
        
    }
    public void ClearPlayer()
    {
        if(transform.childCount > 0) Destroy(transform.GetChild(0).gameObject);
       
    }
    public void ClearPlanet()
    {
        if (transform.GetChild(0) == true) Destroy(transform.GetChild(0).gameObject);
        
    }
    void NewGame()
    {
       
        //ClearPlayer();
        

        StopAllCoroutines();
        this.GetComponent<Display>().timerOn = false;
        this.GetComponent<Display>().totalTime = levelTimer;

        PlanetCanMove = false;
        Instantiate(planet,  new Vector2(0, 40), Quaternion.identity, this.transform);
        LevelComplete = false;
        StartCoroutine(NextLevel());
        

    }
    private void PlayerDied()
    {
        playerdied = false;
        Auso.Play();

        StartCoroutine(StartNewAfterPlayerDeath());

    }
    private IEnumerator StartNewAfterPlayerDeath()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   
    private IEnumerator LevelCountDown()
    {
        yield return new WaitForSeconds(levelTimer);
        SceneManager.LoadScene(2, LoadSceneMode.Single);
        //NewGame();

    }
    private IEnumerator NextLevel()
    {

        yield return new WaitForSeconds(TransitionTimer);
        StartCoroutine(LevelCountDown());
        Instantiate(player, new Vector2(0, 15), Quaternion.identity, this.transform);
        this.GetComponent<Display>().timerOn = true;
        PlanetCanMove = true;


    }
   
    

}
