using UnityEngine;
using UnityEngine.SceneManagement;

public class next : MonoBehaviour
{

    [SerializeField] private string fourLevel = "Scene4-AppleEater";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onButtonClick()
    {
        SceneManager.LoadScene(fourLevel);

    }
}
