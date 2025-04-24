using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _Instance;

    public static GameManager GetGameManager()
    {
        if (_Instance == null)
        {
            _Instance = FindObjectOfType<GameManager>();
        }
        return _Instance;
    }
    
    public void GoToSceneTwo() {
        SceneManager.LoadScene("MainScene");
    }

    public void EndScene() {
        SceneManager.LoadScene("EndScene");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartScene() {
        SceneManager.LoadScene("TitleScene");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public TextMeshProUGUI HealthText;

    public int MaxHealth;
    public int PlayerDeath = 0;

    public string LosingWords;

    public TextMeshProUGUI DefeatedText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int IncrementHealth(bool IsHealth) {
        if (IsHealth) {
            MaxHealth -= 1;
            string Health = "";

            Health += (MaxHealth);
            HealthText.SetText(Health);
        }

        if (MaxHealth <= PlayerDeath) {
            LosingWords = "You Lose!";
            string LoseText = "";

            LoseText += (LosingWords);
            DefeatedText.SetText(LoseText);
            
            EndScene();
        }

        return 0;
    }
}
