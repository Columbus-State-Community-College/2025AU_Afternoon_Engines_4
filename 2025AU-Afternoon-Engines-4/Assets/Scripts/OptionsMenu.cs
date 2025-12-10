using UnityEngine;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public AudioSource musicManaged;
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
    }

    public void PlayMusic()
    {
        musicManaged.Play();
    }

    public void FullScreenToggle()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
