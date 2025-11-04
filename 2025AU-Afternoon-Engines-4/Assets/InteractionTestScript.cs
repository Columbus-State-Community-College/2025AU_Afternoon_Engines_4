using UnityEngine;

public class InteractionTestScript : MonoBehaviour, IInteractable
{
    void IInteractable.Interact()
    {
        Debug.Log(Random.Range(0, 100));
    }
}
