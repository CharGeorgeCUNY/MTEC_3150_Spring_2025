using UnityEngine;

public class Sphere : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destroyer"))
        {
            //Destroy(gameObject);
            Debug.Log("Detected something. destroying now");
        }

        //if (other.CompareTag("Sphere"))
        //{
        //    Destroy(gameObject);
        //    Debug.Log("Spheerereee");
        //}

    }

}
