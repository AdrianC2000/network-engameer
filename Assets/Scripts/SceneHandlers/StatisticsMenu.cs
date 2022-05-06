using System;
using CharacterActions;
using TMPro;
using UnityEngine;

public class StatisticsMenu : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI deathsCounterTextMesh;
    [SerializeField] 
    private TextMeshProUGUI correctAnswersPercentageTextMesh;
    [SerializeField] 
    private TextMeshProUGUI timerTextMesh;
    [SerializeField]
    private GameObject statisticsMenuUI;
    private bool _areStatisticsVisible;
    public static Player Player;

    private void Awake()
    {
        
    }
    private void Update()
    {
        UpdateStatistics();
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (_areStatisticsVisible)
            {
                HideStatistics();
            }
            else
            {
                ShowStatistics();
            }
        }
    }

    private void HideStatistics()
    {
        statisticsMenuUI.SetActive(false);
        _areStatisticsVisible = false;
    }

    private void ShowStatistics()
    {
        statisticsMenuUI.SetActive(true);
        _areStatisticsVisible = true;
    }

    private void UpdateStatistics()
    {
        deathsCounterTextMesh.text = "Śmierci: " + Player.GetDeathsCounter();
        if (Player.GetTotalAnswersCounter() != 0)
        {
            double fraction = (double) Player.GetCorrectAnswersCounter() / (double) Player.GetTotalAnswersCounter();
            double percentage = Math.Round(fraction, 2) * 100;
            correctAnswersPercentageTextMesh.text = "Poprawne odpowiedzi: " + percentage + "%";
        } else
        {
            correctAnswersPercentageTextMesh.text = "Brak odpowiedzi";
        }

        double actualTime = Time.realtimeSinceStartup;
        
        if (Math.Round(actualTime) < 60)
        {
            timerTextMesh.text = "Czas gry: " + Math.Round(actualTime) + "s";
        } else if (Math.Round(actualTime) < 3600)
        {
            int mins = (int) Math.Round(actualTime) / 60;
            double seconds = Math.Round(actualTime) - mins * 60;
            timerTextMesh.text = "Czas gry: " + mins + "m " + seconds + "s";
        }
    }
}
