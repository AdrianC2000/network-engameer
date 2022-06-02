using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField] 
    private GameObject difficultiesMenu;

    public void Start()
    {
        Quest.ReloadQuestsFile();
        QuestInput.ReloadQuestsFile();
    }
    public void ShowDifficulties()
    {
        mainMenu.SetActive(false);
        difficultiesMenu.SetActive(true);
    }

    public void ChangeDifficultyLevel()
    {
        String difficultyLevel = EventSystem.current.currentSelectedGameObject.name;
        Quest.DifficultyLevel = difficultyLevel;
        QuestInput.DifficultyLevel = difficultyLevel;
        Quest.ReloadQuestsFile();
        QuestInput.ReloadQuestsFile();
    }

    public void ShowMenu()
    {
        mainMenu.SetActive(true);
        difficultiesMenu.SetActive(false);
    }
}
