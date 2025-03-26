using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class gameamanger : MonoBehaviour
{
    private static gameamanger _gameManager;
    public static gameamanger GetGameManager()
    {
        return GameObject.FindObjectOfType<gameamanger>();
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
        if(isScore = false)
        {
           Debug.Log("You win!");
        }
    }
}
