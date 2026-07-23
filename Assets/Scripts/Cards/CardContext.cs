using UnityEngine;

[System.Serializable]
public class CardContext
{
    public Vector2 PlayerPosition;
    public Vector2 MousePosInWorld;
    public Vector2 AimingDirection => (PlayerPosition - MousePosInWorld).normalized;
    public PlayerManager player;
    //hovering enemy;
}