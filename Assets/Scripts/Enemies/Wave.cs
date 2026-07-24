using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Game Data/Wave")]
public class Wave : ScriptableObject
{
    public string Name;
    public List<EnemyDataReference> Enemies;
}