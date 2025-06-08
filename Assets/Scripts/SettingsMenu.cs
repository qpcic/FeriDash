using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{

    public AudioSource buttonClickSound;
    public void OpenMenu()
    {
        PlayClickSound();
        Debug.Log("Click Test");
        Invoke(nameof(LoadMenu), 0.40f);
    }


    private void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void PlayClickSound()
    {
        Debug.Log("Playing click sound");

        if (buttonClickSound != null)
            buttonClickSound.Play();
        else
            Debug.LogWarning("buttonClickSound is null!");
    }

    // "TODO: Music Slider"
    // "TODO: SFX Slider"
    // "TODO: Auto-Retry Toggle"
    // "TODO: Show FPS Toggle"

}
