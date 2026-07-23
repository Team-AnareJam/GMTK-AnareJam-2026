using Cards.ObjectBehaviours;
using UnityEngine;

namespace Cards.CardLogics
{
    public class HeartseekerLogic : CardLogic
    {
        [SerializeField]private float Damage;
        [SerializeField]private float TimeToDie;
        [SerializeField]private float CritChance;
        [SerializeField]private float Speed;
        
        public override void Visualize()
        {
        }

        public override void Play()
        {
            var go = ContextManager.InstantiateObject(ctx.GetPrefab("Heartseeker"));
            var hs = go.GetComponent<Heartseeker>();
            hs.Init(Damage, TimeToDie, CritChance, Speed, ctx.AimingDirection);
        }
    }
}