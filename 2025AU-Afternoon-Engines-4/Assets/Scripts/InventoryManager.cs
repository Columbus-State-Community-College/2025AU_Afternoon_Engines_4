using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Tooltip("Put the hotbar selector / highlighter from the UI here")]
    public Image hotBarSelector;
    [Tooltip("Put the main inventory png from the UI here")]
    public GameObject mainInventoryImage;
    [Tooltip("Put the item Name text from the UI here")]
    public TMP_Text itemNameText;
    [Tooltip("Put the item Description text from the UI here")]
    public TMP_Text itemDescreptionText;
    [Tooltip("Put the PreviewCamera object from the Player prefab here")]
    public Camera previewCamera;
    private int[] hotBarPositionX = { -505, -353, -209, -66, 80, 224, 365, 515 };
    private int hotBarPositionY = -9;
    // Repeats because that was easier than trying to make an algorithm for it
    private int[] mainInventoryPositionX = { -480, -328, -184, -41, 105, 249, 390, 540, -480, -328, -184, -41, 105, 249, 390, 540, -480, -328, -184, -41, 105, 249, 390, 540 };
    private int[] mainInventoryPositionY = { 120, 230, 350 };
    private Sprite spriteThumbnail;
    private Image itemThumbnail;
    private Image bigItemThumbnail;
    private int spriteSize = 92;
    private List<Image> hotBarSlotsUsed = new List<Image>();
    private List<Image> inventorySlotsUsed = new List<Image>();
    [HideInInspector]
    public List<GameObject> mainInventoryItems = new List<GameObject>();
    private GameObject parentUI;
    [HideInInspector]
    public bool inventoryOpen = false;
    [HideInInspector]
    public bool inventorySwap = false;
    [HideInInspector]
    public int inventorySelectorPosition = 0;

    void Start()
    {
        // Ensures the selector always starts at the first slot
        CycleSelectorPosition(0);
        // Hides the main inventory initially
        mainInventoryImage.SetActive(false);
        itemNameText.gameObject.SetActive(false);
        itemDescreptionText.gameObject.SetActive(false);
        
        // Gets the "Canvas" element of the UI to be able to access all the other UI elements
        parentUI = mainInventoryImage.transform.parent.gameObject;
    }

    public void OpenInventory()
    {
        // On opening the inventory hide every other UI element (Use the if statement for exceptions that should stay shown), then show every item stored in the main inventory, and then show the main inventory
        foreach (Transform uiElement in parentUI.transform)
        {
            if (uiElement.gameObject.name != "Timer_Text")
            {
                uiElement.gameObject.SetActive(false);
            }
        }
        foreach (Image thumbnail in inventorySlotsUsed)
        {
            thumbnail.transform.gameObject.SetActive(true);
        }

        hotBarSelector.transform.gameObject.SetActive(true);
        hotBarSelector.rectTransform.anchoredPosition = new Vector3(mainInventoryPositionX[0], mainInventoryPositionY[0] - 15, 0);
        mainInventoryImage.SetActive(true);
        UpdateItemDescriptions();
        itemNameText.gameObject.SetActive(true);
        itemDescreptionText.gameObject.SetActive(true);
        bigItemThumbnail?.gameObject.SetActive(true);
        inventoryOpen = true;
    }

    public void CloseInventory()
    {
        // On closing the inventory show every other UI element (Use the if statement for exceptions that should stay hidden), then hide every item stored in the main inventory, and then hide the main inventory
        foreach (Transform uiElement in parentUI.transform)
        {
            // This makes sure these arent enabled erroneously
            if (uiElement.gameObject.name != "WinScreen" && uiElement.gameObject.name != "LoseScreen" && uiElement.gameObject.name != "PuzzleView1Controls_Text" && uiElement.gameObject.name != "PauseScreen" && uiElement.gameObject.name != "MusicSettings_Text" && uiElement.gameObject.name != "FullscreenSettings_Text" && uiElement.gameObject.name != "OptionsPopup_text" && uiElement.gameObject.name != "LoadingScreen")
            {
                uiElement.gameObject.SetActive(true);
            }
            
        }
        foreach (Image thumbnail in inventorySlotsUsed)
        {
            thumbnail.transform.gameObject.SetActive(false);
        }

        hotBarSelector.rectTransform.anchoredPosition = new Vector3(hotBarPositionX[0], hotBarPositionY, 0);
        mainInventoryImage.SetActive(false);
        bigItemThumbnail?.gameObject.SetActive(false);
        itemNameText.gameObject.SetActive(false);
        itemDescreptionText.gameObject.SetActive(false);
        inventoryOpen = false;
    }

    public void SendToInventory(GameObject item)
    {
        // position calculates the X-axis of the slot the item will be sent to
        int position = 0;
        position = mainInventoryItems.Count % 8;
        InstantiateInventoryItem(item, position);
        mainInventoryItems.Add(item);
    }

    public GameObject SendToHotBar()
    {
        // Gets the GameObject that is being sent, removes it from the main inventory, destroys its saved image thumbnail, resets the selector to one position to the left, return the GameObject that will be added to the hotbar
        GameObject temp = mainInventoryItems[inventorySelectorPosition];
        mainInventoryItems.RemoveAt(inventorySelectorPosition);
        GameObject.Destroy(inventorySlotsUsed[inventorySelectorPosition].gameObject);
        inventorySlotsUsed.RemoveAt(inventorySelectorPosition);
        inventorySelectorPosition--;
        if (inventorySelectorPosition < 0) { inventorySelectorPosition = 0; }

        return temp;
    }

    // Moves the selector to the next slot
    public void CycleSelectorPosition(int position)
    {
        if (position < 0) { position = 0; }

        if (!inventoryOpen)
        {
            // For the hotbar the position math is done in PickUpInventory.cs (and is simpler)
            hotBarSelector.rectTransform.anchoredPosition = new Vector3(hotBarPositionX[position], hotBarPositionY, 0); 
        }
    }

    // Controls for main inventory navigation
    public void MainInventoryControls()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || (Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            inventorySelectorPosition -= 8;
            MainInventoryNavigation();
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetKeyDown(KeyCode.JoystickButton3)))
        {
            inventorySelectorPosition += 8;
            MainInventoryNavigation();
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow) || (Input.GetKeyDown(KeyCode.JoystickButton2)))
        {
            inventorySelectorPosition--;
            MainInventoryNavigation();
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKeyDown(KeyCode.JoystickButton1)))
        {
            inventorySelectorPosition++;
            MainInventoryNavigation();
        }
    }

    // Math for deciding the selector position (X & Y axis) in the main inventory
    private void MainInventoryNavigation()
    {
        // If the position is "above" the inventory amount, send it to the first slot
        // Else If the position is "below" the inventory amount, send it to the last slot
        if (inventorySelectorPosition >= mainInventoryItems.Count) { inventorySelectorPosition = 0; }
        else if (inventorySelectorPosition < 0) { inventorySelectorPosition = mainInventoryItems.Count - 1; }

        int positionY = 0;
        if (inventorySelectorPosition <= 7) { positionY = 0; }
        else if (inventorySelectorPosition <= 15) { positionY = 1; }
        else { positionY = 2; }
        hotBarSelector.rectTransform.anchoredPosition = new Vector3(mainInventoryPositionX[inventorySelectorPosition], mainInventoryPositionY[positionY] - 15, 0);

        UpdateItemDescriptions();
    }

    public void InstantiateInventoryItem(GameObject item, int position)
    {
        // Goes through the whole process of making the thumbnail images
        // GetItemThumbnail() returns a Texture2D variable
        // CreateItemImage() turns that into a sprite variable, and then an image variable
        // If statement is used to determine where the thumbnail image goes
        Texture2D tempThumbnail = GetItemThumbnail(item);
        CreateItemImage(tempThumbnail);
        itemThumbnail.name = item.name;

        if (hotBarSlotsUsed.Count < 8 && !inventorySwap && !inventoryOpen)
        {
            itemThumbnail.rectTransform.anchoredPosition = new Vector3(hotBarPositionX[position], -hotBarPositionY, 0);
            itemThumbnail.transform.gameObject.SetActive(true);
            hotBarSlotsUsed.Add(itemThumbnail);
        }
        else
        {
            int positionY = 0;
            if (position < 8) { positionY = 0; }
            else if (position < 16) { positionY = 1; }
            else { positionY = 2; }
            itemThumbnail.rectTransform.anchoredPosition = new Vector3(mainInventoryPositionX[position], mainInventoryPositionY[positionY], 0);
            if (!inventoryOpen) { itemThumbnail.transform.gameObject.SetActive(false); }
            inventorySlotsUsed.Add(itemThumbnail);
        }
    }

    public void UpdateInventoryUI(List<GameObject> inventory)
    {
        int i = 0;
        // Currently called every frame in the PickUpInventory.cs Update() function | on the week 4 task list to change this
        // Iterates through every hotbar thumbnail image, destroying thumbnails for items that are no longer stored, and repositioning thumbnails for items still there
        if (!inventoryOpen)
        {
            foreach (Image uiItem in hotBarSlotsUsed)
            {
                if (uiItem != null)
                {
                    Destroy(uiItem.gameObject);
                }
            }
            hotBarSlotsUsed.Clear();

            i = 0;
            foreach (GameObject item in inventory)
            {
                InstantiateInventoryItem(item, i);
                i++;
            }
        }

        if (inventoryOpen)
        {
            // Iterates through every main inventory thumbnail image, destroying thumbnails for items that are no longer stored, and repositioning thumbnails for items still there
            foreach (Image uiItem in inventorySlotsUsed)
            {
                if (uiItem != null)
                {
                    Destroy(uiItem.gameObject);
                }
            }
            inventorySlotsUsed.Clear();

            i = 0;
            foreach (GameObject item in mainInventoryItems)
            {
                InstantiateInventoryItem(item, i);
                i++;
            }
        }
    }

    private void UpdateItemDescriptions()
    {
        if (mainInventoryItems.Count > 0)
        {
            // Gets the item data from the ItemData.cs script on that specific GameObject
            string itemName = mainInventoryItems[inventorySelectorPosition].GetComponent<ItemData>().itemName;
            string itemDescription = mainInventoryItems[inventorySelectorPosition].GetComponent<ItemData>().itemDescription;

            // Displays the item data in the text boxes
            itemNameText.text = itemName;
            itemDescreptionText.text = itemDescription;

            // For displaying a large version of the item thumbnail
            if (bigItemThumbnail == null)
            {
                bigItemThumbnail = Instantiate(inventorySlotsUsed[inventorySelectorPosition], inventorySlotsUsed[inventorySelectorPosition].transform.parent);
                bigItemThumbnail.name = "bigItemThumbnail_Image";
            }
            else
            {
                Sprite tempItemSprite = inventorySlotsUsed[inventorySelectorPosition].GetComponent<Image>().sprite;
                bigItemThumbnail.sprite = tempItemSprite;
            }
            bigItemThumbnail.rectTransform.sizeDelta = new Vector2(spriteSize * 5, spriteSize * 5);
            bigItemThumbnail.rectTransform.anchoredPosition = new Vector3(-368, 478, 0);
        }
    }

    private void CreateItemImage(Texture2D tempThumbnail)
    {
        spriteThumbnail = Sprite.Create(tempThumbnail, new Rect(0.0f, 0.0f, spriteSize, spriteSize), new Vector2(0.5f, 0.5f), 100.0f);
        itemThumbnail = Instantiate(hotBarSelector, hotBarSelector.transform.parent);
        itemThumbnail.rectTransform.sizeDelta = new Vector2(spriteSize, spriteSize);
        itemThumbnail.sprite = spriteThumbnail;
    }

    private Texture2D GetItemThumbnail(GameObject item)
    {
        // Instantiate the GameObject, reset its position/rotation, and get its bounds (2D dimensions basically)
        GameObject objectPreview = Instantiate(item);
        objectPreview.transform.position = new Vector3(0, 0, 0);
        objectPreview.transform.rotation = Quaternion.identity;
        objectPreview.layer = 6;
        objectPreview.SetActive(true);
        Renderer[] objectRenderer = objectPreview.GetComponentsInChildren<Renderer>(); ;
        Bounds objectDimensions = objectRenderer[0].bounds;

        // Position the camera to be centered on the Instanced GameObject and create a texture of what the camera sees
        RenderTexture tempTexture = new RenderTexture(spriteSize, spriteSize, 32);
        previewCamera.targetTexture = tempTexture;
        float zoom = Mathf.Max(objectDimensions.size.x, objectDimensions.size.y, objectDimensions.size.z);
        previewCamera.orthographicSize = zoom / 2f;
        previewCamera.transform.position = objectDimensions.center + Vector3.back * zoom; 
        previewCamera.transform.LookAt(objectDimensions.center);
        previewCamera.Render();

        // Turn the texture into a usable formate and process the transparency
        RenderTexture.active = tempTexture;
        Texture2D objectThumbnail = new Texture2D(spriteSize, spriteSize, TextureFormat.RGBA32, false);
        objectThumbnail.ReadPixels(new Rect(0, 0, spriteSize, spriteSize), 0, 0);
        objectThumbnail.Apply();

        // Clean up | .Destroy() does not work, it needs to be .DestroyImmediate()
        previewCamera.targetTexture = null;
        RenderTexture.active = null;
        tempTexture.Release();
        GameObject.DestroyImmediate(tempTexture);
        GameObject.DestroyImmediate(objectPreview);

        return objectThumbnail;
    }
}