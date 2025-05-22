using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    Rigidbody rbPlayer;

    float speedDefault = 7.0f;
    public float fullSpeedfill = 1.0f;


    //checkPoints Signs
    public GameObject checkPointOneText;
    public GameObject checkPointTwoText;
    public GameObject checkPointThreeText;
    public GameObject theEndText;

    //checkPoints GameObject Transform
    public Transform checkPointOnePos;
    public Transform checkPointTwoPos;
    public Transform checkPointThreePos;

   
    //reload scene, collision with startBorder
    string currentSceneName;

    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {

        setSpeed();
        Debug.Log("fullSpeedfill = " + fullSpeedfill);
        Debug.Log("speedSetting = " + speedDefault);

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
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rbPlayer.velocity = (-transform.forward * speedDefault);
        }

    }

    void setSpeed()
    {
        if (Input.GetKey(KeyCode.Z) && (fullSpeedfill > 0)){
            speedDefault = 15.0f;
        }
        else
        {
            speedDefault = 7.0f;
        }

        if ((rbPlayer.velocity.z > 0.5f) && Input.GetKey(KeyCode.Z)){
            fullSpeedfill -= 0.2f * Time.deltaTime;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "hazardOne")
        {
            transform.position = new Vector3(checkPointOnePos.position.x,checkPointOnePos.position.y,
                checkPointOnePos.position.z - 10.0f);
        }
        if (collision.gameObject.tag == "hazardTwo")
        {
            transform.position = new Vector3(checkPointTwoPos.position.x, checkPointTwoPos.position.y,
                checkPointTwoPos.position.z - 10.0f);
        }
        if (collision.gameObject.tag == "hazardThree")
        {
            transform.position = new Vector3(checkPointThreePos.position.x, checkPointThreePos.position.y,
                checkPointThreePos.position.z - 10.0f);
        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "start")
        {
            SceneManager.LoadScene(currentSceneName);
        }
        if (collision.gameObject.tag == "end")
        {
            StartCoroutine(theEnd());
        }
        if (collision.gameObject.tag == "One")
        {
            StartCoroutine(checkPointOne());
        }
        if (collision.gameObject.tag == "Two")
        {
            StartCoroutine(checkPointTwo());
        }
        if (collision.gameObject.tag == "Three")
        {
            StartCoroutine(checkPointThree());
        }

    }

    public IEnumerator theEnd()
    {
        WaitForSeconds delay = new WaitForSeconds(0.8f);
        theEndText.SetActive(true);
        yield return delay;
        SceneManager.LoadScene(currentSceneName);
    }
    public IEnumerator checkPointOne()
    {
        WaitForSeconds delay = new WaitForSeconds(1.5f);
        checkPointOneText.SetActive(true);
        yield return delay;
        checkPointOneText.SetActive(false);
        yield return delay;
    }
    public IEnumerator checkPointTwo()
    {
        WaitForSeconds delay = new WaitForSeconds(1.5f);
        checkPointTwoText.SetActive(true);
        yield return delay;
        checkPointTwoText.SetActive(false);
        yield return delay;
    }
    public IEnumerator checkPointThree()
    {
        WaitForSeconds delay = new WaitForSeconds(1.5f);
        checkPointThreeText.SetActive(true);
        yield return delay;
        checkPointThreeText.SetActive(false);
        yield return delay;
    }
}
