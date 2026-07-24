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

    public void InstantiatePool()
    {
        SFXPool = new ObjectPool<AudioSource>(
            createFunc: CreateSFX,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroyEnemy,
            collectionCheck: true,
            defaultCapacity: defaultPoolSize,
            maxSize: maxPoolSize
        );
    }

    #region SFX Pool Functions
    private AudioSource CreateSFX()
    {
        GameObject pooledObject = Instantiate(SFXPrefab);
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
    public void PlaySound()
    {
        PlaySound("test");
    }

    public void PlaySound(string name)
    {
        if (!SFXLib.SoundEffects.Any(SFX => SFX.Name == name)) return;
        AudioSource sfxObj = SFXPool.Get();
        SoundEffect effect = SFXLib.SoundEffects.First(SFX => SFX.Name == name);
        sfxObj.clip = effect.Clip;
        sfxObj.pitch = UnityEngine.Random.Range(effect.MinPitch, effect.MaxPitch);
        StartCoroutine(ReleaseSound(sfxObj, 1 + sfxObj.clip.length / sfxObj.pitch));
    }

    public IEnumerator ReleaseSound(AudioSource sfxObj, float duration)
    {
        yield return new WaitForSeconds(duration);
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