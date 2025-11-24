using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupInventory : MonoBehaviour, IInteractable
{
    [Tooltip("A lower number will require you to be closer to the object to pick it up, while a higher number will allow you to pick up an object from farther away.")] 
    public float pickupRange = 2f;
        // Keybinds now set in the editor via the PlayerInput(Input Action Asset) thingy
    [Tooltip("This is where the object will be held at. Create an empty object, set it near the player, and then bring it over to this empty spot.")] 
    public Transform holdPoint;
    public float throwForce = 500f;
    [Tooltip("Objects with this tag can be picked up")] 
    public string[] objectTagArray;
    [Tooltip("Objects with this tag will bring you to a Puzzle View.")]
    public string[] PuzzleViewTagArray;
    [Tooltip("This key will allow you to exit the Puzzle view and you'll be back to the player's view.")] 
    public KeyCode exitPuzzleViewKey = KeyCode.A;
    public KeyCode exitPuzzleViewGamepad = KeyCode.JoystickButton8;

    private GameObject currentPickupTarget;
    private GameObject heldObject;

    private List<GameObject> inventory = new List<GameObject>();
    private int selectedInventoryIndex = 0;

    public TMP_Text inventoryDisplayText;
    public TMP_Text PuzzleView1ControlsText;
    public TMP_Text PuzzleView2ControlsText;
    public TMP_Text ManikinPartCheckerText;
    public GameObject Player;
    public GameObject PlayerCamera;
    [SerializeField] private PlayerInputHandler playerInputHandler;
    public GameObject PuzzleViewCamera1;
    public GameObject PuzzleViewCamera2;
    public GameObject PuzzleViewManager;
    public InventoryManager inventoryManager;


    [Header("Sounds")]
    [Tooltip("Create a new empty object that has an audio source with it and drag the object into one of the slots in the inspector.")]
    public AudioSource ThrowSound;
    public AudioSource ShuffleSound;
    public AudioSource PuzzleViewEnter;
    public AudioSource PuzzleViewExit;
    public AudioSource PutAwaySound;
    public AudioSource TakeOutSound;

    void Start()
    {
        inventoryDisplayText.gameObject.SetActive(false);
        PuzzleViewManager.GetComponent<PuzzleView1>().enabled = false;
        PuzzleViewManager.GetComponent<PuzzleView2>().enabled = false;
        currentPickupTarget = null;
        heldObject = null;
    }

    void IInteractable.Interact()
    {
        // what happens when something is interacted with
    }

    void Update()
    {
        // Checks for left clicks | picking up items
        if (playerInputHandler.InteractTriggered)
        {
            // Might be able to simplify this after it was changed to raycasts idk
            if (heldObject != null)
            {
                DropObject();
            }
            else if (currentPickupTarget == null)
            {
                DetectObject();
                if (currentPickupTarget != null)
                {
                    PickupObject(currentPickupTarget);
                    currentPickupTarget = null;
                }
            }
            // Set all this "Triggered" to false to simulate a single keyDown() (AKA if you hold it down it won't keep firing)
            playerInputHandler.InteractTriggered = false;
        }

        // Checks for right clicks | throwing items
        if (playerInputHandler.DropTriggered && heldObject != null)
        {
            ThrowObject();
            ThrowSound.Play();
            playerInputHandler.DropTriggered = false;
        }

        // Checks for "E" presses | storing items
        if (playerInputHandler.StoreTriggered)
        {
            if (heldObject != null)
            {
                StoreHeldObject();
                PutAwaySound.Play();
            }
            else if (inventory.Count > 0)
            {
                RetrieveFromInventory();
                TakeOutSound.Play();
            }
            playerInputHandler.StoreTriggered = false;
            inventoryManager.UpdateInventoryUI(inventory);
        }

        // Checks for "F" presses | cycling through items / inventory UI
        if (playerInputHandler.CycleTriggered)
        {
            // Slightly confusing that the hotbar is called inventory in this script | will probably change this week (4)
            if (!inventoryManager.inventoryOpen && inventory.Count > 0)
            {
                InventoryCycle();
                ShuffleSound.Play();
            }
            else
            {
                inventoryManager.CycleSelectorPosition(inventoryManager.inventorySelectorPosition);
            }

            playerInputHandler.CycleTriggered = false;
        }

        if (Input.GetKeyDown(exitPuzzleViewKey) || Input.GetKeyDown(exitPuzzleViewGamepad))
        {
            ExitPuzzleView();
            PuzzleViewExit.Play();
        }

        // Checks for "I" presses | opens and closes main inventory
        if (playerInputHandler.InventoryTriggered)
        {
            if (!inventoryManager.inventoryOpen)
            {
                inventoryManager.OpenInventory();
                playerInputHandler.OnDisable();
            }
            else
            {
                inventoryManager.CloseInventory();
                playerInputHandler.OnEnable();
            }
            playerInputHandler.InventoryTriggered = false;
            inventoryManager.UpdateInventoryUI(inventory);
        }

        // Checks for "Tab" presses | swaps items between hotbar and main inventory or vice versa
        if (playerInputHandler.SwapTriggered)
        {
            inventoryManager.inventorySwap = true;
            if (!inventoryManager.inventoryOpen && inventory.Count > 0 && inventoryManager.mainInventoryItems.Count < 24)
            {
                inventoryManager.SendToInventory(inventory[selectedInventoryIndex]);
                inventory.RemoveAt(selectedInventoryIndex);
                selectedInventoryIndex--;
                if (selectedInventoryIndex < 0) { selectedInventoryIndex = 0; }
                inventoryManager.CycleSelectorPosition(selectedInventoryIndex);
            }
            else if (inventoryManager.inventoryOpen && inventory.Count < 8 && inventoryManager.mainInventoryItems.Count > 0)
            {
                GameObject temp = inventoryManager.SendToHotBar();
                inventory.Add(temp);
                selectedInventoryIndex--;
                if (selectedInventoryIndex < 0) { selectedInventoryIndex = 0; }
                inventoryManager.CycleSelectorPosition(selectedInventoryIndex);
            }
            else { Debug.Log("Hot Bar Full, Can't Swap"); }
            inventoryManager.inventorySwap = false;
            playerInputHandler.SwapTriggered = false;
            inventoryManager.UpdateInventoryUI(inventory);
        }

        // Updates the hotbar &/or main inventory UI | Previously was everyframe, now it only does when an action that changes the inventory is taken
        //inventoryManager.UpdateInventoryUI(inventory);
    }

    void DetectObject()
    {
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out RaycastHit hit, pickupRange))
        {
            if (CheckTagArray(hit.collider.gameObject.tag, objectTagArray))
            {
                currentPickupTarget = hit.collider.gameObject;
                Debug.Log("hit");
            }

            else if (CheckTagArray(hit.collider.gameObject.tag, PuzzleViewTagArray))
            {
                currentPickupTarget = hit.collider.gameObject;
                Debug.Log("hit");
            }
        }
    }

    bool CheckTagArray(string otherTag, string[] tagArray)
    {
        foreach (string tag in tagArray)
        {
            if (tag == otherTag)
            {
                return true;
            }
        }
        return false;
    }

    void PickupObject(GameObject objectPickup)
    {
        heldObject = objectPickup;

        if (CheckTagArray(heldObject.gameObject.tag, PuzzleViewTagArray))
        {
            DropObject();
            Player.GetComponent<FirstPersonController>().enabled = false;
            PuzzleViewEnter.Play();
            
            // PuzzleView1();
            PuzzleView2();
        }

        else {

            heldObject.transform.SetParent(holdPoint);
            heldObject.transform.localPosition = Vector3.zero;
            heldObject.transform.localRotation = Quaternion.identity;

            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
            Debug.Log("Picked up: " + heldObject.name);
        }
    }

    void DropObject()
    {
        heldObject.transform.SetParent(null);

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        Debug.Log("Dropped: " + heldObject.name);
        heldObject = null;
    }

    void ThrowObject()
    {
        heldObject.transform.SetParent(null);

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce(transform.forward * throwForce);
        }
        heldObject = null;
    }

    void StoreHeldObject()
    {
        // If hotbar has less than 8 items, send to hotbar
        if (inventory.Count < 8)
        {
            inventory.Add(heldObject);
            Debug.Log("Stored in hotbar: " + heldObject.name);
            heldObject.SetActive(false);
            heldObject.transform.SetParent(null);
            heldObject = null;
        }
        // Else, If main inventory has less than 24 items, send to main inventory
        else if(inventoryManager.mainInventoryItems.Count < 24)
        {
            inventoryManager.SendToInventory(heldObject);
            Debug.Log("Hot Bar is Full! Sending to Main Inventory.");
            heldObject.SetActive(false);
            heldObject.transform.SetParent(null);
            heldObject = null;
        }
        // Else-else nothing happens
    }

    void RetrieveFromInventory()
    {
        GameObject obj = inventory[selectedInventoryIndex];
        inventory.RemoveAt(selectedInventoryIndex);

        if (inventory.Count == 0)
        {
            selectedInventoryIndex = 0;
        }
        else
        {
            selectedInventoryIndex %= inventory.Count;
        }
        inventoryManager.CycleSelectorPosition(selectedInventoryIndex);

        obj.SetActive(true);
        heldObject = obj;
        heldObject.transform.SetParent(holdPoint);
        heldObject.transform.localPosition = Vector3.zero;
        heldObject.transform.localRotation = Quaternion.identity;

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        Debug.Log("Retrieved from inventory: " + heldObject.name);
    }

    void InventoryCycle()
    {
        selectedInventoryIndex = (selectedInventoryIndex + 1) % inventory.Count;
        Debug.Log("Selected inventory item: " + inventory[selectedInventoryIndex].name);
        inventoryDisplayText.gameObject.SetActive(true);
        UpdateInventoryDisplay();
        StartCoroutine(HideInventoryText(4f));
    }

    void UpdateInventoryDisplay()
    {
        if (inventory.Count > 0)
        {
            inventoryDisplayText.text = inventory[selectedInventoryIndex].name;
            inventoryManager.CycleSelectorPosition(selectedInventoryIndex);
        }
    }

    IEnumerator HideInventoryText(float delay)
    {
        yield return new WaitForSeconds(delay);
        inventoryDisplayText.gameObject.SetActive(false);
    }

    void PuzzleView1()
    {
        PuzzleView1ControlsText.gameObject.SetActive(true);
        PuzzleViewManager.GetComponent<PuzzleView1>().enabled = true;
        PlayerCamera.SetActive(false);
        PuzzleViewCamera1.SetActive(true);
    }

    void PuzzleView2()
    {
        ManikinPartCheckerText.gameObject.SetActive(true);
        PuzzleView2ControlsText.gameObject.SetActive(true);
        PuzzleViewManager.GetComponent<PuzzleView2>().enabled = true;
        PlayerCamera.SetActive(false);
        PuzzleViewCamera2.SetActive(true);
    }

    void ExitPuzzleView()
    {
        PuzzleView1ControlsText.gameObject.SetActive(false);
        PuzzleView2ControlsText.gameObject.SetActive(false);
        ManikinPartCheckerText.gameObject.SetActive(false);
        PuzzleViewManager.GetComponent<PuzzleView1>().enabled = false;
        PuzzleViewManager.GetComponent<PuzzleView2>().enabled = false;
        Player.GetComponent<FirstPersonController>().enabled = true;
        PlayerCamera.SetActive(true);
        PuzzleViewCamera1.SetActive(false);
        PuzzleViewCamera2.SetActive(false);
    }
}