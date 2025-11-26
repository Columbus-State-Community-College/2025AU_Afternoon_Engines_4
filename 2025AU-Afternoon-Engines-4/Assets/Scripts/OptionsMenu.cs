using UnityEngine;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public AudioSource musicManaged;
    public TMP_Text musicSettingsText;
    public KeyCode toggleMusic;
    public KeyCode toggleFullScreen;

    void Update()
    {
        if (Input.GetKeyDown(toggleMusic))
        {
            if (musicManaged.isPlaying)
            {
                StopMusic();
            }

            else {
                PlayMusic();
            }
        }

        if (Input.GetKeyDown(toggleFullScreen))
        {
            FullScreenToggle();
        }
    }

    public void StopMusic()
    {
        musicManaged.Stop();
        musicSettingsText.text = "Turn Music On - Press M Key";
    }

    public void PlayMusic()
    {
        musicManaged.Play();
        musicSettingsText.text = "Turn Music Off - Press M Key";
    }

    public void FullScreenToggle()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
