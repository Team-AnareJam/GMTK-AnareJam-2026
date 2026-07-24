using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Game Data/Card")]
[System.Serializable]
public class Card : ScriptableObject
{
    public string ID;
    public string Name;
    public CardRarity cardRarity;
    public CardType cardType;
    public float Cost;
    public Texture2D Art;
    public string Description;
    public Texture2D Background;
    public string Credits;
    public CardLogic Logic;

    public Card()
    {

    }
    public Card(Card reference)
    {
        ID = reference.ID;
        Name = reference.Name;
        Cost = reference.Cost;
        Art = reference.Art;
        Description = reference.Description;
        Background = reference.Background;
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