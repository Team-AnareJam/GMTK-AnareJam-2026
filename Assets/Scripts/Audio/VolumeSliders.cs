using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class VolumeSliders : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private void Awake()
    {
        bgmSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        bgmSlider.value = PlayerPrefs.GetFloat("BGM_VOLUME", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFX_VOLUME", 1f);
    }

    public void SetMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("BGM_VOLUME", value);
        mixer.SetFloat("BGM_VOLUME", Mathf.Log10(value) * 20);
    }

    public void SetSFXVolume(float value)
    {
        PlayerPrefs.SetFloat("SFX_VOLUME", value);
        mixer.SetFloat("SFX_VOLUME", Mathf.Log10(value) * 20);
    }
}
