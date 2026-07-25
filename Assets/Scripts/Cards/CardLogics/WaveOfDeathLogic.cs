using Cards.ObjectBehaviours;

public class WaveOfDeathLogic : CardLogic
{
    public float Duration;
    public float Size;
    public float Damage;
    public float Speed;
    public float AddedTime;
    
    public override void Visualize()
    {
        throw new System.NotImplementedException();
    }

    public override void Play()
    {
        var go = ContextManager.InstantiateObject(Prefab);
        var wod = go.GetComponent<WaveOfDeath>();
        wod.Init(Duration, Size, Damage, Speed, AddedTime, ctx.AimingDirection);
    }
}