using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider Master;
    public Slider Music;
    public Slider Sounds;
    public TextMeshProUGUI MasterVolume;
    public TextMeshProUGUI MusicVolume;
    public TextMeshProUGUI SoundsVolume;


    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
        MasterVolume.text = ((int)((volume + 70) * 1.42858)).ToString();
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        MusicVolume.text = ((int)((volume + 70) * 1.25)).ToString();
    }

    public void SetSoundsVolume(float volume)
    {
        audioMixer.SetFloat("EffectsVolume", volume);
        SoundsVolume.text = ((int)((volume + 70) * 1.25)).ToString();
    }

    public void ContinueButtonClicked()
    {
        gameObject.SetActive(false);
    }

    public void LoadSettingsMenu()
    {
        float volume;

        audioMixer.GetFloat("MasterVolume", out volume);
        Master.SetValueWithoutNotify(volume);
        MasterVolume.text = ((int)((volume + 70) * 1.42858)).ToString();

        audioMixer.GetFloat("EffectsVolume", out volume);
        Sounds.SetValueWithoutNotify(volume);
        SoundsVolume.text = ((int)((volume + 70) * 1.25)).ToString();

        audioMixer.GetFloat("MusicVolume", out volume);
        Music.SetValueWithoutNotify(volume);
        MusicVolume.text = ((int)((volume + 70) * 1.25)).ToString();


        gameObject.SetActive(true);
    }
}
