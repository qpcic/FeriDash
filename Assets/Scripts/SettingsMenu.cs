using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{

    public void OpenMenu()
    {
        Debug.Log("Click Test");
        SceneManager.LoadScene("MainMenu");
    }

    // "TODO: Music Slider"
    // "TODO: SFX Slider"
    // "TODO: Auto-Retry Toggle"
    // "TODO: Show FPS Toggle"
    
}
