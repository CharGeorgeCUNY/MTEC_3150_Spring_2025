using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class GameTheManager : MonoBehaviour
{
    private static GameTheManager _gameManager;
    public static GameTheManager GetGameManager()
    {
        return GameObject.FindObjectOfType<GameTheManager>();
    }
    // Start is called before the first frame update
    public TextMeshProUGUI ScoreText;
    public Canvas GameOver;
    public int Score;
    void Start()
    {
        GameOver.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementScore()
    {
         Score++;
        SetScoreDraw();
        CheckScore();
    }

    public void SetScoreDraw()
    {
        ScoreText.SetText(Score.ToString());

    }

    public void CheckScore()
    {
         bool isScore = (Score==5);
        if(isScore = true)
        {
           Debug.Log("You win!");
        }
    }
}
