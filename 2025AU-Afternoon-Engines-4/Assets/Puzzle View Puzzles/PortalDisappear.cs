using UnityEngine;

public class PortalDisappear : MonoBehaviour
{
    public GameObject PortalEntry;

    void Start()
    {
        PortalEntry.SetActive(false);
    }
}
