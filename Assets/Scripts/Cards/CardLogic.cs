using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public abstract class CardLogic : ScriptableObject
{
    public static CardContext ctx;
    [SerializeField] protected GameObject Prefab;
    public int Cost;
    public abstract void Visualize();
    public abstract void Play();

    [ContextMenu("CardLogic/Generate Logics")]
    private void Generate()
    {
        var classes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
            .Where(cs => typeof(CardLogic).IsAssignableFrom(cs) && cs != typeof(CardLogic) && !cs.IsAbstract);

        foreach (var cs in classes)
        {
            var cl = CreateInstance(cs);
            AssetDatabase.CreateAsset(cl, $"Assets/Data/CardLogics/{cs}.asset");
        }
    }
}