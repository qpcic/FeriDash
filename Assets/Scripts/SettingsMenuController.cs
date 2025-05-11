using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    [Header("Volume")]
    [SerializeField] private Slider volumeSlider;

    [Header("FPS Toggle")]
    [SerializeField] private Toggle fpsToggle;
    [SerializeField] private GameObject fpsDisplay; // Assign the FPS counter UI Text or panel

    [Header("Auto retry toggle")]
    [SerializeField] private Toggle autoRetryToggle;

    private void Start()
    {
        if (fpsDisplay == null)
            fpsDisplay = GameObject.Find("FPSCanvas");

        // Load and apply saved volume
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.6f);
        volumeSlider.value = savedVolume;
        if (MusicManager.Instance != null)
            MusicManager.Instance.SetVolume(savedVolume);
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        // Load and apply FPS toggle state
        bool showFPS = PlayerPrefs.GetInt("ShowFPS", 0) == 1;
        fpsToggle.isOn = showFPS;
        if (FPSDisplay.Instance != null)
            FPSDisplay.Instance.SetVisible(showFPS);
        fpsToggle.onValueChanged.AddListener(OnFPSToggleChanged);

        // Load and apply Auto Retry state
        bool autoRetry = PlayerPrefs.GetInt("AutoRetry", 0) == 1;
        autoRetryToggle.isOn = autoRetry;
        autoRetryToggle.onValueChanged.AddListener(OnAutoRetryToggle);

    }

    private void OnVolumeChanged(float value)
    {
        if (MusicManager.Instance != null)
            MusicManager.Instance.SetVolume(value);
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
    }

    private void OnFPSToggleChanged(bool isOn)
    {
        if (FPSDisplay.Instance != null)
            FPSDisplay.Instance.SetVisible(isOn);

        PlayerPrefs.SetInt("ShowFPS", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
        fpsToggle.onValueChanged.RemoveListener(OnFPSToggleChanged);
    }

    private void OnAutoRetryToggle(bool autoRetry) {
        PlayerPrefs.SetInt("AutoRetry", autoRetry ? 1 : 0);
        PlayerPrefs.Save();
    }
}
