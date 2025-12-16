using System;
using UnityEngine;

public class RestartGameScript : MonoBehaviour
{

    public void RestartGame()
    {
        Debug.Log("Game Should Restart");
        MainManager.Instance.RestartGame();
    }

}
