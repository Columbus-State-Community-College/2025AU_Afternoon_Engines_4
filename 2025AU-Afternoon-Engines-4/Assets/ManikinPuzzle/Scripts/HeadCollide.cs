using UnityEngine;

public class HeadCollide : MonoBehaviour
{
    public AudioSource WrongPlaceSound;
    public AudioSource CorrectPlaceSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Head"))
        {
            GameObject objectToUntag = GameObject.FindWithTag("HeadPart");
            if (objectToUntag != null)
            {
                objectToUntag.tag = "Untagged";
                CorrectPlaceSound.Play();
            }
        }

        else {
            WrongPlaceSound.Play();
        }
    }
}
