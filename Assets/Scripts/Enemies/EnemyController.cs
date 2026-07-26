using Enemies;
using NaughtyAttributes;
using System.Collections.Generic;
using System.Data;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour, IDamageable
{
    public EnemyDataReference DataRef;
    [SerializeField] private EnemyData data;
    [SerializeField] private SpriteRenderer rend;

    [SerializeField] EnemyMovement movement;
    [SerializeField] private Vector3 playerPosition;
    [SerializeField] private ETask currentTask;
    [SerializeField] private float remainingAttackTime;
    private bool Initialized;

    private Collider target;
    private float timestamp;

    [Button]
    public void Init()
    {
        Init(DataRef);
    }

    public void Init(EnemyDataReference reference)
    {
        playerPosition = ContextManager.Instance.CardCtx.PlayerPosition;
        DataRef = reference;
        data = reference.GetCopy();
        rend.sprite = data.Sprite;
        movement.Init(data.MovementSpeed);
        currentTask = ETask.Idle;
        Initialized = true;
        Evaluate();
    }

    public DamageInstance TakeDamage(DamageInstance instance)
    {
        Debug.Log("takin damage");
        data.HP -= (int)instance.Damage;
        if (data.HP < 0) instance.IsDead = true;
        return instance;
    }

    private void FixedUpdate()
    {
        
        playerPosition = (Vector3)ContextManager.Instance.CardCtx.PlayerPosition + Constants.GetDepth();
        if (data == null || !Initialized) return;
        if (data.IsMelee)
        {
            if (remainingAttackTime > 0) remainingAttackTime -= Time.fixedDeltaTime;
            if (remainingAttackTime <= 0)
            {
                if (target != null)
                {
                    var instance =
                        new DamageInstance(TimerManager.Instance, this, ETargetType.Player, data.AttackPower);
                    DamageMediator.DealDamage(instance);
                }

                remainingAttackTime = data.MaxAttackTime;
            }
        }
    }

    private void Update()
    {
        LastAttack += Time.deltaTime;
        if (data == null || !Initialized) return;
        if (timestamp + data.TaskDelay <= Time.time)
        {
            Evaluate();
        }
    }

    private void Evaluate()
    {
        timestamp = Time.time;
        ETask task = EvaluateTask();

        currentTask = task;
        PerformTask(currentTask);
    }

    private ETask EvaluateTask()
    {
        foreach (var task in data.Tasks)
        {
            if (CheckCondition(task))
            {
                return task.Task;
            }
        }

        return ETask.Idle;
    }

    public float Distance;

    private bool CheckCondition(TaskObject task)
    {
        Distance = (playerPosition - transform.position).magnitude;
        return task.Condition switch
        {
            ECondition.FurtherThan => (playerPosition - transform.position).magnitude >= task.ConditionValue,
            ECondition.CloserThan => (playerPosition - transform.position).magnitude <= task.ConditionValue,
            _ => false
        };
    }

    [SerializeField]private float LastAttack;
    private void PerformTask(ETask task)
    {
        switch (task)
        {
            case ETask.Idle:
                if (movement != null) movement.MovementTarget = transform.position;
                break;
            case ETask.Attack:
                if (LastAttack > data.MaxAttackTime)
                {
                    DoAttack();
                    LastAttack = 0;
                }
                
                break;
            case ETask.MoveCloser:
                Debug.Log("MoveCloser!");
                Debug.Log(movement.MovementTarget + " = " + playerPosition);
                movement.MovementTarget = playerPosition;
                break;
            case ETask.MoveFurther:
                Debug.Log("MoveFart!");
                Vector2 pos = (playerPosition - transform.position) * (-1 * data.MovementDistance);
                movement.MovementTarget = pos;
                break;
            case ETask.Strafe:
                Debug.Log("Move... strafer?");
                Vector2 strafePos = Vector2.Perpendicular((playerPosition - transform.position).normalized) *
                                    (data.MovementDistance * (Random.Range(0, 2) == 0 ? 1 : -1));
                movement.MovementTarget = strafePos;
                break;
        }
    }

    private void DoAttack()
    {
        var go = Instantiate(data.ProjectilePrefab, transform.position, Quaternion.identity);
        var pj = go.GetComponent<EnemyProjectile>();
        Vector2 dir = (Vector3)ContextManager.Instance.CardCtx.PlayerPosition - transform.position;
        pj.Init(this, data.ProjectileSprite, dir, data.Scale, data.Speed, data.Lifetime, data.AttackPower);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = collision;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            target = null;
        }
    }
}

[System.Serializable]
public class TaskObject
{
    public ETask Task;
    public ECondition Condition;
    public float ConditionValue;
}

public enum ECondition
{
    FurtherThan,
    CloserThan,
}

public enum ETask
{
    Idle,
    Attack,
    MoveCloser,
    MoveFurther,
    Strafe
}