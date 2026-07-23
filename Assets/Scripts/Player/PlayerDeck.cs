using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    [SerializeField] private List<Card> cards;

    public List<Card> GetCopy()
    {
        return new List<Card>(cards);
    }

    [SerializeField] private string newCardID;
    [Button]
    public void AddCardByID()
    {
        //cards.Add()
    }
}
