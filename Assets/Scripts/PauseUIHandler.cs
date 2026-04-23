using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseUIHandler : MonoBehaviour
{
    [SerializeField] PauseManager pauseManager;
    [SerializeField] VisualElement ui;

    [SerializeField] Button ResumeButton;
    [SerializeField] Button SettingsButton;
    [SerializeField] Button ExitButton;

    bool btnPressed = true;

    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        ResumeButton = ui.Q<Button>("Resume");
        SettingsButton = ui.Q<Button>("Settings");
        ExitButton = ui.Q<Button>("Exit");
    }

    private void Update()
    {
        if (btnPressed)
        {
            ExitButton.clicked += OnExitButtonClicked;
            SettingsButton.clicked += OnSettingsButtonClicked;
            ResumeButton.clicked += OnResumeButtonClicked;
        }
    }

    void OnResumeButtonClicked()
    {
        btnPressed = false;
        pauseManager.TogglePause();
        btnPressed = true;
    }

    void OnSettingsButtonClicked()
    {
        print("ye go to settings chud");
    }

    void OnExitButtonClicked()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
