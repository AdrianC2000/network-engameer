using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level 2");
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
