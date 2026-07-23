using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public Camera cam;

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
                    MoveAction = InputManager.Actions.Player.MousePosition;
                    InputManager.Actions.Player.Attack += TestRay;
                    break;
            }
        }
    }

    void UnsubscribeAllListeners()
    {
        MoveAction = null;
        InputManager.Actions.Player.Attack -= TestRay;
    }
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void TestRay()
    {
        Debug.Log(cam.ScreenPointToRay(Input.mousePosition));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(cam.ScreenPointToRay(Input.mousePosition));
        }
    }

    
}
