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
    [SerializeField] private string interact = "Interact";
    //[SerializeField] private string crouch = "Crouch";
    [SerializeField] private string jump = "Jump";
    //[SerializeField] private string previous = "Previous";
    //[SerializeField] private string next = "Next";
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string store = "Store";
    // Had to name the "throw" action as "drop" because unity apperantly uses "throw" for something
    [SerializeField] private string drop = "Drop";
    [SerializeField] private string cycle = "Cycle";
    [SerializeField] private string inventory = "Inventory";
    [SerializeField] private string swap = "Swap";

    private InputAction moveAction;
    private InputAction lookAction;
    //private InputAction holdAction;
    private InputAction interactAction;
    //private InputAction crouchAction;
    private InputAction jumpAction;
    //private InputAction previousAction;
    //private InputAction nextAction;
    private InputAction sprintAction;
    private InputAction storeAction;
    private InputAction dropAction;
    private InputAction cycleAction;
    private InputAction inventoryAction;
    private InputAction swapAction;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool InteractTriggered { get; set; }
    public bool JumpTriggered { get; private set; }
    public bool SprintTriggered { get; private set; }
    public bool StoreTriggered { get; set; }
    public bool DropTriggered { get; set; }
    public bool CycleTriggered { get; set; }
    public bool InventoryTriggered { get; set; }
    public bool SwapTriggered { get; set; }
    

    private void Awake()
    {
        InputActionMap mapReference = playerControls.FindActionMap(actionMapName);
        moveAction = mapReference.FindAction(move);
        lookAction = mapReference.FindAction(look);
        interactAction = mapReference.FindAction(interact);
        jumpAction = mapReference.FindAction(jump);
        sprintAction = mapReference.FindAction(sprint);
        storeAction = mapReference.FindAction(store);
        dropAction = mapReference.FindAction(drop);
        cycleAction = mapReference.FindAction(cycle);
        inventoryAction = mapReference.FindAction(inventory);
        swapAction = mapReference.FindAction(swap);

        SubscribeActionValuesToInputEvents();
    }

    private void SubscribeActionValuesToInputEvents()
    {
        moveAction.performed += inputInfo => MoveInput = inputInfo.ReadValue<Vector2>();
        moveAction.canceled += inputInfo => MoveInput = Vector2.zero;

        lookAction.performed += inputInfo => LookInput = inputInfo.ReadValue<Vector2>();
        lookAction.canceled += inputInfo => LookInput = Vector2.zero;

        interactAction.performed += inputInfo => InteractTriggered = true;
        interactAction.canceled += inputInfo => InteractTriggered = false;

        jumpAction.performed += inputInfo => JumpTriggered = true;
        jumpAction.canceled += inputInfo => JumpTriggered = false;

        sprintAction.performed += inputInfo => SprintTriggered = true;
        sprintAction.canceled += inputInfo => SprintTriggered = false;

        storeAction.performed += inputInfo => StoreTriggered = true;
        storeAction.canceled += inputInfo => StoreTriggered = false;

        dropAction.performed += inputInfo => DropTriggered = true;
        dropAction.canceled += inputInfo => DropTriggered = false;

        cycleAction.performed += inputInfo => CycleTriggered = true;
        cycleAction.canceled += inputInfo => CycleTriggered = false;

        inventoryAction.performed += inputInfo => InventoryTriggered = true;
        inventoryAction.canceled += inputInfo => InventoryTriggered = false;

        swapAction.performed += inputInfo => SwapTriggered = true;
        swapAction.canceled += inputInfo => SwapTriggered = false;
    }

    public void OnEnable()
    {
        //playerControls.FindActionMap(actionMapName).Enable();
        moveAction.Enable();
        lookAction.Enable();
        interactAction.Enable();
        jumpAction.Enable();
        sprintAction.Enable();
        storeAction.Enable();
        dropAction.Enable();
    }

    public void OnDisable()
    {
        //playerControls.FindActionMap(actionMapName).Disable();
        moveAction.Disable();
        lookAction.Disable();
        interactAction.Disable();
        jumpAction.Disable();
        sprintAction.Disable();
        storeAction.Disable();
        dropAction.Disable();
    }

}