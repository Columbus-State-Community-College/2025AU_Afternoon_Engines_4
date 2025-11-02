using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupInventory : MonoBehaviour
{
    [Tooltip("A lower number will require you to be closer to the object to pick it up, while a higher number will allow you to pick up an object from farther away.")] 
    public float pickupRange = 2f;
    [Header("Object Pickup and Inventory Keys")]
    [Header("Key to pick up an object")]
    public KeyCode pickupKey = KeyCode.Mouse0;
    [Header("Key to throw an object")]
    public KeyCode throwKey = KeyCode.Mouse1;
    [Header("Key to store an object into the inventory")]
    public KeyCode storeKey = KeyCode.LeftShift;
    [Header("Key to switch between different stored objects")]
    [Tooltip("Objects stored in the inventory can be switched between one another, so you can take out what you need into the playfield.")] 
    public KeyCode cycleKey = KeyCode.LeftControl;
    [Tooltip("This is where the object will be held at. Create an empty object, set it near the player, and then bring it over to this empty spot.")] 
    public Transform holdPoint;
    public float throwForce = 500f;
    [Tooltip("Objects with this tag can be picked up")] 
    public string ObjectTag1 = "Pickup1";
    [Tooltip("Objects with this tag can be picked up")]
    public string ObjectTag2 = "PickUp2";

    private GameObject currentPickupTarget;
    private GameObject heldObject;

    private List<GameObject> inventory = new List<GameObject>();
    private int selectedInventoryIndex = 0;

    public TMP_Text inventoryDisplayText;

    // Added to be able to switch isTrigger on the collider off/on so it can collide with the "PuzzleLock" (a Rigid body is also needed for it to work)
    private Collider colliderTrigger;

    void Start()
    {
        inventoryDisplayText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            if (heldObject != null)
            {
                DropObject();
            }
            else if (currentPickupTarget != null)
            {
                PickupObject(currentPickupTarget);
            }
        }

        if (Input.GetKeyDown(throwKey) && heldObject != null)
        {
            ThrowObject();
        }

        if (Input.GetKeyDown(storeKey))
        {
            if (heldObject != null)
            {
                StoreHeldObject();
            }
            else if (inventory.Count > 0)
            {
                RetrieveFromInventory();
            }
        }

        if (Input.GetKeyDown(cycleKey) && inventory.Count > 0)
        {
            InventoryCycle();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ObjectTag1))
        {
            currentPickupTarget = other.gameObject;
            Debug.Log("Object in range: " + other.name);
        }

        else if (other.CompareTag(ObjectTag2))
        {
            currentPickupTarget = other.gameObject;
            Debug.Log("Object in range: " + other.name);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(ObjectTag1) && other.gameObject == currentPickupTarget)
        {
            currentPickupTarget = null;
            Debug.Log("Object out of range: " + other.name);
        }

        else if (other.CompareTag(ObjectTag2) && other.gameObject == currentPickupTarget)
        {
            currentPickupTarget = null;
            Debug.Log("Object out of range: " + other.name);
        }
    }

    void PickupObject(GameObject objectPickup)
    {
        heldObject = objectPickup;
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
        //colliderTrigger = heldObject.GetComponent<Collider>();
        //colliderTrigger.isTrigger = true;

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
            inventoryDisplayText.text = "Selected: " + inventory[selectedInventoryIndex].name;
        }
    }

    IEnumerator HideInventoryText(float delay)
    {
        yield return new WaitForSeconds(delay);
        inventoryDisplayText.gameObject.SetActive(false);
    }
}