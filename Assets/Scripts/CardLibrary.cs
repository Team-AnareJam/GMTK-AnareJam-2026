using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardLibrary", menuName = "Libraries/CardLibrary")]
public class CardLibrary : ScriptableObject
{
    [SerializeField] private List<Card> Cards;
}
