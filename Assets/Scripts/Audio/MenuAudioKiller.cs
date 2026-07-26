using UnityEngine;

public class MenuAudioKiller : MonoBehaviour
{
    public void KillAudio()
    {
        GameObject menuMusicObj = GameObject.Find("MenuMusic");
        menuMusicObj.GetComponent<AudioSource>().Pause();
        Destroy(menuMusicObj);
    }
}
