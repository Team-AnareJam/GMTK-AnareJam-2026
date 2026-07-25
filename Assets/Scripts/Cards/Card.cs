using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Game Data/Card")]
[System.Serializable]
public class Card : ScriptableObject
{
    public string ID;
    public string Name;
    public CardRarity cardRarity;
    public CardType cardType;
    public PlayType playType;
    public float Cost;
    public Sprite Art;
    public string Description;
    public string Credits;
    public CardLogic Logic;

    public Card()
    {

    }
    public Card(Card reference)
    {
        ID = reference.ID;
        Name = reference.Name;
        cardRarity = reference.cardRarity;
        cardType = reference.cardType;
        playType = reference.playType;
        Cost = reference.Cost;
        Art = reference.Art;
        Description = reference.Description;
        Credits = reference.Credits;
        Logic = reference.Logic;
    }
}

public enum CardRarity
{
    Common,
    Rare,
    SuperRare,
    Legendary
}

public enum CardType
{
    Attack,
    Skill,
    Status
}

public enum PlayType
{
    Grave,
    Discard,
}