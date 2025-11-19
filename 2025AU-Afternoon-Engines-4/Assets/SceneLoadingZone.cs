using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingZone : MonoBehaviour
{
    [Tooltip("This is the scene that Unity will load up upon colliding with the loading zone.")]
    [SerializeField] private string Scene;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(Scene);
        }
    }
}
