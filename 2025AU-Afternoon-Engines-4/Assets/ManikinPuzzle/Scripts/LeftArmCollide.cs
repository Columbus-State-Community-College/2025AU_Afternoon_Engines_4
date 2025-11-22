using UnityEngine;

public class LeftArmCollide : MonoBehaviour
{
    public AudioSource WrongPlaceSound;
    public AudioSource CorrectPlaceSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LeftArm"))
        {
            GameObject objectUntag = GameObject.FindWithTag("LeftArmPart");
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
