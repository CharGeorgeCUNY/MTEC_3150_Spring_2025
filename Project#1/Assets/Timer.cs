using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    float timer = 0.0f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = "Time survived : " + seconds.ToString();
    }
}
