using System;
using System.Collections;
using UnityEngine;

namespace WinterSym
{
    public class MusicPlayer : MonoBehaviour
    {
        public AudioSource audioSource;
        public float fadeTime = 0.5f;
       [Range(0, 1)] public float volume = 0.5f;

        public static MusicPlayer _MP;

        public MusicPlayer GetMusicPlayer()
        {
            if (_MP == null)
            {
                _MP = GameObject.FindFirstObjectByType<MusicPlayer>();
            }

            else if (_MP != this)
            {
                Destroy(gameObject);
            }

            return _MP;
        }

        void Awake()
        {
            GetMusicPlayer();
        }

        void Start()
        {
            audioSource.volume = volume;
        }


        public void FadeMusic()
        {
            StartCoroutine(FadeOut());
        }

        public IEnumerator FadeOut()
        {
            while (audioSource.volume > 0)
            {
                audioSource.volume -= volume * Time.deltaTime / fadeTime;
                yield return null;
            }

            StartCoroutine(WaitForMusic());
        }

        public IEnumerator FadeIn()
        {
            while (audioSource.volume < volume)
            {
                audioSource.volume += Time.deltaTime / fadeTime;
                yield return null;
            }

            audioSource.volume = 0.5f;
        }


        public IEnumerator WaitForMusic()
        {
            yield return new WaitForSeconds(PlayerSFX._SFX.dieClip.length);
            StartCoroutine(FadeIn());
        }
    }
}