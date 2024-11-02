using UnityEngine;
using UnityEngine.Events;

public class InteractableObjectMinigame : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactionContext;
    [SerializeField] private GameObject[] objectsToEnable;
    [SerializeField] private GameObject[] objectsToDisable;

    [Header("Event")]
    public UnityEvent Event;



    public void Interact()
    {
        EnableMinigame();
    }

    public string GetInteractText()
    {
        return interactionContext;
    }

    public void EnableMinigame()
    {
        UIManager.Instance.PlayFadeEffect();

        if (objectsToEnable != null)
        {
            foreach (GameObject item in objectsToEnable)
            {
                item.SetActive(true);

            }

        }

        if (objectsToDisable != null)
        {
            foreach (GameObject item in objectsToDisable)
            {
                item.SetActive(false);

            }

        }

        Event.Invoke();
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
