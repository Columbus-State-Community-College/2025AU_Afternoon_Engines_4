using UnityEngine;

// Put this script onto a GameObject that you want to move and/or rotate when a puzzle is completed
// Then drag that GameObject into the variable slot in the cooresponding "PuzzleLock" script

public class PuzzleLinkedMovement : MonoBehaviour
{
    [Header("Main Variables")]
    [Tooltip("Activates the rotation logic")]
    public bool willRotate = false;
    [Tooltip("Activates the moving logic")]
    public bool willMove = false;
    [Tooltip("Uses an animation instead of manually setting the movement / rotation (disables them)")]
    public bool willAnimate = false;

    [Header("Rotation Controls")]
    [Tooltip("Rotation speed")]
    public float rotationSpeed = 10;
    [Tooltip("X-axis rotation")]
    public float rotationX = 0;
    [Tooltip("Y-axis rotation")]
    public float rotationY = 0;
    [Tooltip("Z-axis rotation")]
    public float rotationZ = 0;

    [Header("Movement Controls")]
    [Tooltip("Movement speed")]
    public float movementSpeed = 1;
    [Tooltip("X-axis movement")]
    public float movementX = 0;
    [Tooltip("Y-axis movement")]
    public float movementY = 0;
    [Tooltip("Z-axis movement")]
    public float movementZ = 0;

    [Header("Audio Controls")]
    [Tooltip("Place audio file here")]
    public AudioClip soundEffect;
    private AudioSource soundEffectSource;
    [Tooltip("Sound effect volume (0.0 to 1.0)")]
    public float effectVolume = 1;
    [Tooltip("If the effect plays localcally (unchecked = it plays globally)")]
    public bool isLocal = true;
    [Tooltip("Play sound effect on movement completion (or unchecked for on movement start (aka puzzle completion))")]
    public bool playOnCompletion = false;
    private bool soundPlayed = false;

    private Vector3 objectPosition;
    private Quaternion rotationTarget;
    private Vector3 movementCalc;
    private Animation animationSource;

    private bool scriptCompleted = false;
    private bool rotationCompleted = false;
    private bool movementCompleted = false;
    private bool animationCompleted = false;
    [HideInInspector]
    public bool linkedPuzzleCompleted = false;

    void Start()
    {
        // Audio configuration
        SetupSoundEffect();

        // Movement configuration
        if (!willAnimate)
        {
            objectPosition = gameObject.transform.position;
            movementCalc = new Vector3(movementX, movementY, movementZ) + objectPosition;
            rotationTarget = transform.rotation * Quaternion.Euler(rotationX, rotationY, rotationZ);
        }
        else
        {
            animationSource = GetComponent<Animation>();
        }
    }

    void Update()
    {
        if (linkedPuzzleCompleted && !scriptCompleted)
        {
            if (willRotate && !rotationCompleted && !willAnimate)
            {
                ObjectRotation();
            }
            else { rotationCompleted = true; }

            if (willMove && !movementCompleted && !willAnimate)
            {
                ObjectMovement();
            }
            else { movementCompleted = true; }

            if (!soundPlayed && !playOnCompletion)
            {
                PlaySoundEffect();
            }

            if (willAnimate && !animationCompleted)
            {
                animationSource.Play();
                animationCompleted = true;
            }
            else { animationCompleted = true; }

            if (rotationCompleted && movementCompleted && animationCompleted)
            {
                PlaySoundEffect();
                scriptCompleted = true;
            }
        }
    }

    void ObjectRotation()
    {
        float rotationThisFrame = rotationSpeed * Time.deltaTime;

        gameObject.transform.rotation = Quaternion.RotateTowards(gameObject.transform.rotation, rotationTarget, rotationThisFrame);

        if (Quaternion.Angle(transform.rotation, rotationTarget) < 0.1f)
        {
            rotationCompleted = true;
            gameObject.transform.rotation = rotationTarget;
        }
    }

    void ObjectMovement()
    {
        float movementThisFrame = movementSpeed * Time.deltaTime;

        gameObject.transform.position = Vector3.MoveTowards(transform.position, movementCalc, movementThisFrame);

        if (Vector3.Distance(transform.position, movementCalc) < 0.1f)
        {
            movementCompleted = true;
            gameObject.transform.position = movementCalc;
        }
    }

    void SetupSoundEffect()
    {
        if (soundEffect != null)
        {
            soundEffectSource = gameObject.AddComponent<AudioSource>();
            soundEffectSource.volume = effectVolume;
            soundEffectSource.clip = soundEffect;
            if (isLocal)
            {
                soundEffectSource.spatialBlend = 1.0f;
                soundEffectSource.rolloffMode = AudioRolloffMode.Linear;
                soundEffectSource.minDistance = 1.0f;
                soundEffectSource.maxDistance = 25.0f;
            }
            else
            {
                soundEffectSource.spatialBlend = 0.0f;
            }
        }
    }

    void PlaySoundEffect()
    {
        if (soundEffect != null)
        {
            soundEffectSource.PlayOneShot(soundEffect);
            soundPlayed = true;
        }
    }
}