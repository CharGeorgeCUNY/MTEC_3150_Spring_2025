using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawnerbehavior : MonoBehaviour
{
    private PlayerBehavior player;
    public GameObject platform, bouncyPlatform;
    public float spawnDistance;
    public float xBounds, yBounds;
    public float xPlatVariation;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position)< spawnDistance)
        {
            float xRandPos = Random.Range(-xBounds, xBounds);
            float xRandScale = Random.Range(xPlatVariation, 10);
            Vector3 nextPlatPos = (Vector3.up * yBounds) + (Vector3.right * xRandPos);

            GameObject choosenPlatform = platform;
            if (xRandPos > xBounds * 0.75f || xRandPos < -xBounds * 0.75f)
            {
                choosenPlatform = bouncyPlatform;
            }
           else  if (xRandScale < 5)
            {
                GameObject platMirror = Instantiate(choosenPlatform, -nextPlatPos + (Vector3.up * transform.position.y), transform.rotation);
                platMirror.transform.localScale = (Vector3.right * xRandScale) + Vector3.up;

            }

            GameObject platClone = Instantiate(choosenPlatform, nextPlatPos + (Vector3.up* transform.position.y), transform.rotation) ;
            platClone.transform.localScale = (Vector3.right * xRandScale)+ Vector3.up;
            transform.position = platClone.transform.position;
        }
        
    }
}
