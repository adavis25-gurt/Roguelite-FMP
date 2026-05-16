using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System;

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

    Resolution[] AllResolutions;

    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
        AllResolutions = Screen.resolutions;
    }

    private void OnEnable()
    {
        MasterVolumeSlider = ui.Q<Slider>();
        MasterVolumeSlider.RegisterValueChangedCallback(OnMasterVolumeChanged);
        AudioListener.volume = 0.5f;

        ResolutionDropdown = ui.Q<DropdownField>("Resolution");
        List<string> resolutionStrings = new List<string>();
        foreach (Resolution res in AllResolutions)
        {
            resolutionStrings.Add(res.width + "x" + res.height + "x" + res.refreshRateRatio);
        }
        ResolutionDropdown.choices = resolutionStrings;
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
        AudioListener.volume = evt.newValue;
    }

    void OnResolutionChanged(ChangeEvent<string> evt)
    {
        string[] result = evt.newValue.Split('x');
        Screen.SetResolution(Int32.Parse(result[0]), Int32.Parse(result[1]), FullScreenMode.ExclusiveFullScreen, Int32.Parse(result[2]));
    }

    void OnHighQualityClicked() => SetQualityByName("High Quality");
    void OnMedQualityClicked()  => SetQualityByName("Medium Quality");
    void OnLowQualityClicked()  => SetQualityByName("Low Quality");

    void OnExitButtonClicked()
    {
        ui.Q<VisualElement>("Panel").AddToClassList("hide");
        PauseUi.rootVisualElement.Q<VisualElement>("Container").RemoveFromClassList("hide");
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
