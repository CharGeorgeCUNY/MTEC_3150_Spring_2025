using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    public bool timer;
    public float totalTime; 
    public TextMeshProUGUI timerText; 
    public bool timerOn = true;
    int levelnum;

    void Start()
    {
      
        UpdateTimerText();
        UpdateLevelText();
    }

    void FixedUpdate()
    {
        levelnum = GetComponent<GameManager>().levelnum;
        UpdateLevelText();

        if (timerOn && totalTime > 0)
        {
            totalTime -= Time.deltaTime;
            UpdateTimerText();
        }
        else if (totalTime <= 0)
        {
            timerOn = false; 
            UpdateTimerText(); 
        }
    }

    void UpdateTimerText()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(totalTime / 60);
            int seconds = Mathf.FloorToInt(totalTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = "Levels Complete: " + (levelnum).ToString();
        }
    }
}





  

