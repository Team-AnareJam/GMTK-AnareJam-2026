using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance {  get; private set; }
    public static InputControls Actions;
    [HideInInspector] public InputActionMap currentMap;
    //private InputActionMap globalMap;
    public static event Action<InputActionMap> OnActionMapChange;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        Actions = new InputControls();
        //globalMap = Actions.GlobalAndDebug;
    }
    // Start is called before the first frame update
    void Start()
    {
        ToggleActionMap(Actions.Player); // put in player state machine later.
    }
    public void ToggleActionMap(InputActionMap actionMap)
    {
        if (actionMap.enabled) return;

        Actions.Disable();
        //globalMap.Enable();
        OnActionMapChange?.Invoke(actionMap);
        Debug.Log("Action map changed to: " +  actionMap.name);
        currentMap = actionMap;
        actionMap.Enable();
        
    }

    public void DisableActions()
    {
        Actions.Disable();
        OnActionMapChange?.Invoke(null);
        Debug.Log("Action map disabled");
    }
    public void EnableActions()
    {
        ToggleActionMap(Actions.Player);
        Debug.Log("Action map enabled");
    }
}
