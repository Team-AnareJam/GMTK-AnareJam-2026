using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    private PlayerDeck deck;
    [SerializeField] private List<CardHolder> CardsInHand = new();
    [SerializeField] private List<Card> DrawPile = new();
    [SerializeField] private List<Card> DiscardPile = new();
    [SerializeField] private List<Card> GraveyardPile = new();
    public int HandSize;
    public GameObject CardPrefab;
    public RectTransform Transform;
    private float Width => Transform.rect.width;
    private Vector2 Center => Transform.rect.center;
    private float LeftSide => Center.x - Width / 2;
    private float RightSide => LeftSide + Width;
    private int SelectedIndex;

    private void Start()
    {
        VoidEveryone();
        deck = GetComponent<PlayerDeck>();
        //function call to clear everything
        GetCardsFromDeck();
        for(int i = 0; i < 5; i++)
        {
            DrawCard();
        }
    }
    public CardHolder AddCardToHand(Card newcard)
    {
        if (CardsInHand.Count >= HandSize)
        {
            return null;
        }
        var go = Instantiate(CardPrefab, Transform);
        var card = go.GetComponent<CardHolder>();
        card.Init(newcard, CardsInHand.Count+1);
        CardsInHand.Add(card);
        Reposition();
        return card;
    }
    
    /// <summary>
    /// Destroys all shreds.
    /// </summary>
    public void VoidEveryone()
    {
        foreach (var crd in CardsInHand)
        {
            Destroy(crd.gameObject);
        }

        CardsInHand = new List<CardHolder>();
        DrawPile = new List<Card>();
        DiscardPile = new List<Card>();
        GraveyardPile = new List<Card>();
    }


    public void GetCardsFromDeck()
    {
        DrawPile = deck.GetCopy();
    }
    public bool DrawCard()
    {
        if (DrawPile.Count <= 0)
        {
            if (DiscardPile.Count <= 0)
            {
                return false;
            }
            DrawPile.AddRange(DiscardPile);
            DiscardPile = new List<Card>();
        }
        int index = Random.Range(0, DrawPile.Count);
        Card card = DrawPile[index];
        AddCardToHand (card);
        DrawPile.RemoveAt(index);
        return true;
    }

    public Card TestCard;
    [Button(enabledMode:EButtonEnableMode.Playmode)]
    public void AddCardTest()
    {
        AddCardToHand(TestCard);
    }

    public void AddHand(params Card[] Cards)
    {
        foreach (var Card in Cards)
        {
            if (AddCardToHand(Card) == null) return;
        }
    }

    public void HoverCard(int index)
    {
        for(int i = 0; i < CardsInHand.Count; i++)
        {
            if(i == index)
            {
                CardsInHand[i].ToggleHover(!CardsInHand[i].IsPreviewing);
                SelectedIndex = i;
            }
            else
            {
                CardsInHand[i].ToggleHover(false);
            }
        }
    }

    private void Reposition()
    {
        for (var i = 0; i < CardsInHand.Count; i++)
        {
            float pos = Mathf.Lerp(LeftSide, RightSide, (i + 1f) / (CardsInHand.Count+1));
            CardsInHand[i].MoveToPosition(pos, i);
        }
    }
}