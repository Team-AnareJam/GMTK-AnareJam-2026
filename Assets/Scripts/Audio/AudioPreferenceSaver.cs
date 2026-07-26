using UnityEngine;
using UnityEngine.Audio;

public class AudioPreferenceSaver : MonoBehaviour
{
    public AudioMixer Mixer;

    public void Awake()
    {
        Debug.Log(PlayerPrefs.GetFloat("BGM_VOLUME"));
        Mixer.SetFloat("BGM_VOLUME", Mathf.Log10(PlayerPrefs.GetFloat("BGM_VOLUME", 0.6f)));
        Mixer.SetFloat("SFX_VOLUME", Mathf.Log10(PlayerPrefs.GetFloat("SFX_VOLUME", 0.6f)));
    }

    private void Start()
    {
        SceneTransitionManager.Instance.GameData.CurrentLevel = 0;
    }
}
