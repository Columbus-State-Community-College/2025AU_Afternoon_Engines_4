using UnityEngine;

public class EndScreenScript : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler inputHandlerObject;


    void OnBecameVisible()
    {
        inputHandlerObject.ActivateUIActionMap(true);
    }

    void OnBecameInvisible()
    {
        inputHandlerObject.ActivateUIActionMap(false);
    }

    // add in an enabled state and disabled state, provide button event functions

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /*
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
