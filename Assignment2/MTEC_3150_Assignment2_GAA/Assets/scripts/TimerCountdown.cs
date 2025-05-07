
using UnityEngine;
using TMPro;    

public class TimerCountdown : MonoBehaviour
{
    public TMP_Text timerText;          //This is the inspector thingy
    public float totalTime = 240f;  // 4 minutes in seconds

    private float remainingTime;

    void Start()
    {
        remainingTime = totalTime;
    }

    void Update()
    {
        if (remainingTime > 0)  // Credit to Timmir on itch.io
        {
            remainingTime -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            timerText.text = "00:00";
        }
    }
}