using UnityEngine;

public class RightArmCollide : MonoBehaviour
{
    public AudioSource WrongPlaceSound;
    public AudioSource CorrectPlaceSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RightArm"))
        {
            GameObject objectUntag = GameObject.FindWithTag("RightArmPart");
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
