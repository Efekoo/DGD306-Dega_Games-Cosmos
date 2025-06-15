using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundSlider;
    public Slider brightnessSlider;

    public Image brightnessOverlay;

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.8f);
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 0.8f);
        brightnessSlider.value = PlayerPrefs.GetFloat("Brightness", 1f);

        ApplyBrightness(brightnessSlider.value);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        soundSlider.onValueChanged.AddListener(SetSoundVolume);
        brightnessSlider.onValueChanged.AddListener(ApplyBrightness);
    }

    public void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.SetVolume(value);
        }
    }

    public void SetSoundVolume(float value)
    {
        PlayerPrefs.SetFloat("SoundVolume", value);
    }

    public void ApplyBrightness(float value)
    {
        if (brightnessOverlay != null)
            brightnessOverlay.color = new Color(0, 0, 0, 1f - value);

        PlayerPrefs.SetFloat("Brightness", value);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}