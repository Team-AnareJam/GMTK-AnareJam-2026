using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class SettingsOpener : MonoBehaviour
{
    public GameObject SettingsPrefab;
    public GameObject OpenedSettings;

    public void OpenSettings()
    {
        OpenedSettings = Instantiate(SettingsPrefab);
        Time.timeScale = 0;
    }

    public void CloseSettings()
    {
        Time.timeScale = 1;
        Destroy(OpenedSettings);
        OpenedSettings = null;
    }

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
                    InputManager.Actions.Player.Pause.started += OpenSettings;
                    break;
            }
        }
    }

    private void OpenSettings(InputAction.CallbackContext context)
    {
        if (OpenedSettings == null) OpenSettings();
        else CloseSettings();
    }

    void UnsubscribeAllListeners()
    {
        InputManager.Actions.Player.Pause.started -= OpenSettings;
    }
    #endregion

}
