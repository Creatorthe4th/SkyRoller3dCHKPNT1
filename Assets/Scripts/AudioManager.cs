using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sources")]
    public AudioSource musicSource; // looping background music
    public AudioSource sfxSource;   // one-shot sound effects

    [Header("Clips")]
    public AudioClip deathClip;
    public AudioClip boostClip;
    public AudioClip buttonClip;
    public AudioClip iceClip;

    void Awake()
    {
        Instance = this;
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
            sfxSource.PlayOneShot(clip);
    }

    public void StopMusic()
    {
        if (musicSource != null)
            musicSource.Stop();
    }

    public void PlayDeath()  => PlaySFX(deathClip);
    public void PlayBoost()  => PlaySFX(boostClip);
    public void PlayButton() => PlaySFX(buttonClip);
    public void PlayIce()    => PlaySFX(iceClip);
}