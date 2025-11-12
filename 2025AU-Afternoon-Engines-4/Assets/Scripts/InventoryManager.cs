using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
//using UnityEditor;

public class InventoryManager : MonoBehaviour
{
    public Image hotBarSelector;
    private int[] hotBarPositionsX = { -505, -353, -209, -66, 80, 224, 365, 515 }; // x-axis
    private int hotBarPositionY = -9;
    private Sprite thumbnail;
    private int spriteSize = 64;
    private List<Image> inventorySlotUsed2 = new List<Image>();

    void Start()
    {
        // Ensures the selector always starts at the first slot
        CycleSelectorPosition(0);
    }

    public void CycleSelectorPosition(int position)
    {
        hotBarSelector.rectTransform.anchoredPosition = new Vector3(hotBarPositionsX[position], hotBarPositionY, 0);
    }

    public void InstantiateInventoryItem(GameObject item, int position)
    {
        //GetItemThumbnail(item);
        Image itemThumbnail = Instantiate(hotBarSelector, hotBarSelector.transform.parent);
        itemThumbnail.rectTransform.sizeDelta = new Vector2(spriteSize, spriteSize);
        itemThumbnail.sprite = thumbnail;
        itemThumbnail.rectTransform.anchoredPosition = new Vector3(hotBarPositionsX[position], hotBarPositionY, 0);
        inventorySlotUsed2.Add(itemThumbnail);
    }

    public void GetItemThumbnail(GameObject item)
    {
        //Texture2D tempThumbnail = AssetPreview.GetMiniThumbnail(item);
        //thumbnail = Sprite.Create(tempThumbnail, new Rect(0, 0, spriteSize, spriteSize), new Vector2(1, 1), 100);
    }

    public void UpdateInventoryUI(List<GameObject> inventory)
    {
        foreach (Image uiItem in inventorySlotUsed2)
        {
            if (uiItem != null)
            {
                Destroy(uiItem.gameObject);
            }
        }
        inventorySlotUsed2.Clear();
        
        int i = 0;
        foreach (GameObject item in inventory)
        {
            InstantiateInventoryItem(item, i);
            i++;
        }
    }
}