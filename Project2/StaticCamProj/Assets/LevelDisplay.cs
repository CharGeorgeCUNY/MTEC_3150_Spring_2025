using UnityEngine;
using TMPro;

public class LevelDisplay : MonoBehaviour
{
    public int levelNum, deathNum, health;
    public string title1, title2, title3;
    [SerializeField] private TextMeshProUGUI levelText;
    

    void Start()
    {
        
        deathNum = 0;
        UpdateLevelText();
    }



    public void SetLevel(int newLevel)
    {
        levelNum = newLevel;
        UpdateLevelText();
    }
    public void SetHealth(int currentHealth)
    {
        health = currentHealth;
        UpdateLevelText();
    }
    public void AddDeaths()
    {
        deathNum++;
        UpdateLevelText();
    }
    public void SetTitle(int levelNum, int wrathfulSlain, int boxesExecuted)
    {
        deathNum = wrathfulSlain;
        if(levelNum > 20)
        {
            title1 = " Immortal ";
        }
        if(wrathfulSlain > 30)
        {
            title2 = " Slayer ";
        }
        if (boxesExecuted >= 100)
        {
            title3 = " Boxer ";
        }

    }
    private void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = "Level: " + levelNum.ToString() + "\n" + "Wrathful Slain: " + deathNum.ToString() + "\n" + "Health: " + health.ToString() + "\n" + "Title: " + title1 + title2 + title3;
        }

    }
}