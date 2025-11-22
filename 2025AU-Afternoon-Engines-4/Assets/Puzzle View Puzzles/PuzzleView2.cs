using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class PuzzleView2 : MonoBehaviour
{
    [Tooltip("Objects with this tag will affect the puzzle view 2 script.")]
    private string[] manikinPartTagArray = { "HeadPart", "LeftLegPart", "RightLegPart", "LeftArmPart", "RightArmPart" };
    public TMP_Text ManikinPartCheckerText;
    public AudioSource PuzzleFinishedSound;

    void Update()
    {
        List<GameObject> foundObjects = new List<GameObject>();

        foreach (string tag in manikinPartTagArray)
        {
            GameObject[] manikinPartFind = GameObject.FindGameObjectsWithTag(tag);
            foundObjects.AddRange(manikinPartFind);
        }

        if (foundObjects.Count == 0)
        {
            ManikinPartCheckerText.text = "Every Manikin part has been correctly placed! This puzzle is complete!";
            PuzzleFinishedSound.Play();
        }
        else
        {
            ManikinPartCheckerText.text = "Not every Manikin part has been correctly placed yet!";
        }
    }
}


