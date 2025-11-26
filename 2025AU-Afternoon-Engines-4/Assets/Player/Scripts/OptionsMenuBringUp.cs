using UnityEngine;
using TMPro;

public class OptionsMenuBringUp : MonoBehaviour
{
    public GameObject OptionsManager;
    public KeyCode BringUpOptions;
    public KeyCode BringDownOptions;
    public TMP_Text musicSettingsText;
    public TMP_Text optionsPopupText;
    public TMP_Text fullScreenText;

    void Start()
    {
        OptionsManager.GetComponent<OptionsMenu>().enabled = false;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(BringUpOptions))
        {
            OptionsMenuTurnOn();
        }

        else if (Input.GetKeyDown(BringDownOptions))
        {
            OptionsMenuTurnOff();
        }
    }

    void OptionsMenuTurnOn()
    {
        OptionsManager.GetComponent<OptionsMenu>().enabled = true;
        optionsPopupText.gameObject.SetActive(true);
        musicSettingsText.gameObject.SetActive(true);
        fullScreenText.gameObject.SetActive(true);
    }

    void OptionsMenuTurnOff()
    {
        OptionsManager.GetComponent<OptionsMenu>().enabled = false;
        optionsPopupText.gameObject.SetActive(false);
        musicSettingsText.gameObject.SetActive(false);
        fullScreenText.gameObject.SetActive(false);
    }
}
