using UnityEngine;
using UnityEngine.SceneManagement;

namespace PhysicsSym
{
    public class PauseMenu : MonoBehaviour
    {
        public Canvas pauseCanvas;

        public void Pause()
        {
            pauseCanvas.enabled = true;
            Time.timeScale = 0;
        }

        public void Resume()
        {
            pauseCanvas.enabled = false;
            Time.timeScale = 1;
        }

        public void GoHome()
        {
            this.gameObject.GetComponent<Canvas>().enabled = false;
            CameraScript.SwitchCameras();
            SceneManager.LoadScene(0);
        }

        public void QuitGame()
        {
            Application.Quit();
            Debug.Log("You quit the game!");
        }

    }
}
