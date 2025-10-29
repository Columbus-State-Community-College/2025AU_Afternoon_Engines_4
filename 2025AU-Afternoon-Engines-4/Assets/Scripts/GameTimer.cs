using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float timeLeft = 60.0f;
    public TextMeshProUGUI timerText;
    private bool timerIsRunning = false;

    void Start()
    {
        timerIsRunning = true;
    }


    void Update()
    {
        if (timerIsRunning)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                UpdateTimerDisplay(timeLeft);
            }
            else
            {
                timerIsRunning = false;
                GameOver();
            }
        }
    }

    void UpdateTimerDisplay(float timeDisplay)
    {
        float minutes = Mathf.FloorToInt(timeDisplay / 60);
        float seconds = Mathf.FloorToInt(timeDisplay % 60);
        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }

    void GameOver()
    {
        timerText.text = "Game Over!";
        Time.timeScale = 0; // Pause the game
        // Possible Idea: SceneManager.LoadScene("GameOverScene"); // Load up a new scene when you get a game over
    }
}
