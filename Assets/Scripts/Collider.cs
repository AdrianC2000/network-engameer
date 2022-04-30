using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Router"))
        {
            SceneManager.LoadScene("Quest");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            StaticClass.PlayerPosition = collision.transform.position; 
        }
    }
}
