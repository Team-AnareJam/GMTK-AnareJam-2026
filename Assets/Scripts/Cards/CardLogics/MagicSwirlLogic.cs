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
        Debug.Log("Cast Magic Swirl");
        //cast aoe
    }

}
