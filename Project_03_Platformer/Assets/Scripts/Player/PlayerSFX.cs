using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public static PlayerSFX _SFX;
    public AudioSource myAudioSource;


    
    public AudioClip slipClip;
    public AudioClip dieClip;
    public AudioClip winClip;

    public PlayerSFX GetPlayerSFX()
    {
        if (_SFX == null)
        {
            _SFX = FindFirstObjectByType<PlayerSFX>();
        }

        else if (_SFX != this)
        {
            Destroy(gameObject);
        }

        return _SFX;
    }

    void Awake()
    {
        GetPlayerSFX();

        myAudioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlayAudioClip(AudioClip clip, float volume)
    {
        if (!myAudioSource.isPlaying)
        {
            myAudioSource.volume = volume;
            myAudioSource.PlayOneShot(clip);
        }
    }

}
