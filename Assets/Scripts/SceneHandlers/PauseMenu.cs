using CharacterActions;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenuUI;

    public static Player Player; 
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
            Player.GetCharacter().GetComponentInChildren<Camera>().transform.rotation = _cameraRotation;
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _isGamePaused = false;
    }

    private void Pause()
    {
        _cameraRotation = Player.GetCharacter().GetComponentInChildren<Camera>().transform.rotation;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _isGamePaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
