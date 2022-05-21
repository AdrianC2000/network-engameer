using System;
using System.Linq;
using CharacterActions;
using UnityEngine;

public class Collider : MonoBehaviour
{
    [SerializeField] 
    private GameObject questUI;
    [SerializeField] 
    private GameObject questInputUI;
    [SerializeField]
    private Camera temporaryQuestCamera;
    [SerializeField]
    private GameObject statisticsMenuUI;

    private CharacterController _character;
    public static Player Player;
    public static string ActualQuestDeviceName;
    public static bool IsQuestOn; 

    private void OnCollisionEnter(Collision collision)
    {
        if (!Player.GetUsedQuestDevices().Contains(collision.gameObject.name))
        {
            GameObject movingElement;
            // Nothing will be loaded if question on that device has already been answered correctly
            if (collision.gameObject.CompareTag("Firewall"))
            {
                try
                {
                    String tag = "MovingElement" + collision.gameObject.name.Last();
                    movingElement = collision.gameObject.transform.Find(tag).gameObject;
                }
                catch (Exception)
                {
                    movingElement = null;
                }

                _character = Player.GetCharacter();
                Quest.TemporaryQuestCamera = temporaryQuestCamera;
                Quest.Player = Player;

                if (Player.GetFirstQuestCall())
                {
                    // Second step on the device and every subsequent one
                    questUI.SetActive(true);
                    statisticsMenuUI.SetActive(false);
                    IsQuestOn = true;
                    _character.gameObject.SetActive(false);
                    temporaryQuestCamera.gameObject.SetActive(true);

                    Time.timeScale = 0f;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else 
                {
                    // First step on the device -> loading and drawing the quest
                    Quest actualQuest = new Quest(movingElement, questUI);
                    actualQuest.DrawTask(3);
                    Player.SetFirstQuestCall(true);
                    ActualQuestDeviceName = collision.gameObject.name;

                    questUI.SetActive(true);
                    statisticsMenuUI.SetActive(false);
                    IsQuestOn = true;
                    _character.gameObject.SetActive(false);
                    temporaryQuestCamera.gameObject.SetActive(true);

                    Time.timeScale = 0f;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
            if (collision.gameObject.CompareTag("Router"))
            {
                try
                {
                    String tag = "MovingElement" + collision.gameObject.name.Last();
                    movingElement = collision.gameObject.transform.Find(tag).gameObject;
                }
                catch (Exception)
                {
                    movingElement = null;
                }

                _character = Player.GetCharacter();
                QuestInput.TemporaryQuestCamera = temporaryQuestCamera;
                QuestInput.Player = Player;
                
                if (Player.GetFirstQuestCall())
                {
                    // Second step on the device and every subsequent one
                    questInputUI.SetActive(true);
                    statisticsMenuUI.SetActive(false);
                    IsQuestOn = true;
                    _character.gameObject.SetActive(false);
                    temporaryQuestCamera.gameObject.SetActive(true);

                    Time.timeScale = 0f;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else 
                {
                    // First step on the device -> loading and drawing the quest
                    QuestInput actualQuest = new QuestInput(movingElement, questInputUI);
                    actualQuest.DrawTask();
                    Player.SetFirstQuestCall(true);
                    ActualQuestDeviceName = collision.gameObject.name;

                    questInputUI.SetActive(true);
                    statisticsMenuUI.SetActive(false);
                    IsQuestOn = true;
                    _character.gameObject.SetActive(false);
                    temporaryQuestCamera.gameObject.SetActive(true);

                    Time.timeScale = 0f;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        _character = Player.GetCharacter();
        if (other.CompareTag("ParentObject"))
        {
            _character.transform.parent = other.transform;
        }
    }
    
    private void OnTriggerExit(UnityEngine.Collider other)
    {
        if (other.CompareTag("ParentObject"))
        {
            _character.transform.parent = null;
        }
    }
}