using System;
using UnityEngine;

public static class DamageMediator
{
    public static event Action<DamageInstance> OnDealDamageStart;
    public static event Action<DamageInstance> OnDealDamageEnd;
    public static DamageInstance DealDamage(DamageInstance instance)
    {
        instance.Damage = 5;
        Debug.Log("damage before event is " + instance.Damage);
        OnDealDamageStart?.Invoke(instance);
        instance.Target.TakeDamage(instance);
        OnDealDamageEnd?.Invoke(instance);

        return instance;
    }
}

[System.Serializable]
public class DamageInstance
{
    public DamageInstance(IDamageable target, ETargetType type, float damage)
    {
        Target = target;
        TType = type;
        Damage = damage;
        IsDead = false;
    }

    //origin
    public IDamageable Target;
    public ETargetType TType;
    public float Damage;
    public bool IsDead = false;
}

public enum ETargetType
{
    Player,
    Enemy
}
