using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupInventory : MonoBehaviour, IInteractable
{
    //[Tooltip("A lower number will require you to be closer to the object to pick it up, while a higher number will allow you to pick up an object from farther away.")] 
    //public float pickupRange = 2f; <--- unused right now, but will use for future raycasting.
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

    private GameObject currentPickupTarget;
    private GameObject heldObject;

    private List<GameObject> inventory = new List<GameObject>();
    private int selectedInventoryIndex = 0;

    public TMP_Text inventoryDisplayText;
    public TMP_Text PuzzleView1ControlsText;
    public GameObject Player;
    public GameObject PlayerCamera;
    [SerializeField] private PlayerInputHandler playerInputHandler;
    public GameObject PuzzleViewCamera1;
    public GameObject PuzzleViewManager;
    public InventoryManager inventoryManager;

    // Added to be able to switch isTrigger on the collider off/on so it can collide with the "PuzzleLock" (a Rigid body is also needed for it to work)
    private Collider colliderTrigger;

    void Start()
    {
        inventoryDisplayText.gameObject.SetActive(false);
        PuzzleViewManager.GetComponent<PuzzleView1>().enabled = false;
    }

    void IInteractable.Interact()
    {
        // what happens when something is interacted with
    }

    void Update()
    {
        if (playerInputHandler.InteractTriggered)
        {
            if (heldObject != null)
            {
                DropObject();
            }
            else if (currentPickupTarget != null)
            {
                PickupObject(currentPickupTarget);
            }
            playerInputHandler.InteractTriggered = false;
        }

        if (playerInputHandler.DropTriggered && heldObject != null)
        {
            ThrowObject();
            playerInputHandler.DropTriggered = false;
        }

        if (playerInputHandler.StoreTriggered)
        {
            if (heldObject != null)
            {
                StoreHeldObject();
            }
            else if (inventory.Count > 0)
            {
                RetrieveFromInventory();
            }
            playerInputHandler.StoreTriggered = false;
        }

        if (playerInputHandler.CycleTriggered && inventory.Count > 0)
        {
            InventoryCycle();
            playerInputHandler.CycleTriggered = false;
        }

        if (Input.GetKeyDown(exitPuzzleViewKey))
        {
            ExitPuzzleView1();
        }

        inventoryManager.UpdateInventoryUI(inventory);
    }

    void OnTriggerEnter(Collider other)
    {
        if (CheckTagArray(other.gameObject.tag, objectTagArray))
        {
            currentPickupTarget = other.gameObject;
            Debug.Log("Object in range: " + other.name);
        }

        else if (CheckTagArray(other.gameObject.tag, PuzzleViewTagArray))
        {
            currentPickupTarget = other.gameObject;
            Debug.Log("I think this object has a puzzle that goes with it!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (CheckTagArray(other.gameObject.tag, objectTagArray) && other.gameObject == currentPickupTarget)
        {
            currentPickupTarget = null;
            Debug.Log("Object out of range: " + other.name);
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
            PuzzleView1();
        }

        else {

            heldObject.transform.SetParent(holdPoint);
            heldObject.transform.localPosition = Vector3.zero;
            heldObject.transform.localRotation = Quaternion.identity;
            colliderTrigger = heldObject.GetComponent<Collider>();
            colliderTrigger.isTrigger = false;

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
        colliderTrigger = heldObject.GetComponent<Collider>();
        colliderTrigger.isTrigger = true;

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
        colliderTrigger = heldObject.GetComponent<Collider>();
        colliderTrigger.isTrigger = true;

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
        inventory.Add(heldObject);
        heldObject.SetActive(false);
        heldObject.transform.SetParent(null);

        Debug.Log("Stored in inventory: " + heldObject.name);
        heldObject = null;
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

    void ExitPuzzleView1()
    {
        PuzzleView1ControlsText.gameObject.SetActive(false);
        PuzzleViewManager.GetComponent<PuzzleView1>().enabled = false;
        Player.GetComponent<FirstPersonController>().enabled = true;
        PlayerCamera.SetActive(true);
        PuzzleViewCamera1.SetActive(false);
    }
}