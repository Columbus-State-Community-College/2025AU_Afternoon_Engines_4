using UnityEngine;

public class WinScreenScript : MonoBehaviour
{
    [SerializeField] private GameObject ThisScreen;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        //ThisScreen.SetActive(false);
        this.gameObject.SetActive(false);
        MainManager.Instance.WinScreen = this.gameObject;
    }

    /*/ Update is called once per frame
    void Update()
    {
        
    }*/
}
