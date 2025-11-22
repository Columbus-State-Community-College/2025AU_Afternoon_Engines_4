using UnityEngine;

public class RightLegCollide : MonoBehaviour
{
    public AudioSource WrongPlaceSound;
    public AudioSource CorrectPlaceSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RightLeg"))
        {
            GameObject objectUntag = GameObject.FindWithTag("RightLegPart");
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
