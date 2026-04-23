using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PauseManager : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction pauseAction;

    [SerializeField] UIDocument pauseUi;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        pauseAction = playerInput.actions.FindAction("Pause");
        pauseAction.performed += OnPause;
    }

    void OnPause(InputAction.CallbackContext context)
    {
        pauseUi.enabled = !(pauseUi.enabled);
        if (pauseUi.enabled) 
        {
            Time.timeScale = 0;
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }
    }

    public void TogglePause()
    {
        print(pauseUi.isActiveAndEnabled);
        pauseUi.enabled = !(pauseUi.enabled);
        if (pauseUi.enabled)
        {
            Time.timeScale = 0;
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }
    }
}
