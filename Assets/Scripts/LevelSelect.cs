using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public AudioSource buttonClickSound;

    public void OpenMenu()
    {
        PlayClickSound();
        Invoke(nameof(LoadMainMenu), 0.4f); // zakasnjen prehod
    }

    public void PlayLevelOne()
    {
        PlayClickSound();
        Invoke(nameof(LoadLevelOne), 0.4f);
    }

    public void PlayLevelTwo()
    {
        PlayClickSound();
        Invoke(nameof(LoadLevelTwo), 0.4f);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void LoadLevelOne()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void LoadLevelTwo()
    {
        SceneManager.LoadScene("SampleScene2");
    }

    private void PlayClickSound()
    {
        if (buttonClickSound != null)
            buttonClickSound.Play();
        else
            Debug.LogWarning("ButtonClickSound ni nastavljen!");
    }
}