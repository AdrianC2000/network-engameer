using System;
using SceneHandlers;
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
        int nextSceneNumber = Int32.Parse(actualSceneName[actualSceneName.Length - 1].ToString()) + 1;
        String anotherSceneName = "Level " + nextSceneNumber; 
        SceneManager.LoadScene(anotherSceneName);
    }
    
    public void LoadMenu()
    {
        Destroy(StaticContainer.Music);
        SceneManager.LoadScene("Menu");
    }
    
    public void FinishGame()
    {
        SceneManager.LoadScene("Menu");
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
