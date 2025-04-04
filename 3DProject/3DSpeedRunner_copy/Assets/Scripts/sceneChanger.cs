using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


public class sceneChanger : MonoBehaviour
{
    public string targetScene;
    public float delay = 10f;
    public bool automated = false;

    private void Start()
    {
        if (automated) { Invoke("changeSceneAuto", delay); }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 0.4f;
        StartCoroutine(swapScene(targetScene));
    }

    public void changeScene(string scene)
    {
        StartCoroutine(swapScene(scene));
    }

    void changeSceneAuto()
    {
        StartCoroutine(swapScene(targetScene));
    }

    public IEnumerator swapScene(string scene)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene);
    }
}
