using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstQuest : MonoBehaviour
{
    // [SerializeField] 
    // private GameObject emptyObject; 
    [SerializeField] 
    private GameObject movingFirewall;
    
    public static GameObject QuestUI;
    public static Camera TemporaryQuestCamera;
    public static CharacterController Character; 
    
    public void Resume()
    {
        // emptyObject.SetActive(true);
        movingFirewall.SetActive(true);
        
        QuestUI.SetActive(false);
        TemporaryQuestCamera.gameObject.SetActive(false);
        Character.gameObject.SetActive(true);
        Time.timeScale = 1f;
        Collider.FirstTimeStepped = false;
    }
}
