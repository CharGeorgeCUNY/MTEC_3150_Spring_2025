using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWaitThenFade : MonoBehaviour
{    
    public TMP_Text text;
    public UIWaitThenFade nextText;

    public float waitTime = 3.0f;
    public float fadeTime = 1f;

    public bool startText = true;
    
    void Start()
    {
        text = GetComponent<TMP_Text>();
        if (startText)
        {
            StartCoroutine(WaitThenFade());
        }
    }

    public void Hook()
    {
        StartCoroutine(FadeInText());
    }

    public IEnumerator FadeInText()
    {
        float currentTime = 0f;

        while (text.alpha < 255f)
        {
            text.alpha = Mathf.Lerp(text.alpha, 255, 0.5f);

            if (currentTime < 1)
            {
                currentTime += Time.deltaTime/fadeTime;
            }

            yield return null;
        }
        StartCoroutine(WaitThenFade());
    }

    public IEnumerator WaitThenFade()
    {
        yield return new WaitForSeconds(waitTime);

        float currentTime = 0f;

        while (text.alpha > 0f)
        {
            text.alpha = Mathf.Lerp(text.alpha, 0, currentTime);

            if (currentTime < 1)
            {
                currentTime += Time.deltaTime/fadeTime;
            }

            yield return null;
        }

        currentTime = 0f;

        if (nextText != null)
        {
            nextText.Hook();
        }
    }
}
