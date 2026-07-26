using Cards.ObjectBehaviours;
using UnityEngine;


public class BatStormLogic : CardLogic
{
    public float Count;
    public float Size;
    public float Damage;
    public float ExpansionRate;
    public float Duration;
    public float RotationSpeed;
    public float StartOffset;

    public override void Visualize()
    {
        throw new System.NotImplementedException();
    }

    public override void Play()
    {
        for (int i = 0; i < Count; i++)
        {
            var angle = Mathf.Lerp(0, 360, i / Count);
            var go = Instantiate(Prefab);
            var BS = go.GetComponent<BatStorm>();
            BS.Init(angle, Size, Damage, ExpansionRate, Duration, RotationSpeed, ctx.playerMovement.transform, StartOffset);
        }
    }
}