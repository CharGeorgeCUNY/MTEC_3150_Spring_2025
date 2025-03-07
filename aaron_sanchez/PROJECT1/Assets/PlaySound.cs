using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource

    void Start()
    {
        // Play the audio when the game starts
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource is not assigned!");
        }
    }
}