using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadAnotherLevel()
    {
        String actualSceneName = SceneManager.GetActiveScene().name;
        int nextSceneNumer = Int32.Parse(actualSceneName[actualSceneName.Length - 1].ToString()) + 1;
        String anotherSceneName = "Level " + nextSceneNumer; 
        SceneManager.LoadScene(anotherSceneName);
    }
    
    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    
    public void FinishGame()
    {
            //TODO
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
