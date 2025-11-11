using UnityEngine;

public class Red_Key_01_Interaction_Script : MonoBehaviour, IInteractable
{

    void IInteractable.Interact()
    {
        Debug.Log("This is the red key");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
