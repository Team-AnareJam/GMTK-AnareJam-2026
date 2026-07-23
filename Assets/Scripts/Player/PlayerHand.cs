using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using Unity.Mathematics;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private List<CardHolder> CardsInHand = new();
    [SerializeField] private List<CardHolder> DrawPile = new();
    [SerializeField] private List<CardHolder> DiscardPile = new();
    [SerializeField] private List<CardHolder> GraveyardPile = new();
    public int HandSize;
    public float CardMoveTime;
    public GameObject CardPrefab;

    public RectTransform Transform;
    private float Width => Transform.rect.width;
    private Vector2 Center => Transform.rect.center;
    private float LeftSide => Center.x - Width / 2;
    private float RightSide => LeftSide + Width;

    public CardHolder AddCard(Card newcard)
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

    public Card TestCard;
    [Button(enabledMode:EButtonEnableMode.Playmode)]
    public void AddCardTest()
    {
        AddCard(TestCard);
    }

    public void AddHand(params Card[] Cards)
    {
        foreach (var Card in Cards)
        {
            if (AddCard(Card) == null) return;
        }
    }

    public void HoverCard(int index)
    {
        for(int i = 0; i < CardsInHand.Count; i++)
        {
            if(i == index)
            {
                CardsInHand[i].ToggleHover(!CardsInHand[i].IsPreviewing);
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
            float pos = math.lerp(LeftSide, RightSide, (i + 1f) / (CardsInHand.Count+1));
            CardsInHand[i].MoveToPosition(pos, i);
        }
    }
}