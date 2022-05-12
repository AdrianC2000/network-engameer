using CharacterActions;
using UnityEngine;

public class Collider : MonoBehaviour
{
    [SerializeField] 
    private GameObject questUI;
    [SerializeField]
    private Camera temporaryQuestCamera;

    private CharacterController _character;
    public static Player Player;
    public static bool FirstTimeStepped = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Router") && FirstTimeStepped)
        {
            _character = Player.GetCharacter();
            Quest.TemporaryQuestCamera = temporaryQuestCamera;
            Quest.Player = Player;
            
            questUI.SetActive(true);
            _character.gameObject.SetActive(false);
            temporaryQuestCamera.gameObject.SetActive(true);
            
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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