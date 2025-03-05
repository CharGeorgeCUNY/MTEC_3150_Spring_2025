using UnityEngine;
using UnityEngine.UI;

namespace PhysicsSym
{
    public class AudioManager : MonoBehaviour
    {
        private Slider volumeSlider;

        [SerializeField]
        private Sprite volumeOff;

        [SerializeField]
        private Sprite volumeOn;

        [SerializeField]
        private Button volumeButton;

        public static AudioManager audioManager;

        private bool isMuted = false;

        public Canvas myCanvas;
        public AudioSource bgmAudioSource;

        //audio manager singleton
        public static AudioManager GetAudioManager()
        {
            if (audioManager == null)
            {
                audioManager = GameObject.FindFirstObjectByType<AudioManager>();
            }
            return audioManager;
        }

        void Start()
        {
            //make sure to destroy extra audio sources/controllers
            if (audioManager != null)
            {
                Destroy(bgmAudioSource.gameObject);
                Destroy(myCanvas.gameObject);
            }
            GetAudioManager();

            volumeButton.GetComponent<Image>().sprite = volumeOn;

            volumeSlider = gameObject.GetComponent<Slider>();

            if (!PlayerPrefs.HasKey("musicVolume"))
            {
                PlayerPrefs.SetFloat("musicVolume", 1);
            }

            Load();

            Debug.Log("Audio Manager initialized.");
        }

        public void MuteUnmute()
        {
            if (isMuted)
            {
                isMuted = false;
                volumeButton.GetComponent<Image>().sprite = volumeOn;
                Load();
            }

            else
            {
                isMuted = true;
                volumeButton.GetComponent<Image>().sprite = volumeOff;
                volumeSlider.value = 0;
                AudioListener.volume = 0;
            }
        }

        //set the volume to x% from the volumeslider's value
        public void ChangeVolume()
        {
            AudioListener.volume = volumeSlider.value;

            if (!isMuted)
            {
                Save();
            }
            
            CheckIcon();

        }

        //load audio level preference from file
        private void Load()
        {
            volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
            CheckIcon();
        }

        //save audio level preference from file
        public void Save()
        {
            PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        }

        private void CheckIcon()
        {
            if (AudioListener.volume != 0)
            {
                isMuted = false;
                volumeButton.GetComponent<Image>().sprite = volumeOn;
            }

            else if (AudioListener.volume == 0)
            {
                isMuted = true;
                volumeButton.GetComponent<Image>().sprite = volumeOff;
            }

            else
            {
                Debug.Log("You picked a weird number and I don't know what to do with it.");
            }
        }
    }
}

