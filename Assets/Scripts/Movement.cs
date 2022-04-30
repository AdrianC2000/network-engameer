using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] CharacterController character;
    private Vector3 startingPosition; 
    
    void Start()
    {
        startingPosition = character.transform.position;
    }
    void Update()
    {
        RespawnIfFallen();
        RespawnAfterQuest();
    }

    void RespawnIfFallen()
    {
        if (character.transform.position.y <= 0)
        {
            character.enabled = false;
            character.transform.position = startingPosition;
            character.enabled = true;
        }
    }
    
    void RespawnAfterQuest()
    {
        if (StaticClass.PlayerPosition != new Vector3(0, 0, 0))
        {
            character.enabled = false;
            character.transform.position = StaticClass.PlayerPosition;
            character.enabled = true;
            StaticClass.PlayerPosition = new Vector3(0, 0, 0);
        }
    }
}
