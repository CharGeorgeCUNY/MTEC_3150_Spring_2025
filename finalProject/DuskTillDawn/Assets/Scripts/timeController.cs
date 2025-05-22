using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.Rendering.BoolParameter;
using UnityEngine.SceneManagement;

public class timeController : MonoBehaviour
{
    public float timeMultipler;
    public float startHour;

    public TMP_Text clockDisplay;
    public Light sunLight;
    public float sunRise;
    public float sunSet;
    TimeSpan sunriseTime;
    TimeSpan sunsetTime;

    public DateTime currentTime;

    //reload scene, collision with startBorder
    string currentSceneName;
    public GameObject gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        sunriseTime = TimeSpan.FromHours(sunRise);
        sunsetTime = TimeSpan.FromHours(sunSet);

        //gameOver
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        updateClock();
        rotateSun();
    }

    void updateClock()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultipler);

        if (clockDisplay != null)
        {
            clockDisplay.text = currentTime.ToString("HH:mm");
        }
        if ((currentTime.Hour == 20) && (currentTime.Minute == 50))
        {
            StartCoroutine(gameOver());
        }
       
    }
    void rotateSun()
    {
        float sunRotation;

        if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseTosunset = timeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = timeDifference(sunriseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseTosunset.TotalMinutes;

            sunRotation = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            TimeSpan sunsetTosunrise = timeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = timeDifference(sunsetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetTosunrise.TotalMinutes;

            sunRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunRotation, Vector3.right);
    }

    TimeSpan timeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;
        if(difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }
        return difference;
    }

    public IEnumerator gameOver()
    {
        WaitForSeconds delay = new WaitForSeconds(1.0f);
        gameOverText.SetActive(true);
        yield return delay;
        SceneManager.LoadScene(currentSceneName);


    }
}