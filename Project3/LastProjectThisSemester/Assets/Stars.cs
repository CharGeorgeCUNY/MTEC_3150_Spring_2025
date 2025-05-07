using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static System.TimeZoneInfo;

public class Stars : MonoBehaviour
{

    private ParticleSystem ps; 
  
    //int psvel;
    public int TransitionTimer;
    //float ratio = 2f;
    bool LevelComplete;
    public float starTransitionSpeed;
    // Start is called before the first frame update

    void Start()
    {
        TransitionTimer = GetComponent<GameManager>().TransitionTimer;
        //LevelComplete = GetComponentInChildren<Player>().LevelComplete;
        ps = GetComponent<ParticleSystem>();
        //psvel = ps.velocityOverLifetime;

        //LevelComplete = player.LevelComplete;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (GetComponentInChildren<Player>() != null) LevelComplete = GetComponentInChildren<Player>().LevelComplete;

        //Debug.Log("FUCK");
        if (LevelComplete) {
          // Debug.Log("why");
           StartCoroutine(SpeedUPThenSlow());
        }
    }

    private IEnumerator SpeedUPThenSlow()
    {
        var vel = ps.velocityOverLifetime;
        vel.enabled = true;
        vel.speedModifier = new ParticleSystem.MinMaxCurve(starTransitionSpeed);

        yield return new WaitForSeconds(TransitionTimer);

        vel = ps.velocityOverLifetime;
        
        vel.speedModifier = new ParticleSystem.MinMaxCurve(1f);
    }
  
}
