using System;
using CharacterActions;
using SceneHandlers;
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
    public static bool AreStatisticsVisible;
    
    private void Update()
    {
        UpdateStatistics();
        if (Input.GetKeyDown(KeyCode.K) && !PauseMenu.IsGamePaused && !Collider.IsQuestOn)
        {
            if (AreStatisticsVisible)
            {
                HideStatistics();
                StaticContainer.WereStatisticsLoaded = false;
            }
            else
            {
                ShowStatistics();
                StaticContainer.WereStatisticsLoaded = true;
            }
        }
    }

    private void HideStatistics()
    {
        statisticsMenuUI.SetActive(false);
        AreStatisticsVisible = false;
    }

    public void ShowStatistics()
    {
        statisticsMenuUI.SetActive(true);
        AreStatisticsVisible = true;
    }

    private void UpdateStatistics()
    {
        deathsCounterTextMesh.text = "UPADKI: " + StaticContainer.DeathsCounter;
        if (StaticContainer.TotalAnswersCounter != 0)
        {
            double fraction = (double) StaticContainer.CorrectAnswersCounter / (double) StaticContainer.TotalAnswersCounter;
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
