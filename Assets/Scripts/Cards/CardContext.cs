using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class CardContext
{
    public Vector2 PlayerPosition;
    public Vector2 MousePosInWorld;
    public Vector2 AimingDirection => (MousePosInWorld - PlayerPosition).normalized;
    [HideInInspector] public PlayerUIManager playerUI;
    [HideInInspector] public PlayerMovement playerMovement;
}

[Serializable]
internal struct CardPrefab
{
    public string Name;
    public GameObject Object;
}