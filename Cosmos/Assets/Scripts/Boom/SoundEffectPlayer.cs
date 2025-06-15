using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        float volume = PlayerPrefs.GetFloat("SoundVolume", 0.8f);
        if (audioSource != null)
            audioSource.volume = volume;
    }

    public void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}