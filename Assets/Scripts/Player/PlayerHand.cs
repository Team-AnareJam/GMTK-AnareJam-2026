using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField]private List<Card> CardsInHand = new();
    public int HandSize;
    public float CardMoveTime;
    public GameObject CardPrefab;
    
    //private float Width => (gameObject.transform as RectTransform).
    
    public Card AddCard(Card newcard)
    {
        if (CardsInHand.Count >= HandSize)
        {
            return null;
        }
        CardsInHand.Add(newcard);
        Reposition();
        return newcard;
    }

    public void AddHand(params Card[] Cards)
    {
        foreach (var Card in Cards)
        {
            if (AddCard(Card) == null) return;
        }
    }

    public void Reposition()
    {
        
    }
}
