using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource buttonClickSound;

    void Start()
    {
        PlayerPrefs.SetInt("Attempts", 1);
    }

    public void LevelSelect()
    {
        PlayClickSound();
        Invoke(nameof(LoadLevelSelect), 0.40f); // Počakaj dokler se zvok ne zaigra
    }

    private void LoadLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void QuitGame()
    {
        PlayClickSound();
        Debug.Log("EXIT GAME");
        Application.Quit();
    }

    public void OpenSettings()
    {
        PlayClickSound();
        Invoke(nameof(LoadSettingsMenu), 0.40f);
    }

    private void LoadSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void OpenProfile()
    {
        PlayClickSound();
        Invoke(nameof(LoadProfileMenu), 0.40f);
    }

    private void LoadProfileMenu()
    {
        SceneManager.LoadScene("ProfileMenu");
    }

    private void PlayClickSound()
    {
        Debug.Log("Playing click sound");

        if (buttonClickSound != null)
            buttonClickSound.Play();
        else
            Debug.LogWarning("buttonClickSound is null!");
    }
}