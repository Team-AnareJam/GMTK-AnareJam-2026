using UnityEngine;

[CreateAssetMenu(fileName = "MagicSwirlLogic", menuName = "CardLogic/MagicSwirl")]
public class MagicSwirlLogic : CardLogic
{
    public override void Visualize()
    {
        Debug.Log("Visualizing Magic Swirl");
        //visualize aoe
    }
    public override void Play()
    {
        ContextManager.InstantiateObject(ctx.GetPrefab("magic_swirl"));
    }

}
