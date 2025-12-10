using UnityEngine;

// How TO:
// - Add this script onto the GameObject / prefab
// - Fill out the variables
//
// - Make sure this script is on EVERY object in the game that can be picked up
// - You can also double check the data put in for any object / prefab (especiaclly for your own)
// - Right now they are very basic and devoid of any story aspects

public class ItemData : MonoBehaviour
{
    [Tooltip("Input the item's name here")]
    public string itemName;
    [Tooltip("Input the item's description here")]
    public string itemDescription;
}
