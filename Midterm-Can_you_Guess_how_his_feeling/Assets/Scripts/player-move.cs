using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class playerMove : MonoBehaviour
{
    public CharacterController controller;
    public Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 3.5f;
    private float jumpHeight = 8.0f;
    private float gravityValue = -9.81f;
    public float maxDegreeDelta = 45.0f;
    public LayerMask LayersToCheck;

    private CapsuleCollider capsuleCollider;

    //public CinemachineVirtualCamera myVirtualCamera;

    // Assign each virtual camera in the Inspector.
    public CinemachineVirtualCamera zone1Camera;
    public CinemachineVirtualCamera zone2Camera;
    public CinemachineVirtualCamera zone3Camera;
    public CinemachineVirtualCamera zone4Camera;
    public CinemachineVirtualCamera zone5Camera;
    public CinemachineVirtualCamera zone6Camera;
    public CinemachineVirtualCamera zone7Camera;
    public CinemachineVirtualCamera zone8Camera;
    public CinemachineVirtualCamera zone9Camera;
    public CinemachineVirtualCamera zone10Camera;
    public CinemachineVirtualCamera zone11Camera;
    public CinemachineVirtualCamera zone12Camera;
    public CinemachineVirtualCamera zone13Camera;
    public CinemachineVirtualCamera zone14Camera;
    public CinemachineVirtualCamera zone15Camera;
    public CinemachineVirtualCamera zone16Camera;



    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        capsuleCollider = GetComponent<CapsuleCollider>(); // Cache the collider

    }

    // Update is called once per frame
    void Update()
    {
        //cams pos
        Vector3 Loook = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);

        Vector3 Anngle = Loook - transform.position;
        transform.rotation = Quaternion.LookRotation(Anngle);

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //if (move != Vector3.zero)
        //{
        //    Quaternion LookingAt = Quaternion.LookRotation(move);
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, LookingAt, maxDegreeDelta * Time.deltaTime);
        //}

        // Apply horizontal movement
        controller.Move(move * playerSpeed * Time.deltaTime);

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;


        // Apply vertical movement
        controller.Move(playerVelocity * Time.deltaTime);


        //Jumping
        //if (Input.GetButtonDown("Jump") && groundedPlayer)
        //{
        //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        //}

        //groundedPlayer = controller.isGrounded;
        //ded lol
        //Debug.Log("isGrounded: " + groundedPlayer);

        // playerVelocity.y += gravityValue * Time.deltaTime;
        // controller.Move(playerVelocity * Time.deltaTime);
        // playerVelocity = controller.velocity;
        // Debug.Log(Physics.Raycast(transform.position, Vector3.down, GetComponent<CapsuleCollider>().height / 1.9f, LayersToCheck));

    }
    //how will you know which zone  is which? theyre all tagged zone but i want the cam that activates on the specific zone
    //i dont wanna add didfferent tags for every zone
    //go to the vc thats closest to player
    //void OnTriggerEnter(Collider other)
    //{

    //    if (other.gameObject.CompareTag("zone"))
    //    {
    //        Debug.Log("Exited trigger with object tagged: " + "zone");
    //        myVirtualCamera.Priority = 2; //the higher # higher priority so the main cam becomes this cam
    //    }
    //}
    //void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("zone1"))
    //    {
    //        Debug.Log("Exited trigger with object tagged: " + "zone");
    //        myVirtualCamera.Priority = 1;
    //    }
    //}


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene("Selection");

            //Debug.Log("Exited trigger with object tagged: " + "zone");

        }

        switch (other.gameObject.tag)
        {
            case "zone1":
                Debug.Log("Entered trigger with object tagged: zone1");
                zone1Camera.Priority = 2;
                break;
            case "zone2":
                Debug.Log("Entered trigger with object tagged: zone2");
                zone2Camera.Priority = 2;
                break;
            case "zone3":
                Debug.Log("Entered trigger with object tagged: zone3");
                zone3Camera.Priority = 2;
                break;
            case "zone4":
                Debug.Log("Entered trigger with object tagged: zone4");
                zone4Camera.Priority = 2;
                break;
            case "zone5":
                Debug.Log("Entered trigger with object tagged: zone5");
                zone5Camera.Priority = 2;
                break;
            case "zone6":
                Debug.Log("Entered trigger with object tagged: zone6");
                zone6Camera.Priority = 2;
                break;
            case "zone7":
                Debug.Log("Entered trigger with object tagged: zone7");
                zone7Camera.Priority = 2;
                break;
            case "zone8":
                Debug.Log("Entered trigger with object tagged: zone8");
                zone8Camera.Priority = 2;
                break;
            case "zone9":
                Debug.Log("Entered trigger with object tagged: zone9");
                zone9Camera.Priority = 2;
                break;
            case "zone10":
                Debug.Log("Entered trigger with object tagged: zone10");
                zone10Camera.Priority = 2;
                break;
            case "zone11":
                Debug.Log("Entered trigger with object tagged: zone11");
                zone11Camera.Priority = 2;
                break;
            case "zone12":
                Debug.Log("Entered trigger with object tagged: zone12");
                zone12Camera.Priority = 2;
                break;
            case "zone13":
                Debug.Log("Entered trigger with object tagged: zone13");
                zone13Camera.Priority = 2;
                break;
            case "zone14":
                Debug.Log("Entered trigger with object tagged: zone14");
                zone14Camera.Priority = 2;
                break;
            case "zone15":
                Debug.Log("Entered trigger with object tagged: zone15");
                zone15Camera.Priority = 2;
                break;
            case "zone16":
                Debug.Log("Entered trigger with object tagged: zone16");
                zone16Camera.Priority = 2;
                break;
            default:
                break;
        }
    }

    void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "zone1":
                Debug.Log("Exited trigger with object tagged: zone1");
                zone1Camera.Priority = 1;
                break;
            case "zone2":
                Debug.Log("Exited trigger with object tagged: zone2");
                zone2Camera.Priority = 1;
                break;
            case "zone3":
                Debug.Log("Exited trigger with object tagged: zone3");
                zone3Camera.Priority = 1;
                break;
            case "zone4":
                Debug.Log("Exited trigger with object tagged: zone4");
                zone4Camera.Priority = 1;
                break;
            case "zone5":
                Debug.Log("Exited trigger with object tagged: zone5");
                zone5Camera.Priority = 1;
                break;
            case "zone6":
                Debug.Log("Exited trigger with object tagged: zone6");
                zone6Camera.Priority = 1;
                break;
            case "zone7":
                Debug.Log("Exited trigger with object tagged: zone7");
                zone7Camera.Priority = 1;
                break;
            case "zone8":
                Debug.Log("Exited trigger with object tagged: zone8");
                zone8Camera.Priority = 1;
                break;
            case "zone9":
                Debug.Log("Exited trigger with object tagged: zone9");
                zone9Camera.Priority = 1;
                break;
            case "zone10":
                Debug.Log("Exited trigger with object tagged: zone10");
                zone10Camera.Priority = 1;
                break;
            case "zone11":
                Debug.Log("Exited trigger with object tagged: zone11");
                zone11Camera.Priority = 1;
                break;
            case "zone12":
                Debug.Log("Exited trigger with object tagged: zone12");
                zone12Camera.Priority = 1;
                break;
            case "zone13":
                Debug.Log("Exited trigger with object tagged: zone13");
                zone13Camera.Priority = 1;
                break;
            case "zone14":
                Debug.Log("Exited trigger with object tagged: zone14");
                zone14Camera.Priority = 1;
                break;
            case "zone15":
                Debug.Log("Exited trigger with object tagged: zone15");
                zone15Camera.Priority = 1;
                break;
            case "zone16":
                Debug.Log("Exited trigger with object tagged: zone16");
                zone16Camera.Priority = 1;
                break;
            default:
                break;
        }
    }
}
