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

        float bgmValue;
        mixer.GetFloat("BGM_VOLUME", out bgmValue);
        bgmSlider.value = (float)Math.Pow(10, bgmValue / 20f) ;

        float sfxValue;
        mixer.GetFloat("BGM_VOLUME", out sfxValue);
        sfxSlider.value = (float)Math.Pow(10, sfxValue / 20f) ;
    }

    public void SetMusicVolume(float value)
    {
        mixer.SetFloat("BGM_VOLUME", Mathf.Log10(value) * 20);
    }

    public void SetSFXVolume(float value)
    {
        mixer.SetFloat("SFX_VOLUME", Mathf.Log10(value) * 20);
    }
}
