using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public GameObject EnemyPoolHolder;
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
        DamageMediator.OnDealDamageEnd += RemoveEnemyFromActiveWave;
    }

    private void OnDisable()
    {
        GameManager.OnStartGame -= StartWave;
        DamageMediator.OnDealDamageEnd -= RemoveEnemyFromActiveWave;
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
        GameObject pooledObject = Instantiate(EnemyPrefab);
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

    [Button]
    public void StartWave()
    {
        StartCoroutine(ProcessWave());
    }

    public IEnumerator ProcessWave()
    {
        CurrentWave = Waves[CurrentWaveIndex];

        int enemyIndex = 0;

        foreach (EnemyDataReference enemy in Waves[CurrentWaveIndex].Enemies)
        {
            EnemyController enemyController = EnemyPool.Get();
            enemyController.name = enemy.name;
            enemyController.Init(enemy);
            EnemyControllers.Add(enemyController);
            enemyController.gameObject.transform.parent = EnemyPoolHolder.transform;
            enemyController.gameObject.transform.position = new Vector3(0, 0, 10); //TODO: SPAWN POSITION
            enemyIndex++;
            yield return new WaitForSeconds(CurrentWave.SpawnDelay);
        }
    }

    public void EndWave()
    {
        //TODO: Open card reward
        //TODO: Handle starting the next wave
        //TODO: Handle logic for end of round
    }

    public void RemoveEnemyFromActiveWave(DamageInstance instance)
    {
        if (!instance.IsDead) return;
        if (instance.TType != ETargetType.Enemy) return;
        EnemyController controller = (EnemyController)instance.Target;
        EnemyControllers.Remove(controller);
        OnKillEnemy?.Invoke(controller);
        EnemyPool.Release(controller);
        if (EnemyControllers.Count == 0)
        {
            EndWave();
        }
    }
}
