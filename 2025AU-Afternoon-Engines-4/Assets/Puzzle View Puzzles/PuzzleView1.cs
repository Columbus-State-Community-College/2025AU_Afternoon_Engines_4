using UnityEngine;

public class PuzzleView1 : MonoBehaviour
{
    // This moves an object left and right
    private float xOffset = 1f;
    // This moves an object up and down
    private float yOffset = 1f;
    // This moves an object forwards and backwards
    private float zOffset = 1f;

    [Tooltip("This object will be moved by the Puzzle View.")]
    public GameObject CakeObject;

    private KeyCode moveObjectForwardKey = KeyCode.UpArrow;
    private KeyCode moveObjectBackwardKey = KeyCode.DownArrow;
    private KeyCode moveObjectLeftKey = KeyCode.LeftArrow;
    private KeyCode moveObjectRightKey = KeyCode.RightArrow;
    private KeyCode moveObjectUpKey = KeyCode.A;
    private KeyCode moveObjectDownKey = KeyCode.S;
    private KeyCode moveObjectForwardGamePad = KeyCode.JoystickButton3;
    private KeyCode moveObjectBackwardGamePad = KeyCode.JoystickButton0;
    private KeyCode moveObjectLeftGamePad = KeyCode.JoystickButton2;
    private KeyCode moveObjectRightGamePad = KeyCode.JoystickButton1;
    private KeyCode moveObjectUpGamePad = KeyCode.JoystickButton6;
    private KeyCode moveObjectDownGamePad = KeyCode.JoystickButton7;

    void Update()
    {
        if (Input.GetKeyDown(moveObjectBackwardKey) || (Input.GetKeyDown(moveObjectBackwardGamePad)))
        {
            CakeObject.transform.Translate(0, 0, -zOffset);
        }

        else if (Input.GetKeyDown(moveObjectForwardKey) || (Input.GetKeyDown(moveObjectForwardGamePad)))
        {
            CakeObject.transform.Translate(0, 0, zOffset);
        }

        else if (Input.GetKeyDown(moveObjectLeftKey) || (Input.GetKeyDown(moveObjectLeftGamePad)))
        {
            CakeObject.transform.Translate(-xOffset, 0, 0);
        }

        else if (Input.GetKeyDown(moveObjectRightKey) || (Input.GetKeyDown(moveObjectRightGamePad)))
        {
            CakeObject.transform.Translate(xOffset, 0, 0);
        }

        else if (Input.GetKeyDown(moveObjectUpKey) || (Input.GetKeyDown(moveObjectUpGamePad)))
        {
            CakeObject.transform.Translate(0, yOffset, 0);
        }

        else if (Input.GetKeyDown(moveObjectDownKey) || (Input.GetKeyDown(moveObjectDownGamePad)))
        {
            CakeObject.transform.Translate(0, -yOffset, 0);
        }
    }
}
