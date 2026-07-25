using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public abstract class CardLogic : ScriptableObject
{
    public static CardContext ctx;
    [SerializeField] protected GameObject Prefab;
    public abstract void Visualize();
    public abstract void Play();

    [ContextMenu("Generate Logics")]
    protected void Generate()
    {
        var classes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
            .Where(cs => typeof(CardLogic).IsAssignableFrom(cs) && cs != typeof(CardLogic) && !cs.IsAbstract);

        string asset = "";
        foreach (var cs in classes)
        {
            if (AssetDatabase.FindAssets($"t:{cs}").Length > 0)
            {
                asset += $"Found {cs}\n";
                continue;
            }

            asset += $"Did not find {cs}, Creating...\n";
            var cl = CreateInstance(cs);
            AssetDatabase.CreateAsset(cl, $"Assets/Data/CardLogics/{cs}.asset");
        }
        Debug.Log(asset);
    }
}