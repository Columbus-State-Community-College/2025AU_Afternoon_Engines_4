using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public Image hotBar; // 8 slots
    public Image hotBarSelector; // 171 pixels wide (1.71% of the screen)
    private int[] hotBarPositionsX = { -505, -353, -209, -66, 80, 224, 365, 515 }; // x-axis
    private int hotBarPositionY = -9; // or maybe 0 
    private int selectorPosition = 0;
    private int[] inventorySlotUsed = { 0, 0, 0, 0, 0, 0, 0, 0 };

    void Start()
    {
        // Largely for testing purposes
        CycleSelectorPosition(0);
    }

    public void CycleSelectorPosition(int position)
    {
        selectorPosition = position;
        hotBarSelector.rectTransform.anchoredPosition = new Vector3(hotBarPositionsX[selectorPosition], hotBarPositionY, 0);
    }

    public void InstantiateInventoryItem(TMP_Text itemText, int position)
    {
        if (inventorySlotUsed[position] != 1)
        {
            TMP_Text instancedText = Instantiate(itemText, hotBarSelector.transform.parent);
            instancedText.rectTransform.anchoredPosition = new Vector3(hotBarPositionsX[selectorPosition], hotBarPositionY, 0);
            inventorySlotUsed[position] = 1;
        }
    }
}
