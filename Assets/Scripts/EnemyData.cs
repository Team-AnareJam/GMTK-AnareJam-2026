using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game Data/Enemy")]
public class EnemyDataReference : ScriptableObject
{
    public EnemyData Data;

    public EnemyData GetCopy()
    {
        return new EnemyData(Data);
    }
}

[Serializable]
public class EnemyData
{
    [SerializeField] public List<TaskObject> Tasks;
    [SerializeField] public float MovementDistance;
    [SerializeField] public float TaskDelay;

    
    [SerializeField] public bool IsMelee;
    [SerializeField] public int HP;
    [SerializeField] public int AttackPower;
    [SerializeField] public float MaxAttackTime;

    public EnemyData(EnemyData data)
    {
        Tasks = new List<TaskObject>(data.Tasks);
        MovementDistance = data.MovementDistance;
        TaskDelay = data.TaskDelay; 
        IsMelee = data.IsMelee;
        HP = data.HP;
        AttackPower = data.AttackPower;
        MaxAttackTime = data.MaxAttackTime;
    }
}
