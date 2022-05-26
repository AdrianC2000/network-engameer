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
        Quest.ReloadQuestsFile(); // TODO -> move those reloads into the finish of the game, not the start
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
        Quest.DifficultyPath = Quest.folderPath + difficultyLevel + "Quests/";
        QuestInput.DifficultyPath = QuestInput.folderPath + difficultyLevel + "InputQuests/";
    }

    public void ShowMenu()
    {
        mainMenu.SetActive(true);
        difficultiesMenu.SetActive(false);
    }
}
