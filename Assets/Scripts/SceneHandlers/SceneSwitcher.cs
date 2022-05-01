using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void StartGame() 
    {
        SceneManager.LoadScene("Main");
    }
    
    public void GoToMenu() 
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadRiddle()
    {
        SceneManager.LoadScene("Quest");
    }

    public void BackToGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}