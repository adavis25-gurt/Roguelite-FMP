using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

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
        if (pauseUi == null)
        {
            pauseUi = GameObject.Find("PauseUI").GetComponent<UIDocument>();
        }
        var panel = pauseUi.rootVisualElement.Q<VisualElement>("Panel");
        if (panel.ClassListContains("hide")) 
        {
            print("yea");
            panel.RemoveFromClassList("hide");
            Time.timeScale = 0;
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            print("mnah");
            panel.AddToClassList("hide");
            Time.timeScale = 1;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }
    }

    public void TogglePause()
    {
        var panel = pauseUi.rootVisualElement.Q<VisualElement>("Panel");
        if (panel.ClassListContains("hide"))
        {
            print("yea");
            panel.RemoveFromClassList("hide");
            Time.timeScale = 0;
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            print("mnah");
            panel.AddToClassList("hide");
            Time.timeScale = 1;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }
    }

    public void ForceOff()
    {
        var panel = pauseUi.rootVisualElement.Q<VisualElement>("Panel");
        panel.AddToClassList("hide");
        Time.timeScale = 1;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }
}
