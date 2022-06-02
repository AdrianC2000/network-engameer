using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using CharacterActions;
using Newtonsoft.Json;
using SceneHandlers;
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
    public static GameObject wrongAnswerAnimation;
    public static GameObject correctAnswerAnimation;
    public static GameObject statisticsMenuUI;

    public QuestInput(GameObject movingElement, GameObject questUI)
    {
        _movingElement = movingElement;
        _questUI = questUI;
    }

    public void CheckInput()
    {
        TextMeshProUGUI textField = (TextMeshProUGUI) _questUI.transform.Find("AnswerInputField").Find("Text Area").Find("Text").GetComponents(typeof(TextMeshProUGUI))[0];
        String answer = textField.text;
        answer = answer.ToLower();
        answer = answer.Replace("\u200B", "");
        String correctFormatAnswer = answer.Trim();
        correctFormatAnswer = Regex.Replace(correctFormatAnswer, @"\s+", " ");
        if (correctFormatAnswer.Equals(_correctAnswer))
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
        correctAnswerAnimation.SetActive(true);
        PlayerHandler.ScheduleDeactivation(correctAnswerAnimation, 2, DateTime.Now);
        StaticContainer.CorrectAnswersCounter += 1;
        Player.SetRespawnPosition(Collider.collidedElementPosition);
        Player.SetFirstQuestCall(false);
        Player.AddUsedDevicesWithQuest(Collider.ActualQuestDeviceName);
        Resume(true);
    }
    
    public void ResumeIfWrongAnswer()
    {
        wrongAnswerAnimation.SetActive(true);
        PlayerHandler.ScheduleDeactivation(wrongAnswerAnimation, 2, DateTime.Now);
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
        
        if (StaticContainer.WereStatisticsLoaded)
        {
            statisticsMenuUI.SetActive(true);
        }
        
        Time.timeScale = 1f;
        StaticContainer.TotalAnswersCounter += 1;
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
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, DifficultyLevel + "InputQuests", jsonFileName);
        File.WriteAllText(filePath, json);
        
        TextMeshProUGUI questionTestMesh = (TextMeshProUGUI) _questUI.transform.Find("Question").GetComponents(typeof(TextMeshProUGUI))[0];
        questionTestMesh.text = userQuestsList[drawnIndex].question;

        _correctAnswer = userQuestsList[drawnIndex].answers.correct;
    }

    private List<UserQuest> LoadJson()
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, DifficultyLevel + "InputQuests", jsonFileName);
        StreamReader streamReader = new StreamReader(filePath);
        string json = streamReader.ReadToEnd();
        streamReader.Close();
        List<UserQuest> items = JsonConvert.DeserializeObject<List<UserQuest>>(json);
        return items; 
        
    }

    public static void ReloadQuestsFile()
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, DifficultyLevel + "InputQuests", jsonFileName);
        string filePathOriginal = System.IO.Path.Combine(Application.streamingAssetsPath, DifficultyLevel + "InputQuests", _originalJsonFileName);
        StreamReader streamReader = new StreamReader(filePathOriginal);
        string json = streamReader.ReadToEnd();
        streamReader.Close();
        File.WriteAllText(filePath, json);
    }
}
