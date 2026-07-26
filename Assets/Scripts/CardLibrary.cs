using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "CardLibrary", menuName = "Libraries/CardLibrary")]
public class CardLibrary : ScriptableObject
{
    [SerializeField] private List<Card> Cards;

    [Button]
    public void RemoveDuplicates()
    {
        Cards = Cards.DistinctBy(card => card.ID).ToList();
    }

    [CanBeNull]
    public Card GetCard(string ID)
    {
        return Cards.FirstOrDefault(Card => Card.ID == ID);
    }

    //[ContextMenu("Load Library Cards")]
    //public void LoadLib()
    //{
    //    Cards.Clear();
    //    var cards = AssetDatabase.FindAssets($"t:{typeof(Card)}");
    //    foreach (var card in cards)
    //    {
    //        GUID.TryParse(card, out var result);
    //        Cards.Add(AssetDatabase.LoadAssetByGUID<Card>(result));
    //    }
    //}

    /// <summary>
/// Returns random card from a specified rarity
/// </summary>
/// <param name="rar"></param>
/// <returns></returns>
    public Card GetCardByRarity(CardRarity rar)
    {
        return Cards
            .Where(card => card.cardRarity == rar)
            .OrderBy(_ => Random.value)
            .First();
    }
/// <summary>
/// Returns random card from a specified type
/// </summary>
/// <param name="typ"></param>
/// <returns></returns>
    public Card GetCardByType(CardType typ)
    {
        return Cards
            .Where(card => card.cardType == typ)
            .OrderBy(_ => Random.value)
            .First();
    }
}
