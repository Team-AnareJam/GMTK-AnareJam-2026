using Cards.ObjectBehaviours;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Debuff", menuName = "CardLogic/Debuff")]
public class DebuffCardLogic : CardLogic
{
    [SerializeField] private List<DebuffData> debuffs;
    //[SerializeField] GameObject SwirlPrefab;
    public override void Visualize()
    {
        Debug.Log("Visualizing Commune");
        //visualize aoe
    }
    public override void Play()
    {
        //TODO: add to timer
        foreach (DebuffData data in debuffs)
        {
            var go = ContextManager.InstantiateObject(Prefab);
            var sw = go.GetComponent<Debuff>();
            sw.Init(ctx,data.debuff, data.Duration, data.Count);
        }
    }
}

[System.Serializable]
public class DebuffData
{
    public float Duration;
    public int Count;
    public EDebuff debuff;
}
