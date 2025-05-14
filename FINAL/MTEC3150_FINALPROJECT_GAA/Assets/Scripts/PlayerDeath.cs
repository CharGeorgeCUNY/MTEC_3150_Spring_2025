using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Border"))
        {
            ResetScene();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Border"))
        {
            ResetScene();
        }
    }

    private void ResetScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
