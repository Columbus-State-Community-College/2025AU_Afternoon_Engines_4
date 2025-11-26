using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Tooltip("This is the scene that Unity will load up upon pressing the start button.")]
    public string Scene;
    public GameObject firstMenuItem;
    public Button startButtonUnselected;
    public Button creditsButtonUnselected;
    public Button exitButtonUnselected;
    public TMP_Text textChange;
    public TMP_Text creditsTitle;
    public GameObject makersCreditsText;
    public GameObject soundCreditsText;
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

        if (creditsButtonUnselected != null)
        {
            ColorBlock cb = creditsButtonUnselected.colors;
            cb.normalColor = newColor;
            creditsButtonUnselected.colors = cb;
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

    public void CreditsMenu()
    {
        creditsTitle.text = "---Credits---";

        if (startButtonUnselected != null)
        {
            startButtonUnselected.interactable = false;
        }

        if (exitButtonUnselected != null)
        {
            exitButtonUnselected.interactable = false;
        }

        creditsButtonUnselected.onClick.AddListener(BackMenu);
        makersCreditsText.SetActive(true);
        soundCreditsText.SetActive(true);

        textChange.text = "Back";
        creditsButtonUnselected.onClick.RemoveListener(CreditsMenu);
    }

    public void BackMenu()
    {
        creditsTitle.text = "Mind Break!";
        textChange.text = "Credits";

        if (startButtonUnselected != null)
        {
            startButtonUnselected.interactable = true;
        }

        if (exitButtonUnselected != null)
        {
            exitButtonUnselected.interactable = true;
        }

        creditsButtonUnselected.onClick.AddListener(CreditsMenu);
        makersCreditsText.SetActive(false);
        soundCreditsText.SetActive(false);
        creditsButtonUnselected.onClick.RemoveListener(BackMenu);
    }

    public void QuitGame()
    {
        Debug.Log("Pressing this button inside the build of the game will close the application altogether.");
        Application.Quit();
    }
}
