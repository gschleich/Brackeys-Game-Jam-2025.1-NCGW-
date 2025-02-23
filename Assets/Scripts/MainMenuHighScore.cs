using UnityEngine;
using TMPro; // Use TextMeshPro for the UI

public class MainMenuHighScore : MonoBehaviour
{
    public TextMeshProUGUI highScoreText; // Assign this in the Inspector

    void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        Debug.Log("Loaded High Score: " + highScore); // Add this line for debugging
        highScoreText.text = "HighScore: " + highScore;
    }
}
