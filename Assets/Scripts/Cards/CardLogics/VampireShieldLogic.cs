using Cards.ObjectBehaviours;

namespace Cards.CardLogics
{
    public class VampireShieldLogic : CardLogic
    {
        public int Hits;
        public float TimeRecovered;
        public float Duration;
        public override void Visualize()
        {
            
        }

        public override void Play()
        {
            var go = ContextManager.InstantiateObject(Prefab);
            var vs = go.GetComponent<VampireShield>();
            vs.Init(Hits, TimeRecovered, Duration);
        }
    }
}