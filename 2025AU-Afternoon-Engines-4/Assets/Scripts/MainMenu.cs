using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Tooltip("This is the scene that Unity will load up upon pressing the start button.")]
    public string Scene;
    public GameObject firstMenuItem;
    public Button startButtonUnselected;
    public Button exitButtonUnselected;
    private Color newColor = Color.blue;

    void Start()
    {
        InputSystem.DisableDevice(Mouse.current);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; 

        if (EventSystem.current != null && firstMenuItem != null)
        {
            EventSystem.current.SetSelectedGameObject(firstMenuItem);
        }

        if (startButtonUnselected != null)
        {
            ColorBlock cb = startButtonUnselected.colors;

            cb.normalColor = newColor;
            startButtonUnselected.colors = cb;
        }

        if (exitButtonUnselected != null)
        {
            ColorBlock cb = exitButtonUnselected.colors;
            cb.normalColor = newColor;
            exitButtonUnselected.colors = cb;
        }
    }

    public void StartGame()
    {
        InputSystem.EnableDevice(Mouse.current);
        SceneManager.LoadScene(Scene);
    }

    public void QuitGame()
    {
        Debug.Log("Pressing this button inside the build of the game will close the application altogether.");
        Application.Quit();
    }
}
