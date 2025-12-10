using UnityEngine;
using System.Collections.Generic;

public class PuzzleView3 : MonoBehaviour
{
    [Header("Only assign one of these variables")]
    [Tooltip("This rotates an object on the X-axis by this amount")]
    public float xRotation = 0.0f;
    [Tooltip("This rotates an object on the Y-axis by this amount")]
    public float yRotation = 0.0f;
    [Tooltip("This rotates an object on the Z-axis by this amount")]
    public float zRotation = 0.0f;
    
    [Tooltip("This object will be rotated by the Puzzle View.")]
    public List<GameObject> puzzleObjects = new List<GameObject>();
    // currentPuzzleObject = the index for the above List
    private int currentPuzzleObject = 0;

    private KeyCode moveObjectForwardKey = KeyCode.UpArrow;
    private KeyCode moveObjectBackwardKey = KeyCode.DownArrow;
    private KeyCode switchPuzzlePiece = KeyCode.Q; // This can be changed to whatever
    private KeyCode moveObjectForwardGamePad = KeyCode.JoystickButton3;
    private KeyCode moveObjectBackwardGamePad = KeyCode.JoystickButton0;
    //private KeyCode switchPuzzlePieceGamePad = KeyCode.JoystickButton - idk what button to put here

    void Update()
    {
        if (puzzleObjects != null)
        {
            if (Input.GetKeyDown(switchPuzzlePiece)) //  || (Input.GetKeyDown(moveObjectBackwardGamePad))
            {
                currentPuzzleObject++;
                if (currentPuzzleObject >= puzzleObjects.Count)
                {
                    currentPuzzleObject = 0;
                }
            }
            MoveObject(puzzleObjects[currentPuzzleObject]);
        }
    }

    void MoveObject(GameObject puzzlePiece)
    {
        if (Input.GetKeyDown(moveObjectForwardKey) || (Input.GetKeyDown(moveObjectForwardGamePad)))
        {
            puzzlePiece.transform.Rotate(xRotation, yRotation, zRotation, Space.World);
        }
        else if (Input.GetKeyDown(moveObjectBackwardKey) || (Input.GetKeyDown(moveObjectBackwardGamePad)))
        {
            puzzlePiece.transform.Rotate(-xRotation, -yRotation, -zRotation, Space.World);
        }   
    }
}