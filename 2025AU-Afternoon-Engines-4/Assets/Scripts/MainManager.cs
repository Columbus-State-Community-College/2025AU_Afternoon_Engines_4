using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Dictionary<string, bool> ProgressTracker = new Dictionary<string, bool>();// a dictionary for tracking if a flag/puzzle has been completed

    [SerializeField] private PlayerInputHandler playerInputHandler;

    [Header("Game-Pausing Screens")]
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private GameObject WinScreen;
    [SerializeField] private GameObject LoseScreen;

    public static bool isPaused;

    private void Awake()
    {
        if (Instance != null) // ensures that mainManager remains a singleton; we only want one instance of it travelling between scenes, not creating extra new instances each scene
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    // Brackets used for IDE organization in several instances
    private void Start()
    {
        //set pause state
        isPaused = false;

        // for tracking progress
        {
            ProgressTracker.Add("puzzle01", false);
            ProgressTracker.Add("puzzle02", false);
        }

        /*/ Prepare Game-Pausing Screens
        {
            //PauseScreen = Instantiate(PauseScreen);
            //WinScreen = Instantiate(WinScreen);
            //LoseScreen = Instantiate(LoseScreen);
            PauseScreen.SetActive(false);
            WinScreen.SetActive(false);
            LoseScreen.SetActive(false);
            
            
        }*/
        
    }

    void Update()
    {
        if (!isPaused)  // while not paused
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else            // while paused
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void PauseGameScreen()
    {
        Debug.LogAssertion("MainManager hears the pause call");
        if (!isPaused)
        {
            PauseScreen.SetActive(true);
            isPaused = true;
        }
        else
        {
            PauseScreen.SetActive(false);
            isPaused = false;
        }
        
    }

    public void WinGameScreen()
    {
        WinScreen.SetActive(true);
    }

    public void LoseGameScreen()
    {
        LoseScreen.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("MainMenu");
        /* TODO reset progress
        foreach (var item in ProgressTracker)
        {
            // 
        }*/
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    


}
