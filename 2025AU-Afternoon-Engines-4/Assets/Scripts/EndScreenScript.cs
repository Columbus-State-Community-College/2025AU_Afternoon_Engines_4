using UnityEngine;

public class EndScreenScript : MonoBehaviour
{
    [SerializeField] private GameObject ThisScreen;
    //[SerializeField] private PlayerInputHandler inputHandlerObject;


    void Start()
    {
        //ThisScreen.SetActive(false);
        this.gameObject.SetActive(false);
        MainManager.Instance.LoseScreen = this.gameObject;
    }

    /*/ Update is called once per frame
    void Update()
    {
        
    }*/
}
