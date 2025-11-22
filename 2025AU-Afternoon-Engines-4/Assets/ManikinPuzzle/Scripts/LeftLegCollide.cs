using UnityEngine;

public class LeftLegCollide : MonoBehaviour
{
    public AudioSource WrongPlaceSound;
    public AudioSource CorrectPlaceSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LeftLeg"))
        {
            GameObject objectUntag = GameObject.FindWithTag("LeftLegPart");
            if (objectUntag != null)
            {
                objectUntag.tag = "Untagged";
                CorrectPlaceSound.Play();
            }
        }

        else {
            WrongPlaceSound.Play();
        }
    }
}
