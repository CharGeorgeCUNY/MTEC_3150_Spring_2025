using System.Collections;
using UnityEngine;

namespace CatSym
{
    public class PlaySoundOnCollision : MonoBehaviour
    {
        public AudioClip sfxClip;
        public float volume = 0.5f;

        private bool isPlaying;
        [SerializeField] private float buffer = 1f;

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject != GameManager.player)
            {
                if (!isPlaying)
                {
                    Debug.Log("PlaySoundOnCollision: " + "Playing clip " + sfxClip.name);
                    isPlaying = true;
                    SFXManager._SFX.PlaySFXClip(sfxClip, transform, volume);
                    Invoke(nameof(SetPlayingToFalse), sfxClip.length + buffer);
                }
            }
        }

        private void SetPlayingToFalse()
        {
            isPlaying = false;
        }
    }
}