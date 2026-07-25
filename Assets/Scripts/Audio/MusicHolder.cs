using UnityEngine;

public class MusicHolder : MonoBehaviour
{
    public MusicClip CurrentClip;
    public AudioSource Source;
    public bool Loop = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Loop && Source.time >= CurrentClip.LoopEndTime)
        {
            Source.time -= CurrentClip.LoopLength;
            Debug.Log("Looped");
        }
    }
}
