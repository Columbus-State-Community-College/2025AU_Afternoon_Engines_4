using UnityEngine;
using TMPro;

public class PauseScreenScript : MonoBehaviour
{
    [SerializeField] private GameObject ThisScreen;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        //ThisScreen.SetActive(false);
        this.gameObject.SetActive(false);
        MainManager.Instance.PauseScreen = this.gameObject;
        //this.gameObject.GetComponentInChildren<HintTextScript>().Hint_Text = MainManager.Instance.CurrentHint();
    }
    void OnEnable()
    {
        this.gameObject.GetComponentInChildren<HintTextScript>().Hint_Text = MainManager.Instance.CurrentHint();
    }

    public void SetActivationState(bool state)
    {
        this.gameObject.SetActive(state);
    }
    public void RestartGame()
    {
        Debug.Log("Game Should Restart");
        MainManager.Instance.RestartGame();
    }
    /*/ Update is called once per frame
    void Update()
    {
        
    }*/
}
