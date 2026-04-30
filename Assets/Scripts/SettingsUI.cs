using UnityEngine;
using UnityEngine.UIElements;

public class SettingsController : MonoBehaviour
{
    [SerializeField] VisualElement ui;
    [SerializeField] UIDocument PauseUi;

    [SerializeField] Slider MasterVolumeSlider;

    [SerializeField] DropdownField ResolutionDropdown;

    [SerializeField] Button ExitButton;
    [SerializeField] Button HighQualityButton;
    [SerializeField] Button MedQualityButton;
    [SerializeField] Button LowQualityButton;

    [SerializeField] Toggle FullscreenToggle;
    [SerializeField] Toggle VSyncToggle;

    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        MasterVolumeSlider = ui.Q<Slider>();
        MasterVolumeSlider.RegisterValueChangedCallback(OnMasterVolumeChanged);

        ResolutionDropdown = ui.Q<DropdownField>("Resolution");
        ResolutionDropdown.choices = new System.Collections.Generic.List<string>
        {
            "256 x 144",
            "426 x 240",
            "640 x 360",
            "854 x 480",
            "1280 x 720",
            "1366 x 768",
            "1600 x 900",
            "1920 x 1080",
            "2560 x 1440",
            "3840 x 2160"
        };
        ResolutionDropdown.index = 7;
        ResolutionDropdown.RegisterValueChangedCallback(OnResolutionChanged);

        HighQualityButton = ui.Q<Button>("HighQuality");
        HighQualityButton.clicked += OnHighQualityClicked;

        MedQualityButton = ui.Q<Button>("MedQuality");
        MedQualityButton.clicked += OnMedQualityClicked;

        LowQualityButton = ui.Q<Button>("LowQuality");
        LowQualityButton.clicked += OnLowQualityClicked;

        ExitButton = ui.Q<Button>("ExitButton");
        ExitButton.clicked += OnExitButtonClicked;

        FullscreenToggle = ui.Q<Toggle>("Fullscreen");
        FullscreenToggle.RegisterValueChangedCallback(OnFullscreenToggled);

        VSyncToggle = ui.Q<Toggle>("VSYNC");
        VSyncToggle.RegisterValueChangedCallback(OnVSyncToggled);
    }

    private void OnDisable()
    {
        MasterVolumeSlider.UnregisterValueChangedCallback(OnMasterVolumeChanged);
        ResolutionDropdown.UnregisterValueChangedCallback(OnResolutionChanged);

        HighQualityButton.clicked -= OnHighQualityClicked;
        MedQualityButton.clicked -= OnMedQualityClicked;
        LowQualityButton.clicked -= OnLowQualityClicked;

        FullscreenToggle.UnregisterValueChangedCallback(OnFullscreenToggled);
        VSyncToggle.UnregisterValueChangedCallback(OnVSyncToggled);
    }

    void SetQualityByName(string qualityName)
    {
        for (int i = 0; i < QualitySettings.names.Length; i++)
        {
            if (QualitySettings.names[i] == qualityName)
            {
                QualitySettings.SetQualityLevel(i);
                break;
            }
        }
    }

    void OnMasterVolumeChanged(ChangeEvent<float> evt)
    {
        Debug.Log($"Master Volume: {evt.newValue}");
    }

    void OnResolutionChanged(ChangeEvent<string> evt)
    {
        Debug.Log($"Resolution: {evt.newValue}");
    }

    void OnHighQualityClicked() => SetQualityByName("High Quality");
    void OnMedQualityClicked()  => SetQualityByName("Medium Quality");
    void OnLowQualityClicked()  => SetQualityByName("Low Quality");

    void OnExitButtonClicked()
    {
        ui.Q<VisualElement>("Panel").AddToClassList("hide");
        PauseUi.rootVisualElement.Q<VisualElement>("Panel").RemoveFromClassList("hide");
    }

    void OnFullscreenToggled(ChangeEvent<bool> evt)
    {
        Screen.fullScreen = evt.newValue;
    }

    void OnVSyncToggled(ChangeEvent<bool> evt)
    {
        QualitySettings.vSyncCount = evt.newValue ? 1 : 0;
    }
}
