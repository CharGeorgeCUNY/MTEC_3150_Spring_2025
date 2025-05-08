using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    Rigidbody rbPlayer;

    float speedDefault = 7.0f;
    public float fullSpeedfill = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        setSpeed();

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rbPlayer.velocity = (transform.forward * speedDefault);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Quaternion LookingAt = Quaternion.LookRotation(transform.right, transform.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, LookingAt, 45.0f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Quaternion LookingAt = Quaternion.LookRotation(transform.right, transform.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, LookingAt, -45.0f * Time.deltaTime);
            //rbPlayer.velocity = -transform.right * 2.0f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rbPlayer.velocity = (-transform.forward * speedDefault);
        }

    }

    void setSpeed()
    {
        if (Input.GetKey(KeyCode.Z)){
            speedDefault = 10.0f;
            fullSpeedfill -= 1.0f * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.X))
        {
            speedDefault = 15.0f;
        }

    }

    //public IEnumerator roadBlock1()
    //{
    //    WaitForSeconds delay = new WaitForSeconds(5.0f);
    //    blockOne.SetActive(true);
    //    yield return delay;
    //    blockOne.SetActive(false);

    //}
    //public IEnumerator roadBlock2()
    //{
    //    WaitForSeconds delay = new WaitForSeconds(5.0f);
    //    blockTwo.SetActive(true);
    //    yield return delay;
    //    blockTwo.SetActive(false);

    //}
    //public IEnumerator roadBlock3()
    //{
    //    WaitForSeconds delay = new WaitForSeconds(5.0f);
    //    blockOne.SetActive(true);
    //    yield return delay;
    //    blockTwo.SetActive(false);

    //}
    //public IEnumerator roadBlock4()
    //{
    //    WaitForSeconds delay = new WaitForSeconds(5.0f);
    //    blockOne.SetActive(true);
    //    yield return delay;
    //    blockTwo.SetActive(false);
    //}
}
