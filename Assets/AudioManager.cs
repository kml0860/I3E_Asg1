// File: Assets/Scripts/AudioManager.cs

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    [Header("Clips")]
    public AudioClip coinClip;
    public AudioClip keyClip;
    public AudioClip doorClip;
    public AudioClip lockedClip;
    public AudioClip landmineClip;
    public AudioClip healClip;
    public AudioClip winClip;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public void StopMusic() => musicSource.Stop();
}
