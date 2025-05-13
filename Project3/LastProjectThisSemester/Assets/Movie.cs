using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movie : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(movieplay());
    }

    private IEnumerator movieplay()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(1,LoadSceneMode.Single);
    }

    // Update is called once per frame
    
}
