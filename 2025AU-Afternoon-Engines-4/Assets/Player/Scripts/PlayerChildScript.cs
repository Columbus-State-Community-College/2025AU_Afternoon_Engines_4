using UnityEngine;

public class PlayerChildScript : MonoBehaviour
{
    public GameObject PauseScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        //PauseScreen = GameObject.Find("PauseScreen");//.GetComponentInChildren<PauseScreenScript>().gameObject;
        Debug.LogAssertion(PauseScreen.activeInHierarchy);
    }

    /*/ Update is called once per frame
    void Update()
    {
        
    }*/
}
