using Cards.ObjectBehaviours;
using UnityEngine;


    public class HeartseekerLogic : CardLogic
    {
        [SerializeField]private float Damage;
        [SerializeField]private float Duration;
        [SerializeField]private float CritChance;
        [SerializeField]private float Speed;
        [SerializeField] private float Size = 1;
        [SerializeField] private GameObject prefab; 
        public override void Visualize()
        {
        }

        public override void Play()
        {
            var go = ContextManager.InstantiateObject(prefab);
            var hs = go.GetComponent<Heartseeker>();
            hs.Init(Damage, Duration, CritChance, Speed, ctx.AimingDirection, Size, ctx);
        }
    }