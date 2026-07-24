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
    public DamageInstance(IDamageable target, TargetType type, float damage)
    {
        Target = target;
        TType = type;
        Damage = damage;
    }

    //origin
    public IDamageable Target;
    public TargetType TType;
    public float Damage;
}

public enum TargetType
{
    Player,
    Enemy
}
