using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventSystem))]
public class PlayerChildScript : MonoBehaviour
{
    public GameObject PauseScreen;
    public EventSystem eventSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventSystem = GetComponent<EventSystem>();
        eventSystem.firstSelectedGameObject = GetComponentInChildren<Canvas>().gameObject;

        //PauseScreen = GameObject.Find("PauseScreen");//.GetComponentInChildren<PauseScreenScript>().gameObject;
        //Debug.LogAssertion(PauseScreen.activeInHierarchy);
    }

    /*/ Update is called once per frame
    void Update()
    {
        
    }*/
}
