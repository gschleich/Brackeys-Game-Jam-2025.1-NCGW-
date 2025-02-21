using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    // Call this for the "Yes" button
    public void PlayAgain()
    {
        // Reloads the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Call this for the "No" button
    public void ReturnToMainMenu()
    {
        // Load the Main Menu scene (replace "MainMenu" with your actual scene name)
        SceneManager.LoadScene("MainMenu");
    }
}