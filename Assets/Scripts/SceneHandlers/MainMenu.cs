using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField] 
    private GameObject difficultiesMenu;
    
    public void ShowDifficulties()
    {
        mainMenu.SetActive(false);
        difficultiesMenu.SetActive(true);
    }

    public void ChangeDifficultyLevel()
    {
        String difficultyLevel = EventSystem.current.currentSelectedGameObject.name;
        Quest.DifficultyPath = Quest.folderPath + difficultyLevel + "Quests/";
    }

    public void ShowMenu()
    {
        mainMenu.SetActive(true);
        difficultiesMenu.SetActive(false);
    }
}
