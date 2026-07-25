using NaughtyAttributes;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    public ObjectPool<AudioSource> MusicPool;
    public GameObject MusicPrefab;
    public MusicLibrary MusicLib;
    public GameObject MusicPoolHolder;

    public int defaultPoolSize = 10;
    public int maxPoolSize = 20;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);

        InstantiatePool();
    }

    private void OnEnable()
    {
        TimerManager.OnStartTimer += PlayMusic;
    }

    private void OnDisable()
    {
        TimerManager.OnStartTimer -= PlayMusic;
    }

    public void InstantiatePool()
    {
        MusicPool = new ObjectPool<AudioSource>(
            createFunc: CreateMusic,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroyEnemy,
            collectionCheck: true,
            defaultCapacity: defaultPoolSize,
            maxSize: maxPoolSize
        );
    }

    #region SFX Pool Functions
    private AudioSource CreateMusic()
    {
        GameObject pooledObject = Instantiate(MusicPrefab);
        pooledObject.transform.parent = MusicPoolHolder.transform;
        AudioSource pooledSource = pooledObject.GetComponent<AudioSource>();
        pooledObject.SetActive(false);
        return pooledSource;
    }

    private void OnGet(AudioSource pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    private void OnRelease(AudioSource pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    private void OnDestroyEnemy(AudioSource pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }
    #endregion

    [Button]
    public void PlayMusic()
    {
        PlayMusic("TestMain");
    }

    public void PlayMusic(string name)
    {
        if (!MusicLib.MusicClips.Any(SFX => SFX.Name == name)) return;

        AudioSource musicObj = MusicPool.Get();
        MusicClip musicClip = MusicLib.MusicClips.First(SFX => SFX.Name == name);
        musicObj.clip = musicClip.Clip;
        musicObj.Play();
        musicObj.GetComponent<MusicHolder>().CurrentClip = musicClip;
    }
}

[System.Serializable]
public class MusicClip
{
    public string Name;
    public AudioClip Clip;
    public int BPM;
    public int BeatsPerBar;

    public float LoopStartTime;
    public float LoopEndTime;
    public float BeatLength => 60 / BPM;
    public float BarLength => BeatLength * BeatsPerBar;
    public float LoopLength => LoopEndTime - LoopStartTime;
}