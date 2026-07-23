using Cards.ObjectBehaviours;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicSwirlLogic", menuName = "CardLogic/MagicSwirl")]
public class MagicSwirlLogic : CardLogic
{
    public float TimeToDie = 5;
    public float Cooldown;
    public float Damage;
    public float Radius;
    public override void Visualize()
    {
        Debug.Log("Visualizing Magic Swirl");
        //visualize aoe
    }
    public override void Play()
    {
        var go = ContextManager.InstantiateObject(ctx.GetPrefab("magic_swirl"));
        var sw = go.GetComponent<MagicSwirl>();
        sw.Init(ctx.MousePosInWorld, TimeToDie, Cooldown, Damage, Radius);
    }

}
