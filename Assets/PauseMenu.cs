using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI; // Assign the PauseUI GameObject in the Inspector
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f; // Resume time
        isPaused = false;
    }

    public void PauseGame()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f; // Pause time
        isPaused = true;
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // Ensure time scale resets
        SceneManager.LoadScene("MainMenu"); // Replace with your Main Menu scene name
    }
}