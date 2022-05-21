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
    
    private void Update()
    {
        UpdateStatistics();
        if (Input.GetKeyDown(KeyCode.K) && !PauseMenu.IsGamePaused && !Collider.IsQuestOn)
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
        deathsCounterTextMesh.text = "UPADKI: " + Player.GetDeathsCounter();
        if (Player.GetTotalAnswersCounter() != 0)
        {
            double fraction = (double) Player.GetCorrectAnswersCounter() / (double) Player.GetTotalAnswersCounter();
            double percentage = Math.Round(fraction, 2) * 100;
            correctAnswersPercentageTextMesh.text = "POPRAWNE ODPOWIEDZI: " + percentage + "%";
        } else
        {
            correctAnswersPercentageTextMesh.text = "POPRAWNE ODPOWIEDZI: -";
        }

        double actualTime = Time.time;
        
        if (Math.Round(actualTime) < 60)
        {
            timerTextMesh.text = "CZAS GRY: " + Math.Round(actualTime) + "s";
        } else if (Math.Round(actualTime) < 3600)
        {
            int mins = (int) Math.Round(actualTime) / 60;
            double seconds = Math.Round(actualTime) - mins * 60;
            timerTextMesh.text = "CZAS GRY: " + mins + "m " + seconds + "s";
        }
    }
}
