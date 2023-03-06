using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SoundManager : MonoBehaviour
{
    public enum Sounds
    {
        Bonus, Win , Destroy, Music

    }
    public AudioClip bonusAudio;
    public AudioClip winAudio;
    public AudioClip destroyAudio;
    public AudioClip musicAudio;
    public AudioClip start321;
    AudioSource audioSource;
    
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(start321);
    }

    public void PlaySound(Sounds sound)
    {
        switch (sound)
        {
            case Sounds.Bonus:
                audioSource.loop = true;
                audioSource.PlayOneShot(bonusAudio);
                break;

            case Sounds.Win:
                audioSource.loop = true;
                audioSource.PlayOneShot(winAudio);
                audioSource.volume = 0.5f;
                break;

            case Sounds.Destroy:
                audioSource.loop = true;
                audioSource.PlayOneShot(destroyAudio);
                break;

            case Sounds.Music:
                audioSource.PlayOneShot(musicAudio);
                audioSource.loop = true;
                audioSource.volume = 0.1f;
                break;

            default:
                break;
        }
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
   
}
