using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Tooltip("This is the scene that Unity will load up upon pressing the start button.")]
    public string Scene;

    public void StartGame()
    {
        SceneManager.LoadScene(Scene);
    }

    public void QuitGame()
    {
        Debug.Log("Pressing this button inside the build of the game will close the application altogether.");
        Application.Quit();
    }
}
