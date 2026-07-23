using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] EnemyMovement movement;
    [SerializeField] private Vector3 playerPosition;
    [SerializeField] private List<TaskObject> tasks;
    [SerializeField] private ETask currentTask;
    [SerializeField] private float movementDistance;
    [SerializeField] private float taskDelay;
    private float timestamp;

    private void Update()
    {
        if (timestamp + taskDelay <= Time.time)
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
        foreach(var task in tasks)
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
                movement.MovementTarget = transform.position;
                break;
            case ETask.Attack:
                Debug.Log("Attack!");
                break;
            case ETask.MoveCloser:
                movement.MovementTarget = playerPosition;
                break;
            case ETask.MoveFurther:
                Vector2 pos = (playerPosition - transform.position) * -1 * movementDistance;
                movement.MovementTarget = pos;
                break;
            case ETask.Strafe:
                Vector2 strafePos = Vector2.Perpendicular((playerPosition - transform.position).normalized) 
                    * movementDistance * (Random.Range(0, 2) == 0 ? 1 : -1);
                movement.MovementTarget = strafePos;
                break;
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
