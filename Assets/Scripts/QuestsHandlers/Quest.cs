using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using CharacterActions;
using Newtonsoft.Json;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Quest
{
    private GameObject _questUI;
    private GameObject _movingElement;
    public static Camera TemporaryQuestCamera;
    public static Player Player;
    public static String DifficultyLevel = "Easy";
    public static string folderPath = Directory.GetCurrentDirectory() + "/Assets/Scripts/QuestsHandlers/";
    public static string DifficultyPath = folderPath + DifficultyLevel + "Quests/";
    static string jsonFileName = "Quests.json";
    static string _originalJsonFileName = "OriginalQuests.json";
    public static AudioSource audiosource; 
    public static AudioClip correctAnswer;
    public static AudioClip wrongAnswer;
    public static GameObject wrongAnswerAnimation;

    public Quest(GameObject movingElement, GameObject questUI)
    {
        _movingElement = movingElement;
        _questUI = questUI;
    }

    public void ResumeIfCorrectAnswer()
    {
        if (_movingElement != null)
        {
            _movingElement.SetActive(true);
        }

        Player.IncreaseCorrectAnswersCounter();
        Player.SetRespawnPosition(Collider.collidedElementPosition);
        Player.SetFirstQuestCall(false);
        Player.AddUsedDevicesWithQuest(Collider.ActualQuestDeviceName);
        DeleteQuestFromQuestUI(3);
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
        Time.timeScale = 1f;
        Player.IncreaseTotalAnswersCounter();
    }

    public void DrawTask(int answersNumber)
    {
        List<UserQuest> userQuestsList = LoadJson();
        int listLength = userQuestsList.Count;
        System.Random r = new System.Random();
        int drawnIndex = r.Next(0, listLength);

        List<UserQuest> userQuestsListLeft = new List<UserQuest>();
        userQuestsListLeft.AddRange(userQuestsList);
        userQuestsListLeft.Remove(userQuestsList[drawnIndex]);

        String json = JsonConvert.SerializeObject(userQuestsListLeft);
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, DifficultyLevel + "Quests", jsonFileName);
        File.WriteAllText(filePath, json);

        int drawnCorrectButtonIndex = r.Next(0, answersNumber);
        
        TextMeshProUGUI questionTestMesh = (TextMeshProUGUI) _questUI.transform.Find("Question").GetComponents(typeof(TextMeshProUGUI))[0];
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

    public void DeleteQuestFromQuestUI(int answersNumber)
    {
        Transform[] transforms = GetAnswersTransforms(answersNumber);
        for (int i = 0; i < answersNumber; i++)
        {
            transforms[i].GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }
    
    private List<UserQuest> LoadJson()
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, DifficultyLevel + "Quests", jsonFileName);
        StreamReader streamReader = new StreamReader(filePath);
        string json = streamReader.ReadToEnd();
        streamReader.Close();
        List<UserQuest> items = JsonConvert.DeserializeObject<List<UserQuest>>(json);
        return items;
        }

    private Transform[] GetAnswersTransforms(int listLength)
    {
        Transform[] transforms = new Transform[listLength];
        int i = 0; 
        foreach (Transform child in _questUI.transform)
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
        Debug.Log(Application.streamingAssetsPath);
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, DifficultyLevel + "Quests", jsonFileName);
        string filePathOriginal = System.IO.Path.Combine(Application.streamingAssetsPath, DifficultyLevel + "Quests", _originalJsonFileName);
        StreamReader streamReader = new StreamReader(filePathOriginal);
        string json = streamReader.ReadToEnd();
        File.WriteAllText(filePath, json);
        streamReader.Close();
    }
}
