using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowBehavior : MonoBehaviour
{
    private PlayerBehavior player;
    private float yLerp;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        yLerp = Mathf.Lerp(transform.position.y, player.highestPoint, Time.fixedDeltaTime* speed);
        transform.position = new Vector3(0, yLerp, transform.position.z);

    }
}
