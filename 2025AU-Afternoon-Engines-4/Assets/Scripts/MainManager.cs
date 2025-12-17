using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    // A class for tracking Puzzle objects
    public class Puzzle
    {
        public string name;
        public bool solvedStatus = false;
        public string hint;

        // Constructor
        public Puzzle(string puzzleName, string puzzleHint)
        {
            name = puzzleName;
            hint = puzzleHint;
        }
    }
    public List<Puzzle> ProgressTracker = new List<Puzzle>();// a dictionary for tracking if a flag/puzzle has been completed
    //private bool FinalPuzzleSolved; // not quite working

    [SerializeField] private GameObject PlayerObject;

    [Header("Game-Pausing Screens")]
    [SerializeField] public GameObject PauseScreen;
    [SerializeField] public GameObject WinScreen;
    [SerializeField] public GameObject LoseScreen;

    [Header("Scene Event System")]
    [SerializeField] public EventSystem EventSystem;

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
        /*if (FinalPuzzleSolved == true) // not quite working
        {
            WinGameScreen();
        }*/
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
        SceneManager.LoadScene("MenuScene");
        
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

        // progress trackers are stored in Puzzle class objects with the pattern <[puzzleName], [solvedStatus], [puzzleHint]> 
        List<Puzzle> FreshProgressTracker = new List<Puzzle>();
        {
            // Water Level Puzzles
            {
                FreshProgressTracker.Add(new Puzzle("TubePuzzle01","Bring the tubes to the peg board."));
                FreshProgressTracker.Add(new Puzzle("TubePuzzle02","Interact with the green switch on the wall and position the tubes by color."));
                FreshProgressTracker.Add(new Puzzle("TreasureChestLockPuzzle01", "The chest is closed with a rolling combination lock."));
                //FreshProgressTracker.Add(new Puzzle("TreasureChestNumbers01","Roll the numbers accordingly to unlock Treasure Chest."));
                FreshProgressTracker.Add(new Puzzle("TrapDoorKeyHolePuzzle01","The key has a lock on the wall."));
                //FreshProgressTracker.Add(new Puzzle("TrapDoorKey","Key Hole to unlock Trap Door above ladder. Use Key to unlock."));
                //FreshProgressTracker.Add(new Puzzle("Ladder","Ladder to Trap Door. Unlock Wall Key Hole to gain access to next room."));
            }
            // Wild West Level Puzzles
            {
                FreshProgressTracker.Add(new Puzzle("FigureBoardPuzzle01","Find 3 figurines around the room and drop them on the table! Balls can be thrown with RMB."));
                FreshProgressTracker.Add(new Puzzle("FigureBoardPuzzle02","Arrange the figures on the table to unlock the door."));
                //FreshProgressTracker.Add(new Puzzle("HorseRacingPuzzle","TBD."));
            }
            // Vamp Level Puzzles
            {
                FreshProgressTracker.Add(new Puzzle("MannequinPuzzle01","Assemble the mannequin! Drop each piece you find on the limb it should connect to."));
                /* FreshProgressTracker.Add(new Puzzle("MannequinPuzzle02","Assemble the mannequin!"));
                FreshProgressTracker.Add(new Puzzle("MannequinPuzzle03","Assemble the mannequin!")); */
            }
            
        }
        
        ProgressTracker = FreshProgressTracker;
        //FinalPuzzleSolved =  ProgressTracker.ElementAt(ProgressTracker.Count - 1).solvedStatus; // not quite workin as intended, may implement differently
    }

    public string CurrentHint()
    {
        string neededHint = "";
        foreach (var Hint in ProgressTracker)
        {
            if (Hint.solvedStatus == false)
            {
                neededHint = Hint.hint;
                break;
            }
            else
            {
                continue;
            }
            
        }
        return neededHint;
    }

    public void SolvePuzzle(string solvedPuzzleName)
    {
        foreach (var Puzzle in ProgressTracker)
        {
            if (solvedPuzzleName == Puzzle.name)
            {
                Puzzle.solvedStatus = true;
            }
        }

    }


    


}
