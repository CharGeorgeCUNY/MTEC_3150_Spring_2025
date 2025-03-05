using UnityEngine;
using UnityEngine.SceneManagement;

namespace PhysicsSym
{
    public class WinMenu : MonoBehaviour
    {
        public void RestartGame()
        {
            this.gameObject.GetComponent<Canvas>().enabled = false;
            CameraScript.SwitchCameras();
            SceneManager.LoadScene(1);
        }

        public void QuitGame()
        {
            AudioManager.GetAudioManager().Save();
            Application.Quit();
            Debug.Log("You quit the game!");
        }
    }

}
