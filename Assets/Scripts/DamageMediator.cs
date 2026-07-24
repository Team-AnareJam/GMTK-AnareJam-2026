using System;
using UnityEngine;

public static class DamageMediator
{
    public static event Action<DamageInstance> OnDealDamageStart;
    public static event Action<DamageInstance> OnDealDamageEnd;
    public static DamageInstance DealDamage(DamageInstance instance)
    {
        instance.damage = 5;
        Debug.Log("damage before event is " + instance.damage);
        OnDealDamageStart?.Invoke(instance);
        instance.target.TakeDamage(instance);
        OnDealDamageEnd?.Invoke(instance);

        return instance;
    }
}

[System.Serializable]
public class DamageInstance
{
    //origin
    public IDamageable target;
    public float damage;
}
