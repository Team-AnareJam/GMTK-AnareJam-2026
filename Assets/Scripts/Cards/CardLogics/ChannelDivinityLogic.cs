using UnityEngine;

public class ChannelDivinityLogic : CardLogic
{
    public float Radius;
    public override void Visualize()
    {
        throw new System.NotImplementedException();
    }

    public override void Play()
    {
        var hits = Physics.OverlapSphere(ctx.PlayerPosition, Radius);
        if (hits.Length > 1)
        {
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    var dmg = hit.GetComponent<IDamageable>();
                    DamageInstance instance = new DamageInstance(dmg, ETargetType.Enemy, 99999999);
                    DamageMediator.DealDamage(instance);
                }
            }
        }
    }
}