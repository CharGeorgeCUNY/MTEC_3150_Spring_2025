using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonUI : MonoBehaviour
{
    [SerializeField] private string lastGameLevel = "ending";
    public void EndButton()
    {
        SceneManager.LoadScene(lastGameLevel);
    }
}
