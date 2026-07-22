using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public int MovementSpeed;
    public InputAction MoveAction;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Debug.Log(MoveAction.ReadValue<Vector2>());
        Vector3 moveTo = MoveAction.ReadValue<Vector2>().normalized * MovementSpeed * Time.fixedDeltaTime;
        transform.position += moveTo;
    }
}
