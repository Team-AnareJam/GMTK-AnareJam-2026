using UnityEngine;

public abstract class CardLogic : ScriptableObject
{
    public static CardContext ctx;
    [SerializeField] protected GameObject Prefab;
    public int Cost;
    public abstract void Visualize();
    public abstract void Play();
}