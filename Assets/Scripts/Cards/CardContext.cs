using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class CardContext
{
    public Vector2 PlayerPosition;
    public Vector2 MousePosInWorld;
    public Vector2 AimingDirection => (PlayerPosition - MousePosInWorld).normalized;
    [FormerlySerializedAs("player")] public PlayerUIManager playerUI;
    //hovering enemy;
}