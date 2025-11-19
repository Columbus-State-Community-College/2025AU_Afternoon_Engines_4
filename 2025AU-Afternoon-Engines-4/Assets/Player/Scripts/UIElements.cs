using UnityEngine;

public class UIElements : MonoBehaviour
{
    [Header("Game Over Screens")]
    [SerializeField] private GameObject WinScreen;
    [SerializeField] private GameObject LoseScreen;
    
    public void WinGame()
    {
        WinScreen.SetActive(true);
    }

    // Update is called once per frame
    public void LoseGame()
    {
        LoseScreen.SetActive(true);
    }
}
