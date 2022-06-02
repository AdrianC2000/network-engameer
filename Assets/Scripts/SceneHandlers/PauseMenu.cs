using CharacterActions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenuUI;
    [SerializeField]
    private GameObject statisticsMenuUI;
    [SerializeField]
    private GameObject temporaryQuestCamera;
    public static Player Player; 
    public static bool IsGamePaused;
    public static GameObject FPSController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !Collider.IsQuestOn)
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
    }

    public void Resume()
    {
        FPSController.GetComponent<FirstPersonController>().enabled = true;
        pauseMenuUI.SetActive(false);
        Player.GetCharacter().enabled = true;
        Time.timeScale = 1f;
        IsGamePaused = false;
    }

    private void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        FPSController.GetComponent<FirstPersonController>().enabled = false;
        Player.GetCharacter().enabled = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void QuitGame()
    {
        statisticsMenuUI.SetActive(false);
        Player.GetCharacter().gameObject.SetActive(false);
        temporaryQuestCamera.gameObject.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Menu");
    }
}
