using UnityEngine;

public class InteractableObjectDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactionContext;
    [SerializeField] public bool isAnInteractableDoor = true;
    Animator doorAnimator;
    AudioSource doorAudioSource;

    public bool doorIsOpen = false;

    [Header("Door Sound FX")]
    public AudioClip doorOpeningSound;
    public AudioClip doorClosingSound;
    public AudioClip doorLockedSound;


    string animatorTrigger = "Trigger";

    private void Start()
    {
        doorAnimator = GetComponent<Animator>();
        doorAudioSource = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        if (isAnInteractableDoor)
        {
            ToggleDoorPosition();
        }
        else
        {
            if (doorLockedSound != null)
                doorAudioSource.PlayOneShot(doorLockedSound);
        }
    }

    public string GetInteractText()
    {
        return interactionContext;
    }

    public void ToggleDoorPosition()
    {
        doorAnimator.SetTrigger(animatorTrigger);
        if (doorIsOpen)
        {
            if(doorOpeningSound != null)
                doorAudioSource.PlayOneShot(doorOpeningSound);
        }
        else
        {
            if (doorClosingSound != null)

                doorAudioSource.PlayOneShot(doorClosingSound);

        }
        doorIsOpen = !doorIsOpen;

    }

    public void DisableDoorMovement()
    {
        if (doorIsOpen) 
            ToggleDoorPosition();
        isAnInteractableDoor = false;
        interactionContext = "Locked Door";

    }

    public void DisableISInteractable()
    {
        isAnInteractableDoor = false;
    }
    

    public void EnableDoorMovement()
    {
        isAnInteractableDoor = true;
        interactionContext = "Door";

    }

    public void EnableAndOpenTheDoor()
    {
        if (!doorIsOpen)
            ToggleDoorPosition();


        isAnInteractableDoor = true;
        interactionContext = "Door";

    }
    public Transform GetTransform()
    {
        return transform;
    }
}
