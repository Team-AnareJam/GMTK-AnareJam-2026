using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataReference", menuName = "Game Data/Enemy")]
public class EnemyDataReference : ScriptableObject
{
    public EnemyData Data;

    public EnemyData GetCopy() =>new EnemyData(Data);
}

[Serializable]
public class EnemyData
{
    public Sprite Sprite;
    public List<TaskObject> Tasks;
    public float MovementSpeed;
    public float MovementDistance;
    public float TaskDelay;

    
    public bool IsMelee;
    public int HP;
    public int AttackPower;
    public float MaxAttackTime;

    [Header("Projectile")]
    public GameObject ProjectilePrefab;
    public float Scale, Speed, Lifetime;
    public Sprite ProjectileSprite;

    public EnemyData(EnemyData data)
    {
        this.Sprite = data.Sprite;
        Tasks = new List<TaskObject>(data.Tasks);
        MovementSpeed = data.MovementSpeed;
        MovementDistance = data.MovementDistance;
        TaskDelay = data.TaskDelay; 
        IsMelee = data.IsMelee;
        HP = data.HP;
        AttackPower = data.AttackPower;
        MaxAttackTime = data.MaxAttackTime;
        
        ProjectilePrefab = data.ProjectilePrefab;
        Scale = data.Scale;
        Speed = data.Speed;
        Lifetime = data.Lifetime;
        ProjectileSprite = data.ProjectileSprite;
    }
}
