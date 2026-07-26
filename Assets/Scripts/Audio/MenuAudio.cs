using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    public static MenuAudio Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            GetComponent<AudioSource>().Play();

        }
        else Destroy(this.gameObject);
    }
}
