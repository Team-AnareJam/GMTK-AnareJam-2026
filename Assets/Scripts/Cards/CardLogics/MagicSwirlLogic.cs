using Cards.ObjectBehaviours;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicSwirlLogic", menuName = "CardLogic/MagicSwirl")]
public class MagicSwirlLogic : CardLogic
{
    public float Duration = 5;
    public float Cooldown;
    public int Damage;
    public float Radius;
    //[SerializeField] GameObject SwirlPrefab;
    public override void Visualize()
    {
        Debug.Log("Visualizing Magic Swirl");
        //visualize aoe
    }
    public override void Play()
    {
        var go = ContextManager.InstantiateObject(Prefab);
        var sw = go.GetComponent<MagicSwirl>();
        sw.Init(ctx.MousePosInWorld, Duration, Cooldown, Damage, Radius);
    }

}
