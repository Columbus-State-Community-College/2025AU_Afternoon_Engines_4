using UnityEngine;
using System.Collections.Generic;

// Put this script onto the "lock" that will accept the "keys"
// The lock has to be an "isTrigger" collider

public class PuzzleLock : MonoBehaviour
{
    [Tooltip("Set the tag for the 'keys' (GameObjects can only have 1 tag)")]
    public string puzzleKeyTag = "Key01";
    [Tooltip("Set the total amount of keys needed to complete the puzzle")]
    public int keyTotalAmount = 1;
    private int keyCurrentAmount = 0;
    [Tooltip("Link the GameObject that will move when this puzzle is completed (& has the movement script on it)")]
    public List<GameObject> linkedMovementObjects = new List<GameObject>();
    private List<PuzzleLinkedMovement> linkedMovementScript = new List<PuzzleLinkedMovement>();

    // Originally was gonna use this variable for linking (thats why its public & hidden) but never used it for that
    // Leaving it as such instead of changing to private incase it becomes useful later
    [HideInInspector]
    public bool puzzleCompleted = false;
    //[HideInInspector]
    //public Diction

    void Start()
    {
        if (linkedMovementObjects != null)
        {
            int i = 0;
            foreach (GameObject movementObject in linkedMovementObjects)
            {
                linkedMovementScript.Add(movementObject.GetComponent<PuzzleLinkedMovement>());
                i++;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (puzzleCompleted == false && other.CompareTag(puzzleKeyTag))
        {
            PuzzleLogic();
            Destroy(other.gameObject);
        }
        // Debugging
        else if (puzzleCompleted == false)
        {
            Debug.Log("Wrong object for this puzzle.");
        }
        else
        {
            Debug.Log("Puzzle already completed");
        }
    }

    void PuzzleLogic()
    {
        keyCurrentAmount++;
        if (keyCurrentAmount < keyTotalAmount)
        {
            // Debugging
            Debug.Log("You have " + keyCurrentAmount + " 'keys' out of " + keyTotalAmount + " for this puzzle.");
            return;
        }
        else
        {
            int i = 0;
            foreach (GameObject movementObject in linkedMovementObjects)
            {
                linkedMovementScript[i].linkedPuzzleCompleted = true;
                i++;
            }
            puzzleCompleted = true;
            MainManager.Instance.ProgressTracker["puzzle01"] = true;
            //for testing
            var puzzle01value = MainManager.Instance.ProgressTracker["puzzle01"];
            Debug.Log("puzzle01 is now tracked as " + puzzle01value);
        }
    }
}