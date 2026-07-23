using UnityEngine;

public abstract class CardLogic : ScriptableObject
{
    public static CardContext ctx;
    public abstract void Visualize();
    public abstract void Play();
}