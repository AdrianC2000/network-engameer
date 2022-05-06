using CharacterActions;
using UnityEngine;

public class FirstQuest : MonoBehaviour
{
    [SerializeField] 
    private GameObject movingFirewall;
    
    public static GameObject QuestUI;
    public static Camera TemporaryQuestCamera;
    public static Player Player;
    
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
        QuestUI.SetActive(false);
        TemporaryQuestCamera.gameObject.SetActive(false);
        Player.GetCharacter().gameObject.SetActive(true);
        Time.timeScale = 1f;
        Player.IncreaseTotalAnswersCounter();
    }
}
