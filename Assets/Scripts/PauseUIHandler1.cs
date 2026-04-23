using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseUIHandler : MonoBehaviour
{
    [SerializeField] PauseManager pauseManager;
    [SerializeField] VisualElement ui;

    [SerializeField] Button ResumeButton;
    [SerializeField] Button SettingsButton;
    [SerializeField] Button ExitButton;

    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        ResumeButton = ui.Q<Button>("Resume");
        SettingsButton = ui.Q<Button>("Settings");
        ExitButton = ui.Q<Button>("Exit");

        ResumeButton.RegisterCallback<ClickEvent>(OnResumeButtonClicked);
        SettingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClicked);
        ExitButton.RegisterCallback<ClickEvent>(OnExitButtonClicked);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        ResumeButton.UnregisterCallback<ClickEvent>(OnResumeButtonClicked);
        SettingsButton.UnregisterCallback<ClickEvent>(OnSettingsButtonClicked);
        ExitButton.UnregisterCallback<ClickEvent>(OnExitButtonClicked);

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnResumeButtonClicked(ClickEvent evt)
    {
        pauseManager.TogglePause();
    }

    void OnSettingsButtonClicked(ClickEvent evt)
    {
        print("ye go to settings chud");
    }

    void OnExitButtonClicked(ClickEvent evt)
    {
        SceneManager.LoadScene("Main Menu");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main Menu" || scene.name == "MainScene")
        {
            pauseManager.ForceOff();
        }
    }
}
