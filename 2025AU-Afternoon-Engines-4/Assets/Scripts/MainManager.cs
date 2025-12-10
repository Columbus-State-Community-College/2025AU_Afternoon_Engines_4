using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Dictionary<string, bool> ProgressTracker = new Dictionary<string, bool>();// a dictionary for tracking if a flag/puzzle has been completed

    [SerializeField] private GameObject PlayerObject;

    [Header("Game-Pausing Screens")]
    [SerializeField] public GameObject PauseScreen;
    [SerializeField] public GameObject WinScreen;
    [SerializeField] public GameObject LoseScreen;

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
            InitializeProgressTracker();
            
        }

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
        if (ProgressTracker.ElementAt(ProgressTracker.Count - 1).Value == true)
        {
            WinGameScreen();
        }
    }

    public void PauseGameScreen()
    {
        
        if (!isPaused)
        {
            isPaused = true;
            PauseScreen.SetActive(true);
        }
        else
        {
            isPaused = false;
            PauseScreen.SetActive(false);
        }

    }

    public void WinGameScreen()
    {
        isPaused = true;
        WinScreen.SetActive(true);
    }

    public void LoseGameScreen()
    {
        isPaused = true;
        LoseScreen.SetActive(true);
    }
    public void RestartGame()
    {
        isPaused = false;
        ResetProgressTracker();
        SceneManager.LoadScene("MainMenu");
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    //strictly for readability
    private void ResetProgressTracker()
    {
        InitializeProgressTracker();
    }

    //  creates the progress tracker on new game and resets it on restart game 
    private void InitializeProgressTracker()
    {
        Dictionary<string, bool> FreshProgressTracker = new Dictionary<string, bool>();
        {
            FreshProgressTracker.Add("puzzle01", false);
            FreshProgressTracker.Add("puzzle02", false);
        }

        ProgressTracker = FreshProgressTracker;
    }


    


}
