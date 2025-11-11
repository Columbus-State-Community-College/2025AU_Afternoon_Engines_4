using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    void IInteractable.Interact()
    {
        /* Copy the code from this onto a new script component any object in the scene the player will need to interact with.
        Either script what happens with this object here or call the relevant functions from relevant scripts/objects as needed.
        The Interact() function is what is called by the Player's "Interact" Action.
        */
        Debug.Log("Interacting!");//Random.Range(0, 100));
    }
}
