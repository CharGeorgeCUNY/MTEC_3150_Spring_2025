using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace PhysicsSym
{
    public class MenuOptions : MonoBehaviour
    {
        public Canvas pauseCanvas;

        private PersistenceFlag[] FlaggedObjects;

        // private GameObject fadePanel;

        // void Start()
        // {
        //     fadePanel = GameObject.Find("FadePanel");
        // }

        public void Pause()
        {
            //if there is a pause canvas in this scene
            if (pauseCanvas != null)
            {
                //and you haven't already pressed pause
                if (pauseCanvas.enabled == false)
                {
                    pauseCanvas.enabled = true;
                }
            }

            Time.timeScale = 0;
        }

        public void Resume()
        {
            if (pauseCanvas != null)
            {
                pauseCanvas.enabled = false;
            }
            Time.timeScale = 1;
        }

        public void NextScene()
        {
            // fadePanel.GetComponent<FadePanel>().FadeOutFadeIn();
            // StartCoroutine("NextSceneCoroutine");
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (SceneManager.GetSceneByBuildIndex(nextSceneIndex) != null)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }

            else
            {
                Debug.Log("Next scene is null!");
            }
        }

        // private IEnumerator NextSceneCoroutine()
        // {
        //     yield return new WaitForSecondsRealtime(1f);
        //     int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        //     if (SceneManager.GetSceneByBuildIndex(nextSceneIndex) != null)
        //     {
        //         SceneManager.LoadScene(nextSceneIndex);
        //     }

        //     else
        //     {
        //         Debug.Log("Next scene is null!");
        //     }
        // }

        public void GoHome()
        {
            //fadePanel.GetComponent<FadePanel>().FadeOutFadeIn();
            // StartCoroutine("GoHomeCoroutine");
            Resume();
            gameObject.GetComponent<Canvas>().enabled = false;
            CameraScript.SwitchCameras();
            SceneManager.LoadScene(0);
            StartCoroutine("GoHomeCoroutine");
        }

        private IEnumerator GoHomeCoroutine()
        {
            yield return new WaitForSecondsRealtime(1f);
            CleanFlaggedObjects();
        }

        // private IEnumerator GoHomeCoroutine()
        // {
        //     yield return new WaitForSecondsRealtime(1f);
        //     Resume();
        //     gameObject.GetComponent<Canvas>().enabled = false;
        //     CameraScript.SwitchCameras();
        //     CleanFlaggedObjects();
        //     SceneManager.LoadScene(0);
        // }

        

        public void QuitGame()
        {
            if (AudioManager.GetAudioManager() != null)
            {
                AudioManager.GetAudioManager().Save();
            }
            
            Application.Quit();
            Debug.Log("You quit the game!");
        }

        private void CleanFlaggedObjects()
        {
            FlaggedObjects = FindObjectsByType<PersistenceFlag>(FindObjectsSortMode.None);

            for (int i = 0; i < FlaggedObjects.Length; i++)
            {
                FlaggedObjects[i].CleanUp();
            }
        }

    }
}
