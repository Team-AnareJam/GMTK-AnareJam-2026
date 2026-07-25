using UnityEngine;

public class MusicHolder : MonoBehaviour
{
    public MusicClip CurrentClip;
    public AudioSource Source;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
