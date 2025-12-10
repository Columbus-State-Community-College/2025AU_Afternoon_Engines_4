using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadingZone : MonoBehaviour
{
    [Tooltip("This is the scene that Unity will load up upon colliding with the loading zone.")]
    [SerializeField] private string Scene;
    [Tooltip("Put the Loading_Screen image from the UI here")]
    [SerializeField] private Image loadingScreen;
    [Tooltip("How long in seconds the loading screen is")]
    [SerializeField] private int loadingScreenDuration = 3;

    private void Awake()
    {
        loadingScreen.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            loadingScreen.gameObject.SetActive(true);
            Invoke("LoadScene", loadingScreenDuration);
        }
    }

    public void LoadScene()
    {
        loadingScreen.gameObject.SetActive(false);
        SceneManager.LoadScene(Scene);
    }
}