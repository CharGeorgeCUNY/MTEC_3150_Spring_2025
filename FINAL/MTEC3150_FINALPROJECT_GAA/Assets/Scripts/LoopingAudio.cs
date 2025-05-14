using UnityEngine;

public class LoopingAudio : MonoBehaviour
{
    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.loop = true;
        audio.Play();
    }
}
