using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    
    public void StartGame()
    {
        SceneManager.LoadScene("MyGame");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Settings()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
