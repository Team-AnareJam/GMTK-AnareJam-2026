using Cards.ObjectBehaviours;

public class BreadCrumbsLogic : CardLogic
{
    public float Duration;
    public float CrumbDuration;
    public float CrumbSize;
    public float Damage;
    public float FrequencyPerSecond;
    public float CrumbDamageFrequency;

    public override void Visualize()
    {
        throw new System.NotImplementedException();
    }

    public override void Play()
    {
        var go = ContextManager.InstantiateObject(Prefab);
        var bc = go.GetComponent<BreadCrumbling>();
        bc.Init(Duration, CrumbDuration, CrumbSize, Damage, FrequencyPerSecond, CrumbDamageFrequency);
    }
}