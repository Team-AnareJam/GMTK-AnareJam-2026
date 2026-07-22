using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Card", menuName = "Game Data/Card")]
[Serializable]
public class Card : ScriptableObject
{
    public string ID;
    public string Name;
    public float Cost;
    public Texture2D Art;
    public string Description;
    public Texture2D Background;
    public string Credits;
}