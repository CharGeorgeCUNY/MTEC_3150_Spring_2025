using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI HealthText;

    public int MaxHealth;
    public int PlayerDeath = 0;

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

        return 0;
    }
}
