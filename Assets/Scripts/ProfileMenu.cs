using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileMenu : MonoBehaviour
{

    public void OpenMenu()
    {
        Debug.Log("Click Test");
        SceneManager.LoadScene("MainMenu");
    }

    public void CharacterOne()
    {
        Debug.Log("Character One Selected");
        // "TODO: Load Character One Scene";
    }

    // "TODO: Load Character Two Scene";
    // "TODO: Load Character Three Scene";
    
}
