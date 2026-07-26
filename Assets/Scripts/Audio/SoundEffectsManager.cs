using NaughtyAttributes;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager Instance;
    public ObjectPool<AudioSource> SFXPool;
    public GameObject SFXPrefab;
    public SFXLibrary SFXLib;
    public GameObject SFXPoolHolder;

    public int defaultPoolSize = 10;
    public int maxPoolSize = 20;

    public float TimeOfLastDamageSFX;
    public float MaxTimeBetweenDamageSFX = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);

        InstantiatePool();
    }

    public void InstantiatePool()
    {
        TimeOfLastDamageSFX = Time.time;
        SFXPool = new ObjectPool<AudioSource>(
            createFunc: CreateSFX,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroySound,
            collectionCheck: true,
            defaultCapacity: defaultPoolSize,
            maxSize: maxPoolSize
        );
    }

    #region SFX Pool Functions
    private AudioSource CreateSFX()
    {
        GameObject pooledObject = Instantiate(SFXPrefab);
        pooledObject.transform.parent = SFXPoolHolder.transform;
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

    private void OnDestroySound(AudioSource pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }
    #endregion

    [Button]
    public void PlaySound()
    {
        PlaySound("test");
    }

    public void PlaySound(string name)
    {
        if (!SFXLib.SoundEffects.Any(SFX => SFX.Name == name)) return;
        if (name == "TimeDown" && (Time.time - TimeOfLastDamageSFX < MaxTimeBetweenDamageSFX)) return;

        AudioSource sfxObj = SFXPool.Get();
        SoundEffect effect = SFXLib.SoundEffects.First(SFX => SFX.Name == name);
        sfxObj.clip = effect.Clip;
        sfxObj.pitch = UnityEngine.Random.Range(effect.MinPitch, effect.MaxPitch);
        sfxObj.Play();
        StartCoroutine(ReleaseSound(sfxObj, 1 + sfxObj.clip.length / sfxObj.pitch));
    }

    public IEnumerator ReleaseSound(AudioSource sfxObj, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SFXPool.Release(sfxObj);
    }
}

[System.Serializable]
public class SoundEffect
{
    public string Name;
    public AudioClip Clip;
    [Range(0.25f, 3)] public float MinPitch;
    [Range(0.25f, 3)] public float MaxPitch;
}