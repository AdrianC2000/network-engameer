using UnityEngine;

public class Collider : MonoBehaviour
{
    [SerializeField] 
    private GameObject questUI;
    [SerializeField]
    private Camera temporaryQuestCamera;
    
    public static CharacterController Character;
    public static bool FirstTimeStepped = true;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Router") && FirstTimeStepped)
        {
            FirstQuest.QuestUI = questUI;
            FirstQuest.TemporaryQuestCamera = temporaryQuestCamera;
            FirstQuest.Character = Character;
            
            questUI.SetActive(true);
            Character.gameObject.SetActive(false);
            temporaryQuestCamera.gameObject.SetActive(true);
            
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}