using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{

    public void PlayLevelOne()
    {
        SceneManager.LoadScene("SampleScene");

    }

    public void PlayLevelTwo()
    {
        SceneManager.LoadScene("SampleScene");

    }

    public void OpenMenu()
    {
        Debug.Log("Click Test");
        SceneManager.LoadScene("MainMenu");
    }

}
