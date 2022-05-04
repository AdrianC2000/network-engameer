using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController character;
    private Vector3 startingPosition;

    void Start()
    {
        PauseMenu.CharacterCamera = character.GetComponentInChildren<Camera>();
        Collider.Character = character;
        startingPosition = character.transform.position;
    }
    void Update()
    {
        RespawnIfFallen();
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
}
