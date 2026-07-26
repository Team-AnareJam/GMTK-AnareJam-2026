using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public bool CanMove;
    public int MovementSpeed;
    public float MovementMult;
    [SerializeField] private GameObject camTarget;
    [SerializeField] private float camTargetDist;
    private InputAction MoveAction;

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
                    MoveAction = InputManager.Actions.Player.Move;
                    break;
            }
        }
    }

    void UnsubscribeAllListeners()
    {
        MoveAction = null;
    }
    #endregion

    private void Awake()
    {
        ContextManager.Instance.CardCtx.PlayerPosition = transform.position;
        CanMove = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ContextManager.Instance.CardCtx.PlayerPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (!CanMove) return;
        if (InputManager.Instance.currentMap != InputManager.Actions.Player.Get()) return;

        Vector2 dir = MoveAction.ReadValue<Vector2>();
        camTarget.transform.position = transform.position + (Vector3)(dir * camTargetDist);
        Vector3 moveTo = dir.normalized * (MovementSpeed * MovementMult * Time.fixedDeltaTime);
        transform.position += moveTo;

    }
}
