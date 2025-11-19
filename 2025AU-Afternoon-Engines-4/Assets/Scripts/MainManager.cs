using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Dictionary<string, bool> ProgressTracker = new Dictionary<string, bool>();// a dictionary for tracking if a flag/puzzle has been completed

    [SerializeField] private PlayerInputHandler playerInputHandler;

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

    private void Start()
    {
        // for tracking progress, brackets for organization in IDE
        {
            ProgressTracker.Add("puzzle01", false);
            ProgressTracker.Add("puzzle02", false);
        }

        /* initialize true variable targets
        {
            WinScreen = WinScreen.GetComponentsInChildren();
        }*/
        
    }

    public void GameWin()
    {
        
    }

    /*private void Start()
    {
        if (MainManager.Instance != null)
        {
            //this would be in the Start() method of other scripts
        }
    }*/
}
