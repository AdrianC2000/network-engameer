using System;
using CharacterActions;
using SceneHandlers;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private CharacterController character;
    private Player _player;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip falling; 
    [SerializeField] private AudioClip correctAnswer; 
    [SerializeField] private AudioClip wrongAnswer;
    [SerializeField] private AudioClip startup;
    [SerializeField] private AudioClip finish;
    [SerializeField] private GameObject wrongAnswerAnimation;
    [SerializeField] private GameObject correctAnswerAnimation;
    [SerializeField] private GameObject statisticsMenuUI;
    public static DateTime actualStartTime;
    public static bool startCounting;
    public static int secondsToPass; 
    public static GameObject gameObjectToBeDeactivated;
    private bool _isFallen;
    
    private void Start()
    {
        audioSource.clip = startup;
        audioSource.Play();
        _player = new Player(character, character.transform.position);
        Collider.Player = _player;
        PauseMenu.Player = _player;
        PauseMenu.FPSController = transform.gameObject;
        Quest.audiosource = audioSource;
        QuestInput.audiosource = audioSource;
        Quest.correctAnswer = correctAnswer;
        QuestInput.correctAnswer = correctAnswer;
        Quest.wrongAnswer = wrongAnswer;
        QuestInput.wrongAnswer = wrongAnswer;
        Quest.wrongAnswerAnimation = wrongAnswerAnimation;
        QuestInput.wrongAnswerAnimation = wrongAnswerAnimation;
        Quest.correctAnswerAnimation = correctAnswerAnimation;
        QuestInput.correctAnswerAnimation = correctAnswerAnimation;
        Collider.finish = finish;
        Quest.statisticsMenuUI = statisticsMenuUI;
        QuestInput.statisticsMenuUI = statisticsMenuUI;
        if (StaticContainer.WereStatisticsLoaded)
        {
            StatisticsMenu.AreStatisticsVisible = true;
            statisticsMenuUI.SetActive(true);
        }

        if (PauseMenu.IsGamePaused)
        {
            PauseMenu.FPSController.GetComponent<FirstPersonController>().enabled = true;
            _player.GetCharacter().enabled = true;
            Time.timeScale = 1f;
            PauseMenu.IsGamePaused = false;
        }
    }

    private void Update()
    {
        RespawnIfFallen();
        if (startCounting)
        {
            TimeSpan difference = DateTime.Now - actualStartTime;
            if (difference.Seconds >= secondsToPass)
            {
                gameObjectToBeDeactivated.SetActive(false);
                startCounting = false; 
            }
        }
    }

    private void RespawnIfFallen()
    {
        if (character.transform.position.y <= -5 && !_isFallen)
        {
            _isFallen = true;
            audioSource.clip = falling;
            audioSource.Play();
        }
        if (character.transform.position.y <= -30)
        {
            _isFallen = false; 
            Respawn(_player);
            StaticContainer.DeathsCounter += 1;
        }
    }

    public static void Respawn(Player player)
    {
        player.GetCharacter().enabled = false;
        player.GetCharacter().transform.position = player.GetRespawnPosition();
        player.GetCharacter().enabled = true;
    }
    
    public static void ScheduleDeactivation(GameObject gameObject, int seconds, DateTime start)
    {
        startCounting = true;
        actualStartTime = start;
        secondsToPass = seconds;
        gameObjectToBeDeactivated = gameObject;
    }
}