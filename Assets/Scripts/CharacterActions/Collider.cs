using System;
using System.Linq;
using CharacterActions;
using TMPro;
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
    [SerializeField] 
    private AudioClip standOnQuest;
    [SerializeField] 
    private GameObject finishLevelUI;
    

    private CharacterController _character;
    public static Player Player;
    public static string ActualQuestDeviceName;
    public static bool IsQuestOn;
    public static Vector3 collidedElementPosition;
    public static AudioClip finish;


    private void OnCollisionEnter(Collision collision)
    {
        if (!Player.GetUsedQuestDevices().Contains(collision.gameObject.name))
        {
            AudioSource audioSource = temporaryQuestCamera.GetComponent<AudioSource>();
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
                    audioSource.clip = standOnQuest;
                    audioSource.Play();

                    Time.timeScale = 0f;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else 
                {
                    // First step on the device -> loading and drawing the quest
                    collidedElementPosition = collision.gameObject.transform.position + new Vector3(0, (float)2.23, 0);
                    Quest actualQuest = new Quest(movingElement, questUI);
                    actualQuest.DrawTask(3);
                    Player.SetFirstQuestCall(true);
                    ActualQuestDeviceName = collision.gameObject.name;

                    questUI.SetActive(true);
                    statisticsMenuUI.SetActive(false);
                    IsQuestOn = true;
                    _character.gameObject.SetActive(false);
                    temporaryQuestCamera.gameObject.SetActive(true);
                    audioSource.clip = standOnQuest;
                    audioSource.Play();

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
                    audioSource.clip = standOnQuest;
                    audioSource.Play();
                    
                    TMP_InputField textField = questInputUI.transform.Find("AnswerInputField").GetComponentInChildren<TMP_InputField>();
                    textField.text = "";
                    textField.Select();
                    textField.ActivateInputField();

                    Time.timeScale = 0f;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else 
                {
                    // First step on the device -> loading and drawing the quest
                    collidedElementPosition = collision.gameObject.transform.position + new Vector3(0, (float)2.23, 0);
                    QuestInput actualQuest = new QuestInput(movingElement, questInputUI);
                    QuestInputHandler.questInput = actualQuest;
                    actualQuest.DrawTask();
                    Player.SetFirstQuestCall(true);
                    ActualQuestDeviceName = collision.gameObject.name;

                    questInputUI.SetActive(true);
                    statisticsMenuUI.SetActive(false);
                    IsQuestOn = true;
                    _character.gameObject.SetActive(false);
                    temporaryQuestCamera.gameObject.SetActive(true);
                    audioSource.clip = standOnQuest;
                    audioSource.Play();
                    
                    TMP_InputField textField = questInputUI.transform.Find("AnswerInputField").GetComponentInChildren<TMP_InputField>();
                    textField.text = "";
                    textField.Select();
                    textField.ActivateInputField();

                    Time.timeScale = 0f;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            finishLevelUI.SetActive(true);
            statisticsMenuUI.SetActive(false);
            _character.gameObject.SetActive(false);
            temporaryQuestCamera.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            AudioSource audioSource = temporaryQuestCamera.GetComponent<AudioSource>();
            audioSource.clip = finish;
            audioSource.Play();
        }
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        _character = Player.GetCharacter();
        if (other.CompareTag("ParentObject"))
        {
            _character.transform.parent = other.transform;
        }
        if (other.CompareTag("FallingElement"))
        {
            if (other.gameObject.GetComponent<Rigidbody>() == null)
            {
                other.gameObject.AddComponent<Rigidbody>();
            }
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