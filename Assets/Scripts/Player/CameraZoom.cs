using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    private InputAction ZoomAction;
    private CinemachineCamera cam;
    [SerializeField] private int zoomSpeed;
    [SerializeField] private float zoomAmount;

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
                    ZoomAction = InputManager.Actions.Player.ZoomCamera;
                    break;
            }
        }
    }

    void UnsubscribeAllListeners()
    {
        ZoomAction = null;
    }
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<CinemachineCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(ZoomAction.ReadValue<Vector2>());
        if (cam != null) 
        {
            Vector2 scrollAmount = ZoomAction.ReadValue<Vector2>() * zoomSpeed * Time.deltaTime;
            zoomAmount -= scrollAmount.y;
            zoomAmount = Mathf.Clamp(zoomAmount, 5, 15);
            cam.Lens.OrthographicSize = zoomAmount;
        }
    }
}
