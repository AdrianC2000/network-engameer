using CharacterActions;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenuUI;
    [SerializeField]
    private GameObject statisticsMenuUI;
    public static Player Player; 
    private Quaternion _cameraRotation;
    public static bool IsGamePaused;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused)
            {
                Resume();
            }
            else
            {
                statisticsMenuUI.SetActive(false);
                Pause();
            }
        }
        
        if (IsGamePaused)
        {
            Player.GetCharacter().GetComponentInChildren<Camera>().transform.rotation = _cameraRotation;
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
    }

    private void Pause()
    {
        _cameraRotation = Player.GetCharacter().GetComponentInChildren<Camera>().transform.rotation;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
