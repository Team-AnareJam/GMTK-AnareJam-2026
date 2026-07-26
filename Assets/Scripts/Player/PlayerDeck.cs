using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDeck", menuName = "CardLogic/Create Deck")]
public class PlayerDeck : ScriptableObject
{
    [SerializeField] private List<Card> Deck;

    public List<Card> GetCopy()
    {
        return new List<Card>(Deck);
    }

    public void AddCard(Card card)
    {
        Deck.Add(card);
    }
}
