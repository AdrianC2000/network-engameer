using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstQuest : MonoBehaviour
{
    public static GameObject questUI;
    public static Camera NewCamera;
    public static CharacterController Character; 
    
    public void Resume()
    {
        questUI.SetActive(false);
        NewCamera.gameObject.SetActive(false);
        Character.gameObject.SetActive(true);
        Time.timeScale = 1f;
        Collider.FirstTimeStepped = false;
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }
}
