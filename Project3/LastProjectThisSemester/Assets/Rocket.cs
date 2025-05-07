using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static System.TimeZoneInfo;

public class Rocket : MonoBehaviour
{
    GameManager gm;
    public float FlySpeed;
    bool getrocketstartpos =true;
    Vector2 currentpos;
    bool stoplerppos = false;

    private float moveDuration = 2f; // duration of the movement
    private float moveElapsed = 0f;
    bool TimeToGo = false;
    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponentInParent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.LevelComplete)
        {
            StartCoroutine(FlyAway());
        }
       
    }
    private void FixedUpdate()
    {
        if (TimeToGo)
        {
            if (getrocketstartpos)
            {
                currentpos = this.transform.position;
                moveElapsed = 0f; // reset time
                getrocketstartpos = false;
            }

            moveElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(moveElapsed / moveDuration);

            Vector2 targetPos = Vector2.up * 10;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3);
            if (!stoplerppos) transform.position = Vector2.Lerp(currentpos, targetPos, t);
        }
    }
    IEnumerator FlyAway()
    {
        this.transform.parent = null;
        TimeToGo = true;
        this.gameObject.tag = "Untagged";
        yield return new WaitForSeconds(gm.TransitionTimer);
       
        StartCoroutine(KeepFlying());
    }

    private IEnumerator KeepFlying()

    {
        stoplerppos = true;
        float elapsed = 0f;

        while (elapsed < 3f)
        {
            this.transform.position += Vector3.up * FlySpeed * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
        Destroy(this.gameObject);

    }
  
}
