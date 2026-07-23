using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public Camera cam;
    public InputAction mousepos;

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
                    break;
            }
        }
    }

    void UnsubscribeAllListeners()
    {
        mousepos = null;
        InputManager.Actions.Player.Attack.performed -= TestRay;
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

    // Update is called once per frame
    void Update()
    {
    }

    
}
