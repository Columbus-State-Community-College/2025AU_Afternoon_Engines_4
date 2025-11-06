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

    void Update()
    {
        if (Input.GetKeyDown(moveObjectBackwardKey))
        {
            CakeObject.transform.Translate(0, 0, -zOffset);
        }

        else if (Input.GetKeyDown(moveObjectForwardKey))
        {
            CakeObject.transform.Translate(0, 0, zOffset);
        }

        else if (Input.GetKeyDown(moveObjectLeftKey))
        {
            CakeObject.transform.Translate(-xOffset, 0, 0);
        }

        else if (Input.GetKeyDown(moveObjectRightKey))
        {
            CakeObject.transform.Translate(xOffset, 0, 0);
        }

        else if (Input.GetKeyDown(moveObjectUpKey))
        {
            CakeObject.transform.Translate(0, yOffset, 0);
        }

        else if (Input.GetKeyDown(moveObjectDownKey))
        {
            CakeObject.transform.Translate(0, -yOffset, 0);
        }
    }
}
