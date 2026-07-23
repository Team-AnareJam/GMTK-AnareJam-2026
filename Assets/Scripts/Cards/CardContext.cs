using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class CardContext
{
    public Vector2 PlayerPosition;
    public Vector2 MousePosInWorld;
    public Vector2 AimingDirection => ( MousePosInWorld - PlayerPosition).normalized;
    public PlayerUIManager playerUI;
    //hovering enemy;

    [SerializeField] private List<CardPrefab> CardPrefabs;

    public GameObject GetPrefab(string Name)
    {
        return CardPrefabs.First(go => go.Name == Name).Object;
    }
}

[Serializable]
internal struct CardPrefab
{
    public string Name;
    public GameObject Object;
}