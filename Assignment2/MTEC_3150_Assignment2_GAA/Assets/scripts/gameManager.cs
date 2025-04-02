using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float startTime;
    public int fishCounter = 0;
    public string fishTag = "Fish"; // Tag for fish objects, This was for when I kept changing my tags like an idiot
    public float interactionRange = 10f; // Distance to collect fish
    public Transform player; // this is where you pull in the player in the inspector

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        // Check if more than 4 minutes have happened since the start.
        if (Time.time - startTime > 240f)
        {
            ResetScene();
        }

        // Reset if fishCounter reaches 4
        if (fishCounter >= 4)
        {
            ResetScene();
        }

        // Check for fish collection input
        if (Input.GetKeyDown(KeyCode.E))
        {
            CollectNearbyFish();
        }
    }

    void CollectNearbyFish()
    {
        GameObject[] fishObjects = GameObject.FindGameObjectsWithTag(fishTag);

        foreach (GameObject fish in fishObjects)
        {
            if (Vector3.Distance(player.position, fish.transform.position) <= interactionRange)
            {
                Destroy(fish);
                fishCounter++;
                Debug.Log("Fish collected! Total: " + fishCounter);
                break; // the break makes it so that it doesnt collect more than one. They are spaced out, but this was useful when I was developing the feature.
            }
        }
    }

    void ResetScene() //Exactly what this ish looks like, fool!
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
