using System;
using System.Collections.Generic;
using System.IO;
using CharacterActions;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    [SerializeField] 
    private GameObject movingFirewall;
    [SerializeField]
    private GameObject questUI;
    public static Camera TemporaryQuestCamera;
    public static Player Player;
    
    static string _folderPath = Directory.GetCurrentDirectory() + "/Assets/Scripts/QuestsHandlers/";
    static string jsonFileName = "Quests.json";
    static string _originalJsonFileName = "OriginalQuests.json";

    private void Start()
    {
        DrawTask(3);
    }
    
    public void ResumeIfCorrectAnswer()
    {
        movingFirewall.SetActive(true);
        Collider.FirstTimeStepped = false;
        Player.IncreaseCorrectAnswersCounter();
        Resume();
    }
    
    public void ResumeIfWrongAnswer()
    {
        PlayerHandler.Respawn(Player);
        Resume();
    }

    private void Resume()
    {
        questUI.SetActive(false);
        TemporaryQuestCamera.gameObject.SetActive(false);
        Player.GetCharacter().gameObject.SetActive(true);
        Time.timeScale = 1f;
        Player.IncreaseTotalAnswersCounter();
    }

    private void DrawTask(int answersNumber)
    {
        List<UserQuest> userQuestsList = LoadJson();
        int listLength = userQuestsList.Count;
        System.Random r = new System.Random();
        int drawnIndex = r.Next(0, listLength);

        List<UserQuest> userQuestsListLeft = new List<UserQuest>();
        userQuestsListLeft.AddRange(userQuestsList);
        userQuestsListLeft.Remove(userQuestsList[drawnIndex]);

        String json = JsonConvert.SerializeObject(userQuestsListLeft);
        File.WriteAllText(_folderPath + jsonFileName, json);

        int drawnCorrectButtonIndex = r.Next(0, answersNumber);
        
        TextMeshProUGUI questionTestMesh = (TextMeshProUGUI) questUI.transform.Find("Question").GetComponents(typeof(TextMeshProUGUI))[0];
        questionTestMesh.text = userQuestsList[drawnIndex].question;

        Transform[] transforms = GetAnswersTransforms(listLength);
        int wrongQuestionIndex = 0;
        for (int i = 0; i < answersNumber; i++)
        {
            if (i == drawnCorrectButtonIndex)
            {
                TextMeshProUGUI answerTestMesh = (TextMeshProUGUI) transforms[i].Find("Text").GetComponents(typeof(TextMeshProUGUI))[0];
                answerTestMesh.text = userQuestsList[drawnIndex].answers.correct;
                transforms[i].GetComponent<Button>().onClick.AddListener(ResumeIfCorrectAnswer);

            } else
            {
                TextMeshProUGUI answerTestMesh = (TextMeshProUGUI) transforms[i].Find("Text").GetComponents(typeof(TextMeshProUGUI))[0];
                answerTestMesh.text = userQuestsList[drawnIndex].answers.wrong[wrongQuestionIndex];
                transforms[i].GetComponent<Button>().onClick.AddListener(ResumeIfWrongAnswer);
                wrongQuestionIndex += 1;
            }
        }
    }
    
    private List<UserQuest> LoadJson()
    {
        using (StreamReader streamReader = new StreamReader(_folderPath + jsonFileName))
        {
            string json = streamReader.ReadToEnd();
            List<UserQuest> items = JsonConvert.DeserializeObject<List<UserQuest>>(json);
            return items; 
        }
    }

    private Transform[] GetAnswersTransforms(int listLength)
    {
        Transform[] transforms = new Transform[listLength];
        int i = 0; 
        foreach (Transform child in questUI.transform)
        {
            if (child.name == "Answer")
            {
                transforms[i] = child;
                i += 1;
            }
        }
        return transforms;
    }

    public static void ReloadQuestsFile()
    {
        using (StreamReader streamReader = new StreamReader(_folderPath + _originalJsonFileName))
        {
            string json = streamReader.ReadToEnd();
            File.WriteAllText(_folderPath + jsonFileName, json);
        }
    }
}
