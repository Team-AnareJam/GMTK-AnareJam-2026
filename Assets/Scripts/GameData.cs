using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/New Game")]
public class GameData : ScriptableObject
{
    public List<LevelData> Levels;
    public int CurrentLevel = 0;
}
