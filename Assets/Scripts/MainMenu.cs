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
        SceneManager.LoadScene("SettingsMenu");
    }

    public void OpenProfile()
    {
        PlayClickSound();
        SceneManager.LoadScene("ProfileMenu");
    }

    private void PlayClickSound()
    {
        if (buttonClickSound != null)
            buttonClickSound.Play();
    }
}