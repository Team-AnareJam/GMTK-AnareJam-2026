using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public Camera cam;
    private InputAction mousepos;

    [SerializeField] private PlayerDeck Deck;
    [SerializeField] private PlayerHand hand;

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
                    InputManager.Actions.Player.Attack.performed += TestRay;
                    InputManager.Actions.Player.PreviewCard.performed += PreviewCard;
                    break;
            }
        }
    }

    void UnsubscribeAllListeners()
    {
        mousepos = null;
        InputManager.Actions.Player.Attack.performed -= TestRay;
        InputManager.Actions.Player.PreviewCard.performed -= PreviewCard;
    }
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    void TestRay(InputAction.CallbackContext ctx)
    {
        var ray = cam.ScreenPointToRay(mousepos.ReadValue<Vector2>());
        Debug.Log(Physics.Raycast(ray, 10000f));
    }

    private int hoverindex = -1;
    private CardHolder hover;
    void PreviewCard(InputAction.CallbackContext ctx)
    {
        var ray = cam.ScreenPointToRay(mousepos.ReadValue<Vector2>());
        if (Physics.Raycast(ray, out var hit, maxDistance:1000))
        {
            if (hit.collider.CompareTag("Cards"))
            {
                if (hit.transform.TryGetComponent<CardHolder>(out var component))
                {
                    hand.HoverCard(component.Index);
                }
            }
            
        }
    }

    // Update is called once per frame
    // void FixedUpdate()
    // {
    //     var ray = cam.ScreenPointToRay(mousepos.ReadValue<Vector2>());
    //     if (Physics.Raycast(ray, 10000f))
    //     {
    //         
    //     }
    // }

    
}
