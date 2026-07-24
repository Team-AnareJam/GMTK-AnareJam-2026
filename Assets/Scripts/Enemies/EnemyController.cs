using Enemies;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    public EnemyDataReference DataRef;
    [SerializeField]private EnemyData data;

    [SerializeField] EnemyMovement movement;
    [SerializeField] private Vector3 playerPosition;
    [SerializeField] private ETask currentTask;
    [SerializeField] private float remainingAttackTime;

    private Collider target;
    private float timestamp;

    public void Init()
    {
        data = DataRef.GetCopy();
    }
    public DamageInstance TakeDamage(DamageInstance instance)
    {
        data.HP -= (int)instance.Damage;
        if(data.HP < 0) instance.IsDead = true;
        return instance;
    }
    private void FixedUpdate()
    {
        if (data == null) return;
        if (data.IsMelee)
        {
            if (remainingAttackTime > 0) remainingAttackTime -= Time.fixedDeltaTime;
            if (remainingAttackTime <= 0)
            {
                if (target != null)
                {
                    TimerManager.Instance.UpdateTimer(data.AttackPower);
                    remainingAttackTime = data.MaxAttackTime;
                }
            }
        }
    }

    private void Update()
    {
        if (data == null) return;
        if (timestamp + data.TaskDelay <= Time.time)
        {
            timestamp = Time.time;
            ETask task = EvaluateTask();
            if (currentTask != task)
            {
                currentTask = task;
                PerformTask(currentTask);
            }
        }
    }
    private ETask EvaluateTask()
    {
        foreach(var task in data.Tasks)
        {
            if (CheckCondition(task))
            {
                return task.Task;
            }
        }
        return ETask.Idle;
    }

    private bool CheckCondition(TaskObject task)
    {
        switch (task.Condition)
        {
            case ECondition.FurtherThan:
                return (playerPosition - transform.position).magnitude > task.ConditionValue;
            case ECondition.CloserThan:
                return (playerPosition - transform.position).magnitude > task.ConditionValue;
        }
        return false;
    }

    private void PerformTask(ETask task)
    {
        switch (task)
        {
            case ETask.Idle:
                if(movement != null) movement.MovementTarget = transform.position;
                break;
            case ETask.Attack:
                Debug.Log("Attack!");
                break;
            case ETask.MoveCloser:
                movement.MovementTarget = playerPosition;
                break;
            case ETask.MoveFurther:
                Vector2 pos = (playerPosition - transform.position) * -1 * data.MovementDistance;
                movement.MovementTarget = pos;
                break;
            case ETask.Strafe:
                Vector2 strafePos = Vector2.Perpendicular((playerPosition - transform.position).normalized) 
                    * data.MovementDistance * (Random.Range(0, 2) == 0 ? 1 : -1);
                movement.MovementTarget = strafePos;
                break;
        }
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
