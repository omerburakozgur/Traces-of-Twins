using UnityEngine;
using UnityEngine.Events;

public class InteractableObjectCollectibleItem : MonoBehaviour, IInteractable
{
    public string interactionContext;
    [SerializeField] private GameObject[] objectsToEnable;
    [SerializeField] private GameObject[] objectsToDisable;
    [SerializeField] private bool isCollectable = true;

    [Header("Event")]
    public UnityEvent Event;

    public void Interact()
    {
        CollectItem();
    }

    public string GetInteractText()
    {
        return interactionContext;
    }

    public void CollectItem()
    {
        if (isCollectable) {

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

    }


    public Transform GetTransform()
    {
        return transform;
    }

    public void SetIsCollectibleTrue()
    {
       isCollectable = true;
    }
}
