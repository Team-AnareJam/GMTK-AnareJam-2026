using NaughtyAttributes;
using UnityEngine;

public class DamageTest : MonoBehaviour
{

    private void OnEnable()
    {
        DamageMediator.OnDealDamageStart += FuckUrDmg;
    }
    private void OnDisable()
    {
        DamageMediator.OnDealDamageStart += FuckUrDmg;
    }

    [Button]
    public void TestDamage()
    {
        DamageInstance dmg = new DamageInstance()
        {
            target = TimerManager.Instance
        };
        DamageMediator.DealDamage(dmg);
    }

    public void FuckUrDmg(DamageInstance instance)
    {
        instance.damage = 0;
    }
}
