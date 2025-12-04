using UnityEngine;
using TMPro;

public class OptionsMenuBringUp : MonoBehaviour
{
    public GameObject OptionsManager;
    public KeyCode BringUpDownOptions;
    public KeyCode BringUpDownOptionsGamepad;
    private bool isOptionsOpen = false;

    void Start()
    {
        OptionsManager.GetComponent<OptionsMenu>().enabled = false;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(BringUpDownOptions) || Input.GetKeyDown(BringUpDownOptionsGamepad))
        {
            if (isOptionsOpen)
            {
                OptionsMenuTurnOff();
                isOptionsOpen = false;
            }
            else
            {
                OptionsMenuTurnOn();
                isOptionsOpen = true;
            }
        }
    }

    void OptionsMenuTurnOn()
    {
        OptionsManager.GetComponent<OptionsMenu>().enabled = true;
    }

    void OptionsMenuTurnOff()
    {
        OptionsManager.GetComponent<OptionsMenu>().enabled = false;
    }
}
