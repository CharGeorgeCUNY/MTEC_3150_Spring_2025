using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            ChangeToGame();
        }
    }
    public void ChangeToGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
