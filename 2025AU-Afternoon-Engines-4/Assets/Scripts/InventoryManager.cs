using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public Image hotBarSelector;
    public GameObject mainInventory;
    public Camera previewCamera;
    private int[] hotBarPositionX = { -505, -353, -209, -66, 80, 224, 365, 515 };
    private int hotBarPositionY = -9;
    private int[] mainInventoryPositionX = { -480, -328, -184, -41, 105, 249, 390, 540, -480, -328, -184, -41, 105, 249, 390, 540, -480, -328, -184, -41, 105, 249, 390, 540 };
    private int[] mainInventoryPositionY = { 120, 230, 350 };
    private Sprite spriteThumbnail;
    private Image itemThumbnail;
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
        mainInventory.SetActive(false);
        parentUI = mainInventory.transform.parent.gameObject;
    }

    public void OpenInventory()
    {
        foreach (Transform uiElement in parentUI.transform)
        {
            uiElement.gameObject.SetActive(false);
        }
        foreach (Image thumbnail in inventorySlotsUsed)
        {
            thumbnail.transform.gameObject.SetActive(true);
        }

        hotBarSelector.transform.gameObject.SetActive(true);
        hotBarSelector.rectTransform.anchoredPosition = new Vector3(mainInventoryPositionX[0], mainInventoryPositionY[0], 0);
        mainInventory.SetActive(true);
        inventoryOpen = true;
    }

    public void CloseInventory()
    {
        foreach (Transform uiElement in parentUI.transform)
        {
            if (uiElement.gameObject.name != "WinScreen" || uiElement.gameObject.name != "LoseScreen") // this makes sure these arent enabled erroneously
            {
                uiElement.gameObject.SetActive(true);
            }
            
        }
        foreach (Image thumbnail in inventorySlotsUsed)
        {
            thumbnail.transform.gameObject.SetActive(false);
        }

        hotBarSelector.rectTransform.anchoredPosition = new Vector3(hotBarPositionX[0], hotBarPositionY, 0);
        mainInventory.SetActive(false);
        inventoryOpen = false;
    }

    public void SendToInventory(GameObject item)
    {
        int position = 0;
        position = mainInventoryItems.Count % 8;
        InstantiateInventoryItem(item, position);
        mainInventoryItems.Add(item);
    }

    public GameObject SendToHotBar()
    {
        GameObject temp = mainInventoryItems[inventorySelectorPosition];
        mainInventoryItems.RemoveAt(inventorySelectorPosition);
        GameObject.Destroy(inventorySlotsUsed[inventorySelectorPosition].gameObject);
        inventorySlotsUsed.RemoveAt(inventorySelectorPosition);
        inventorySelectorPosition--;
        if (inventorySelectorPosition < 0) { inventorySelectorPosition = 0; }

        return temp;
    }

    // Hot Bar Functions
    public void CycleSelectorPosition(int position)
    {
        if (position < 0) { position = 0; }

        if (!inventoryOpen)
        {
            hotBarSelector.rectTransform.anchoredPosition = new Vector3(hotBarPositionX[position], hotBarPositionY, 0); 
        }
        else
        {
            inventorySelectorPosition = position;
            inventorySelectorPosition++;
            if (inventorySelectorPosition >= mainInventoryItems.Count) { inventorySelectorPosition = 0; }

            int positionY = 0;
            if (inventorySelectorPosition <= 8) { positionY = 0; }
            else if (inventorySelectorPosition <= 16) { positionY = 1; }
            else { positionY = 2; }
            hotBarSelector.rectTransform.anchoredPosition = new Vector3(mainInventoryPositionX[inventorySelectorPosition], mainInventoryPositionY[positionY], 0);
        }
    }

    public void InstantiateInventoryItem(GameObject item, int position)
    {
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
            if (mainInventoryItems.Count < 8) { positionY = 0; }
            else if (mainInventoryItems.Count < 16) { positionY = 1; }
            else { positionY = 2; }
            itemThumbnail.rectTransform.anchoredPosition = new Vector3(mainInventoryPositionX[position], mainInventoryPositionY[positionY], 0);
            if (!inventoryOpen) { itemThumbnail.transform.gameObject.SetActive(false); }
            inventorySlotsUsed.Add(itemThumbnail);
        }
    }

    public void UpdateInventoryUI(List<GameObject> inventory)
    {
        foreach (Image uiItem in hotBarSlotsUsed)
        {
            if (uiItem != null)
            {
                Destroy(uiItem.gameObject);
            }
        }
        hotBarSlotsUsed.Clear();

        int i = 0;
        foreach (GameObject item in inventory)
        {
            InstantiateInventoryItem(item, i);
            i++;
        }

        if (inventoryOpen)
        {
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

        // Clean up
        previewCamera.targetTexture = null;
        RenderTexture.active = null;
        tempTexture.Release();
        GameObject.DestroyImmediate(tempTexture);
        GameObject.DestroyImmediate(objectPreview);

        return objectThumbnail;
    }
}