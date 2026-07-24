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

    public void FuckUrDmg(DamageInstance instance)
    {
        instance.Damage = 0;
    }
}
