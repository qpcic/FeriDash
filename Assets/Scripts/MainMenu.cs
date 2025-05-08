using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");

    }

    public void QuitGame()
    {
        Debug.Log("EXIT GAME");
        Application.Quit();
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsMenu");

    }

    public void OpenProfile()
    {
        SceneManager.LoadScene("ProfileMenu");
    }
    
}
