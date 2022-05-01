using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Collider : MonoBehaviour
{
    public GameObject questUI;
    public Camera NewCamera;
    public static CharacterController Character;
    public static bool FirstTimeStepped = true;
    
    private void OnCollisionEnter(Collision collision)
    {
        FirstQuest.questUI = questUI;
        FirstQuest.NewCamera = NewCamera;
        FirstQuest.Character = Character;

        if (collision.gameObject.CompareTag("Router") && FirstTimeStepped)
        {
            questUI.SetActive(true);
            Character.gameObject.SetActive(false);
            NewCamera.gameObject.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}