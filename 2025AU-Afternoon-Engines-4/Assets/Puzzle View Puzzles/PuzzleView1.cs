using UnityEngine;
using System.Collections.Generic;

public class PuzzleView1 : MonoBehaviour
{
    [Tooltip("This moves an object left and right")]
    public float xOffset = 0.5f;
    [Tooltip("This moves an object up and down")]
    public float yOffset = 0.5f;
    [Tooltip("This moves an object forwards and backwards")]
    public float zOffset = 0.5f;

    [Tooltip("This object will be moved by the Puzzle View.")]
    public List<GameObject> puzzleObjects = new List<GameObject>();
    // currentPuzzleObject = the index for the above List
    private int currentPuzzleObject = 0;

    private KeyCode moveObjectForwardKey = KeyCode.UpArrow;
    private KeyCode moveObjectBackwardKey = KeyCode.DownArrow;
    private KeyCode moveObjectLeftKey = KeyCode.LeftArrow;
    private KeyCode moveObjectRightKey = KeyCode.RightArrow;
    private KeyCode moveObjectUpKey = KeyCode.A;
    private KeyCode moveObjectDownKey = KeyCode.S;
    private KeyCode switchPuzzlePiece = KeyCode.Q; // This can be changed to whatever
    private KeyCode moveObjectForwardGamePad = KeyCode.JoystickButton3;
    private KeyCode moveObjectBackwardGamePad = KeyCode.JoystickButton0;
    private KeyCode moveObjectLeftGamePad = KeyCode.JoystickButton2;
    private KeyCode moveObjectRightGamePad = KeyCode.JoystickButton1;
    private KeyCode moveObjectUpGamePad = KeyCode.JoystickButton6;
    private KeyCode moveObjectDownGamePad = KeyCode.JoystickButton7;
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
        if (Input.GetKeyDown(moveObjectBackwardKey) || (Input.GetKeyDown(moveObjectBackwardGamePad)))
        {
            puzzlePiece.transform.Translate(0, 0, -zOffset);
        }

        else if (Input.GetKeyDown(moveObjectForwardKey) || (Input.GetKeyDown(moveObjectForwardGamePad)))
        {
            puzzlePiece.transform.Translate(0, 0, zOffset);
        }

        else if (Input.GetKeyDown(moveObjectLeftKey) || (Input.GetKeyDown(moveObjectLeftGamePad)))
        {
            puzzlePiece.transform.Translate(-xOffset, 0, 0);
        }

        else if (Input.GetKeyDown(moveObjectRightKey) || (Input.GetKeyDown(moveObjectRightGamePad)))
        {
            puzzlePiece.transform.Translate(xOffset, 0, 0);
        }

        else if (Input.GetKeyDown(moveObjectUpKey) || (Input.GetKeyDown(moveObjectUpGamePad)))
        {
            puzzlePiece.transform.Translate(0, yOffset, 0);
        }

        else if (Input.GetKeyDown(moveObjectDownKey) || (Input.GetKeyDown(moveObjectDownGamePad)))
        {
            puzzlePiece.transform.Translate(0, -yOffset, 0);
        }
    }
}
