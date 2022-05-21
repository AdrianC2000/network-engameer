using System;
using System.Collections.Generic;
using System.IO;
using CharacterActions;
using Newtonsoft.Json;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class QuestInput
{
    private GameObject _questUI;
    private GameObject _movingElement;
    public static Camera TemporaryQuestCamera;
    public static Player Player;
    public static String DifficultyLevel = "Easy";
    public static string folderPath = Directory.GetCurrentDirectory() + "/Assets/Scripts/QuestsHandlers/";
    public static string DifficultyPath = folderPath + DifficultyLevel + "InputQuests/";
    static string jsonFileName = "Quests.json";
    static string _originalJsonFileName = "OriginalQuests.json";
    private String _correctAnswer; 
    public static AudioSource audiosource;
    public static AudioClip correctAnswer;
    public static AudioClip wrongAnswer;

    public QuestInput(GameObject movingElement, GameObject questUI)
    {
        _movingElement = movingElement;
        _questUI = questUI;
    }

    public void CheckInput()
    {
        TextMeshProUGUI textField = (TextMeshProUGUI) _questUI.transform.Find("AnswerInputField").Find("Text Area").Find("Text").GetComponents(typeof(TextMeshProUGUI))[0];
        String answer = textField.text;
        answer = answer.Replace("\u200B", "");
        if (answer.Equals(_correctAnswer))
        {
            ResumeIfCorrectAnswer();
        }
        else
        {
            ResumeIfWrongAnswer();
        }
    }

    public void ResumeIfCorrectAnswer()
    {
        if (_movingElement != null)
        {
            _movingElement.SetActive(true);
        }

        Player.IncreaseCorrectAnswersCounter();
        Player.SetRespawnPosition(Player.GetCharacter().transform.position);
        Player.SetFirstQuestCall(false);
        Player.AddUsedDevicesWithQuest(Collider.ActualQuestDeviceName);
        DeleteQuestFromQuestUI();
        Resume(true);
    }
    
    public void ResumeIfWrongAnswer()
    {
        PlayerHandler.Respawn(Player);
        Player.SetFirstQuestCall(true);
        Resume(false);
    }

    private void Resume(bool wasAnswerCorrect)
    {
        _questUI.SetActive(false);
        Collider.IsQuestOn = false; 
        TemporaryQuestCamera.gameObject.SetActive(false);
        Player.GetCharacter().gameObject.SetActive(true);
        if (wasAnswerCorrect)
        {
            audiosource.clip = correctAnswer;
            audiosource.Play();
        }
        else
        {
            audiosource.clip = wrongAnswer;
            audiosource.Play();
        }
        Time.timeScale = 1f;
        Player.IncreaseTotalAnswersCounter();
    }

    public void DrawTask()
    {
        List<UserQuest> userQuestsList = LoadJson();
        int listLength = userQuestsList.Count;
        System.Random r = new System.Random();
        int drawnIndex = r.Next(0, listLength);

        List<UserQuest> userQuestsListLeft = new List<UserQuest>();
        userQuestsListLeft.AddRange(userQuestsList);
        userQuestsListLeft.Remove(userQuestsList[drawnIndex]);

        String json = JsonConvert.SerializeObject(userQuestsListLeft);
        File.WriteAllText(DifficultyPath + jsonFileName, json);
        
        TextMeshProUGUI questionTestMesh = (TextMeshProUGUI) _questUI.transform.Find("Question").GetComponents(typeof(TextMeshProUGUI))[0];
        questionTestMesh.text = userQuestsList[drawnIndex].question;

        _correctAnswer = userQuestsList[drawnIndex].answers.correct;
        _questUI.transform.Find("ConfirmButton").GetComponent<Button>().onClick.AddListener(CheckInput);
    }

    private List<UserQuest> LoadJson()
    {
        using (StreamReader streamReader = new StreamReader(DifficultyPath + jsonFileName))
        {
            string json = streamReader.ReadToEnd();
            List<UserQuest> items = JsonConvert.DeserializeObject<List<UserQuest>>(json);
            return items; 
        }
    }
    
    public void DeleteQuestFromQuestUI()
    {
        _questUI.transform.Find("ConfirmButton").GetComponent<Button>().onClick.RemoveAllListeners();
    }

    public static void ReloadQuestsFile()
    {
        using (StreamReader streamReader = new StreamReader(DifficultyPath + _originalJsonFileName))
        {
            string json = streamReader.ReadToEnd();
            File.WriteAllText(DifficultyPath + jsonFileName, json);
        }
    }
}
