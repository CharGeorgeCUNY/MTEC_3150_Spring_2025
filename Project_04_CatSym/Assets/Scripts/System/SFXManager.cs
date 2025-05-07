using UnityEngine;

namespace CatSym
{
    public class SFXManager : MonoBehaviour
    {
        //components
        public static SFXManager _SFX;

        [SerializeField] private AudioSource sfxObject;

        void Awake()
        {
            if (_SFX == null)
            {
                _SFX = this;
            }

            else
            {
                Destroy(gameObject);
            }
        }

        public void PlaySFXClip(AudioClip audioClip, Transform spawn, float volume = 1f)
        {
            AudioSource audioSource = Instantiate(sfxObject, spawn);

            audioSource.clip = audioClip;

            audioSource.volume = volume;

            audioSource.Play();

            Destroy(audioSource.gameObject, audioSource.clip.length);
        }
    }
}