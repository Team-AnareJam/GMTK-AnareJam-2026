using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using Unity.Mathematics;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private List<CardHolder> CardsInHand = new();
    public int HandSize;
    public float CardMoveTime;
    public GameObject CardPrefab;

    private RectTransform Transform;
    private float Width => Transform.rect.width;
    private float Height => Transform.rect.height;
    private Vector2 Center => Transform.rect.center;
    private float LeftSide => Center.x - Width / 2;
    private float RightSide => LeftSide + Width;

    public CardHolder AddCard(Card newcard)
    {
        if (CardsInHand.Count >= HandSize)
        {
            return null;
        }

        var go = Instantiate(CardPrefab);
        var card = go.GetComponent<CardHolder>();
        card.Init(newcard);
        CardsInHand.Add(card);
        Reposition();
        return card;
    }

    public Card TestCard;
    [Button]
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

    private void Reposition()
    {
        float pos = 1;
        for (var i = 0; i < CardsInHand.Count; i++)
        {
            var goalpos = math.lerp(LeftSide, RightSide, (i+1f) / CardsInHand.Count);
            CardsInHand[i].gameObject.GetComponent<Rigidbody>().MovePosition(new Vector3(goalpos, 0, -1));
        }
    }
}