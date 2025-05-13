using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public GameObject TallPillarPref;
    public GameObject RegPillarPref;
    public GameObject ShortPillarPref;
    public GameObject HookedPillarPref;
    GameManager gm;
    SpriteRenderer sr;
    float TransitionTimer;
    public int fallSpeed;
    public int rotSpeed;   // Start is called before the first frame update
    void Start()
    {
        Color randColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        sr = GetComponent<SpriteRenderer>();
       // sr.color = randColor;
        gm = GetComponentInParent<GameManager>();
        TransitionTimer = gm.TransitionTimer;
        StartCoroutine(FallDown());
        
       
        int NumOfTowers = Random.Range(5, 15);
        for (int i = 0; i < NumOfTowers; i++)
        {
            GameObject currentPillar = null;
            int ran = Random.Range(0, 4);
            int randAnglemin = Random.Range(20, 160);
            int randAnglemax = Random.Range(200, 340);
            int randAngle = randAnglemin;
            if (ran > 2) randAngle = randAnglemax;
            randAngle = Random.Range(20, 340);

            Quaternion rot = Quaternion.Euler(0,0,randAngle);
            switch (ran)
            {
                case 0:
                    currentPillar= TallPillarPref; break;
                case 1:
                    currentPillar = RegPillarPref; break;
                case 2:
                    currentPillar = ShortPillarPref; break;
                case 3:
                    currentPillar = HookedPillarPref; break;    
                       
                default: break;
            }
            Instantiate(currentPillar, this.transform.position + Vector3.forward, rot, this.transform);
            
        }
    }
    void Update()
    {
        if (gm.PlanetCanMove)
        {
            float horizontalInput = Input.GetAxis("Horizontal");


            this.transform.Rotate(new Vector3(0, 0, -horizontalInput * rotSpeed * Time.deltaTime));
        }



        if (gm.LevelComplete)
        {
           // Debug.Log("Planet Level Complete");
            this.transform.parent = null;
            StartCoroutine(FallDownAndDie());
        }
    }
    private IEnumerator FallDownAndDie()
    {
        float elapsed = 0f;

        while (elapsed < TransitionTimer)
        {
            this.transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
        Destroy(this.gameObject);

    }
    private IEnumerator FallDown()
    {

        float elapsed = 0f;

        while (elapsed < TransitionTimer)
        {
            //this.transform.parent = null;
            this.transform.localPosition += Vector3.down * fallSpeed * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }


    }

}
