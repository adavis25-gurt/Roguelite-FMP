using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] VisualElement ui;

    [SerializeField] Button StoryButton;
    [SerializeField] Button EndlessButton;
    [SerializeField] Button SettingsButton;
    [SerializeField] Button ExitButton;

    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        StoryButton = ui.Q<Button>("StoryButton");
        StoryButton.clicked += OnStoryButtonClicked;

        EndlessButton = ui.Q<Button>("EndlessButton");
        EndlessButton.clicked += OnEndlessButtonClicked;

        SettingsButton = ui.Q<Button>("SettingsButton");
        SettingsButton.clicked += OnSettingsButtonClicked;

        ExitButton = ui.Q<Button>("ExitButton");
        ExitButton.clicked += OnExitButtonClicked;
    }

    void OnStoryButtonClicked()
    {
        SceneManager.LoadScene("MainScene");
    }

    void OnEndlessButtonClicked()
    {
        print("ye go to endless mode chud");
    }

    void OnSettingsButtonClicked()
    {
        print("ye go to settings chud");
    }

    void OnExitButtonClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
