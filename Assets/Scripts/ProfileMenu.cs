using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileMenu : MonoBehaviour
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

    public void CharacterOne()
    {
        Debug.Log("Character One Selected");
        // "TODO: Load Character One Scene";
    }

    // "TODO: Load Character Two Scene";
    // "TODO: Load Character Three Scene";
    
}
