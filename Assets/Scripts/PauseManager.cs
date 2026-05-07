using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
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
            panel.RemoveFromClassList("hide");
            Time.timeScale = 0;
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            if (SceneManager.GetActiveScene().name == "TherapyRoom")
            {
                GameObject.Find("Main Camera").GetComponent<CameraController>().enabled = false;
            }
        }
        else
        {
            panel.AddToClassList("hide");
            Time.timeScale = 1;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
            if (SceneManager.GetActiveScene().name == "TherapyRoom")
            {
                GameObject.Find("Main Camera").GetComponent<CameraController>().enabled = true;
            }
        }
    }

    public void TogglePause()
    {
        var panel = pauseUi.rootVisualElement.Q<VisualElement>("Panel");
        if (panel.ClassListContains("hide"))
        {
            panel.RemoveFromClassList("hide");
            Time.timeScale = 0;
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            if (SceneManager.GetActiveScene().name == "TherapyRoom")
            {
                GameObject.Find("Main Camera").GetComponent<CameraController>().enabled = false;
            }
        }
        else
        {
            panel.AddToClassList("hide");
            Time.timeScale = 1;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
            if (SceneManager.GetActiveScene().name == "TherapyRoom")
            {
                GameObject.Find("Main Camera").GetComponent<CameraController>().enabled = true;
            }
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
