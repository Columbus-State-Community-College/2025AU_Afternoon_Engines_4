using UnityEngine;

// Put this script onto a GameObject that you want to move and/or rotate when a puzzle is completed
// Then drag that GameObject into the variable slot in the cooresponding "PuzzleLock" script

public class PuzzleLinkedMovement : MonoBehaviour
{
    [Header("Main Variables")]
    [Tooltip("Activates the rotation logic")]
    public bool willRotate = false;
    [Tooltip("Activates the moving logic")]
    public bool willMove = false;

    [Header("Rotation Controls")]
    [Tooltip("Rotation speed")]
    public float rotationSpeed = 10;
    [Tooltip("X-axis rotation")]
    public float rotationX = 0;
    [Tooltip("Y-axis rotation")]
    public float rotationY = 0;
    [Tooltip("Z-axis rotation")]
    public float rotationZ = 0;

    [Header("Movement Controls")]
    [Tooltip("Movement speed")]
    public float movementSpeed = 1;
    [Tooltip("X-axis movement")]
    public float movementX = 0;
    [Tooltip("Y-axis movement")]
    public float movementY = 0;
    [Tooltip("Z-axis movement")]
    public float movementZ = 0;
    private Vector3 objectPosition;
    private Vector3 objectRotationPosition;
    private Vector3 rotationCalc;
    private Quaternion rotationTarget;
    private Vector3 movementCalc;

    private bool scriptCompleted = false;
    private bool rotationCompleted = false;
    private bool movementCompleted = false;
    [HideInInspector]
    public bool linkedPuzzleCompleted = false;

    void Start()
    {
        objectPosition = gameObject.transform.position;
        objectRotationPosition = gameObject.transform.eulerAngles;
        movementCalc = new Vector3(movementX, movementY, movementZ) + objectPosition;
        rotationCalc = new Vector3(rotationX, rotationY, rotationZ) + objectRotationPosition;
        rotationTarget = Quaternion.Euler(rotationCalc);
    }

    void Update()
    {
        if (linkedPuzzleCompleted && !scriptCompleted)
        {
            if (willRotate && !rotationCompleted)
            {
                ObjectRotation();
            }
            if (willMove && !movementCompleted)
            {
                ObjectMovement();
            }
            else
            {
                scriptCompleted = true;
            }
        }
    }

    void ObjectRotation()
    {
        float rotationThisFrame = rotationSpeed * Time.deltaTime;

        gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, rotationTarget, rotationThisFrame);

        if (Quaternion.Angle(transform.rotation, rotationTarget) < 0.1f)
        {
            rotationCompleted = true;
            gameObject.transform.rotation = rotationTarget;
        }
    }

    void ObjectMovement()
    {
        float movementThisFrame = movementSpeed * Time.deltaTime;

        gameObject.transform.position = Vector3.MoveTowards(transform.position, movementCalc, movementThisFrame);

        if (Vector3.Distance(transform.position, movementCalc) < 0.1f)
        {
            movementCompleted = true;
            gameObject.transform.position = movementCalc;
        }
    }
}
