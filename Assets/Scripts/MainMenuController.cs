using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] VisualElement ui;

    [SerializeField] Button StoryButton;
    [SerializeField] Button TutorialButton;
    [SerializeField] Button SettingsButton;
    [SerializeField] Button ExitButton;

    [SerializeField] UIDocument SettingsUI;

    [SerializeField] FadeBlack fade;

    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        StoryButton = ui.Q<Button>("StoryButton");
        StoryButton.clicked += OnStoryButtonClicked;

        SettingsButton = ui.Q<Button>("SettingsButton");
        SettingsButton.clicked += OnSettingsButtonClicked;

        ExitButton = ui.Q<Button>("ExitButton");
        ExitButton.clicked += OnExitButtonClicked;

        TutorialButton  = ui.Q<Button>("TutorialButton");
        TutorialButton.clicked += OnTutorialButtonClicked;
    }

    void OnStoryButtonClicked() 
    {
        print("FADING");
        fade = GameObject.Find("Black").GetComponent<FadeBlack>();
        fade.FadeIn("MainScene");
    }

    void OnTutorialButtonClicked()
    {
        fade = GameObject.Find("Black").GetComponent<FadeBlack>();
        fade.FadeIn("Tutorial");
    }

    void OnSettingsButtonClicked()
    {
        var MainPanel = ui.Q<VisualElement>("Container");
        var SettingsPanel = SettingsUI.rootVisualElement.Q<VisualElement>("Panel");
        if (SettingsPanel.ClassListContains("hide"))
        {
            MainPanel.AddToClassList("hide");
            SettingsPanel.RemoveFromClassList("hide");
        }
        else
        {
            MainPanel.AddToClassList("hide");
            SettingsPanel.RemoveFromClassList("hide");
        }
    }

    void OnExitButtonClicked()
    {
        Application.Quit();
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #endif
    }
}
