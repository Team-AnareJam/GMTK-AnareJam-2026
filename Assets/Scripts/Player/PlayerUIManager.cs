using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUIManager : MonoBehaviour
{
    public Camera cam;
    private InputAction mousepos;

    [SerializeField] private PlayerDeck Deck;
    [SerializeField] private PlayerHand hand;
    [SerializeField] private LayerMask castingLayerMask;
    [SerializeField] private LayerMask backgroundLayerMask;
    private RaycastHit[] bgCheck;

    private void OnEnable()
    {
        InputManager.OnActionMapChange += SetInputListeners;
    }

    private void OnDisable()
    {
        InputManager.OnActionMapChange -= SetInputListeners;
    }

    #region Input Listeners
    void SetInputListeners(InputActionMap actionMap)
    {
        UnsubscribeAllListeners();
        if (actionMap != null)
        {
            switch (actionMap.name)
            {
                case nameof(InputManager.Actions.Player):
                    mousepos = InputManager.Actions.Player.MousePosition;
                    InputManager.Actions.Player.SelectCard.performed += Select;
                    InputManager.Actions.Player.Attack.performed += Attack;
                    break;
            }
        }
    }

    void UnsubscribeAllListeners()
    {
        mousepos = null;
        InputManager.Actions.Player.SelectCard.performed -= Select;
        InputManager.Actions.Player.Attack.performed -= Attack;
    }
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bgCheck = new RaycastHit[1];
    }

    private void Update()
    {
        if (mousepos == null)return;
        {
            
        }
        var ray = cam.ScreenPointToRay(mousepos.ReadValue<Vector2>());
        Physics.RaycastNonAlloc(ray, bgCheck, maxDistance: 100, backgroundLayerMask);
        if (bgCheck[0].collider)
        {
            if (bgCheck[0].collider.CompareTag("Background"))
            {
                ContextManager.Instance.CardCtx.MousePosInWorld = bgCheck[0].point;
            }
        }
    }
    void Select(InputAction.CallbackContext ctx)
    {
        var ray = cam.ScreenPointToRay(mousepos.ReadValue<Vector2>());
        if (Physics.Raycast(ray, out var hit, maxDistance:100, castingLayerMask))
        {
            switch (hit.collider.tag)
            {
                case "Cards":
                    if (hit.transform.TryGetComponent<CardHolder>(out var component))
                    {
                        hand.HoverCard(component.Index);    
                    }
                    break;
            }
        }
    }

    void Attack(InputAction.CallbackContext ctx)
    {
        var ray = cam.ScreenPointToRay(mousepos.ReadValue<Vector2>());
        if (Physics.Raycast(ray, out var hit, maxDistance: 100, castingLayerMask))
        {
            switch (hit.collider.tag)
            {
                case "Player":
                case "Enemy":
                case "Background":
                    hand.CastCard();
                    break;
                case "Cards":
                    if (hit.transform.TryGetComponent<CardHolder>(out var component))
                    {
                        hand.HoverCard(component.Index);
                    }
                    break;
            }
        }
    }

    public Vector2 dir;
    public float ang;
    public float dist;

    [Button]
    public void TestSwipe()
    {
        Debug.Log(MathAE.SwipePositions(dir, ang, dist));
    }
}
