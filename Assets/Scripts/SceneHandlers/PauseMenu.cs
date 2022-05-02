using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static Camera CharacterCamera;
    public GameObject pauseMenuUI;
    private Quaternion _cameraRotation;
    private bool _isGamePaused;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
        if (_isGamePaused)
        {
            CharacterCamera.transform.rotation = _cameraRotation;
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _isGamePaused = false;
    }

    void Pause()
    {
        _cameraRotation = CharacterCamera.transform.rotation;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _isGamePaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
