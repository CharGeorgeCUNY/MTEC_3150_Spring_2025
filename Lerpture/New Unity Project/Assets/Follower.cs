using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Follower : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public float speed = 4.0f;
    public Vector3 TargetSize_Big;
    public Vector3 TargetSize_Small;
    public Vector3 TargetSize;
    void Start()
    {
        TargetSize = TargetSize_Small;
    }

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime; // calculate distance to move
        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        transform.position = Vector3.Lerp(transform.position, target.transform.position, .02f);
        //transform.localScale = Vector3.MoveTowards(transform.localScale, TargetSize, .04f);
        transform.localScale = Vector3.Lerp(transform.localScale, TargetSize, .04f);
    }

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        TargetSize = TargetSize_Big;
        Debug.Log("Mouse is over GameObject.");
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        TargetSize = TargetSize_Small;
        Debug.Log("Mouse is no longer on GameObject.");
    }
}
