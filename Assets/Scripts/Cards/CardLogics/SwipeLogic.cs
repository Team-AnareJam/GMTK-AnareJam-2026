using Cards.ObjectBehaviours;
using UnityEngine;

[CreateAssetMenu(fileName = "StrikeLogic", menuName = "CardLogic/Strike")]
public class SwipeLogic : CardLogic
{
    public float Distance;
    public float Duration;
    public float Damage;
    public float AngleWidth;
    public float Size;
    public override void Visualize()
    {
        
    }
    public override void Play()
    {
        var go = ContextManager.InstantiateObject(Prefab);
        var sw = go.GetComponent<Swipe>();
        sw.Init(ctx.MousePosInWorld, Distance, Duration, Damage, AngleWidth, Size);
    }
}
