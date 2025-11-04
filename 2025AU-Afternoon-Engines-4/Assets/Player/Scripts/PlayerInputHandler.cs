using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name Reference")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name References")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string look = "Look";
    //[SerializeField] private string hold = "Hold";
    //[SerializeField] private string interact = "Interact";
    //[SerializeField] private string crouch = "Crouch";
    [SerializeField] private string jump = "Jump";
    //[SerializeField] private string previous = "Previous";
    //[SerializeField] private string next = "Next";
    [SerializeField] private string sprint = "Sprint";

    private InputAction moveAction;
    private InputAction lookAction;
    //private InputAction holdAction;
    //private InputAction interactAction;
    //private InputAction crouchAction;
    private InputAction jumpAction;
    //private InputAction previousAction;
    //private InputAction nextAction;
    private InputAction sprintAction;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpTriggered { get; private set; }
    public bool SprintTriggered { get; private set; }

    private void Awake()
    {
        InputActionMap mapReference = playerControls.FindActionMap(actionMapName);
        moveAction = mapReference.FindAction(move);
        lookAction = mapReference.FindAction(look);
        jumpAction = mapReference.FindAction(jump);
        sprintAction = mapReference.FindAction(sprint);

        SubscriveActionValuesToInputEvents();
    }

    private void SubscriveActionValuesToInputEvents()
    {
        moveAction.performed += inputInfo => MoveInput = inputInfo.ReadValue<Vector2>();
        moveAction.canceled += inputInfo => MoveInput = Vector2.zero;

        lookAction.performed += inputInfo => LookInput = inputInfo.ReadValue<Vector2>();
        lookAction.canceled += inputInfo => LookInput = Vector2.zero;

        jumpAction.performed += inputInfo => JumpTriggered = true;
        jumpAction.canceled += inputInfo => JumpTriggered = false;

        sprintAction.performed += inputInfo => SprintTriggered = true;
        sprintAction.canceled += inputInfo => SprintTriggered = false;
    }

    private void OnEnable()
    {
        playerControls.FindActionMap(actionMapName).Enable();
    }

    private void OnDisable()
    {
        playerControls.FindActionMap(actionMapName).Disable();
    }

}
