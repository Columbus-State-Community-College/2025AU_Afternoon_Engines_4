using UnityEngine;

public class EndScreenScript : MonoBehaviour
{
    [SerializeField] private GameObject ThisScreen;
    [SerializeField] private PlayerInputHandler inputHandlerObject;



    void OnEnable()
    {
        inputHandlerObject.ActivateUIActionMap(true);
    }

    void OnDisable()
    {
        inputHandlerObject.ActivateUIActionMap(false);
    }

    // add in an enabled state and disabled state, provide button event functions

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        ThisScreen.SetActive(false);
    }

    /*/ Update is called once per frame
    void Update()
    {
        
    }*/
}
