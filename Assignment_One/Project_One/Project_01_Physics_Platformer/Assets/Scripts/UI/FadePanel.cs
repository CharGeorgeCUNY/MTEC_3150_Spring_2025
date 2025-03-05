using System.Collections;
using UnityEngine;

namespace PhysicsSym
{
    public class FadePanel : MonoBehaviour
    {
        public static FadePanel fadePanel;
        //fade panel singleton
        public static FadePanel GetAudioManager()
        {
            if (fadePanel == null)
            {
                fadePanel = GameObject.FindFirstObjectByType<FadePanel>();
            }

            return fadePanel;
        }

        void Start()
        {
            if (fadePanel != null)
            {
                Destroy(gameObject);
            }
        }

        public void FadeOutFadeIn()
        {
            StartCoroutine("WaitThenFadeIn");
            GetComponent<Animator>().Play("Fade_Out");
        }

        private IEnumerator WaitThenFadeIn()
        {
            yield return new WaitForSecondsRealtime(2f);
            GetComponent<Animator>().Play("Fade_In");
        }
    }
}
