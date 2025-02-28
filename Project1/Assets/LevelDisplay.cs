using UnityEngine;
using TMPro;

public class LevelDisplay : MonoBehaviour
{
     public int levelNum, deathNum;
    [SerializeField] private TextMeshProUGUI levelText;

    void Start()
    {
        levelNum = GetComponentInParent<GameBounds>().levelnum;
        deathNum = 0;
        UpdateLevelText();
    }

    

    public void SetLevel(int newLevel)
    {
        levelNum = newLevel;
        UpdateLevelText();
    }
    public void AddDeaths()
    {
        deathNum++;
        UpdateLevelText();
    }
    private void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = "Level: " + levelNum.ToString() + "\n" + "Goobers Slain: " + deathNum.ToString();
        }
        
    }
}