using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;
    public static event Action OnWaveEnd;
    public static event Action<EnemyController> OnKillEnemy;

    public List<Wave> Waves;
    public Wave CurrentWave;
    public int CurrentWaveIndex = 0;

    public List<EnemyController> EnemyControllers;
    public ObjectPool<EnemyController> EnemyPool;
    public GameObject EnemyPrefab;

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
        EnemyPool = new ObjectPool<EnemyController>(
            createFunc: CreateEnemy,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroyEnemy,
            collectionCheck: true,
            defaultCapacity: 50,
            maxSize: 200
        );
    }

    #region Start/Update/Enable/Disable
    private void OnEnable()
    {
        GameManager.OnStartGame += StartWave;
    }

    private void OnDisable()
    {
        GameManager.OnStartGame -= StartWave;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion


    #region Enemy Pool Functions
    private EnemyController CreateEnemy()
    {
        GameObject pooledObject = EnemyPrefab;
        EnemyController controller = pooledObject.GetComponent<EnemyController>();
        pooledObject.SetActive(false);
        return controller;
    }

    private void OnGet(EnemyController pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    private void OnRelease(EnemyController pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    private void OnDestroyEnemy(EnemyController pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    #endregion

    public void StartWave()
    {
        CurrentWave = Waves[CurrentWaveIndex];

        int enemyIndex = 0;

        foreach (EnemyDataReference enemy in Waves[CurrentWaveIndex].Enemies)
        {
            EnemyController enemyController = EnemyPool.Get();
            enemyController.DataRef = enemy;
            enemyController.Init();
            EnemyControllers.Add(enemyController);
            enemyIndex++;
        }
    }

    public void EndWave()
    {
        //TODO: Handle starting the next wave
        //TODO: Handle logic for end of round
    }


    public void RemoveEnemyFromActiveWave(EnemyController controller)
    {
        EnemyControllers.Remove(controller);
        OnKillEnemy?.Invoke(controller);
        EnemyPool.Release(controller);
        if (EnemyControllers.Count == 0)
        {
            EndWave();
        }
    }
}
