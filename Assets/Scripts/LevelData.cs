using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/New Level")]
public class LevelData : ScriptableObject
{
    public Texture BackgroundTexture;
    public List<Wave> Waves;
    public bool SpawnBoss;
}
