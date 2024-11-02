using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] float interactionRange = 4f;

    // Update is called once per frame
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            IInteractable interactable = GetInteractableObject();
            if (interactable != null)
            {
                interactable.Interact();
                AudioManager.Instance.ButtonClick();
            }
        }

    }

    public IInteractable GetInteractableObject()
    {
        List<IInteractable> interactableList = new List<IInteractable>();
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactionRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                interactableList.Add(interactable);
            }

        }

        IInteractable closestInteractable = null;
        foreach (IInteractable interactable in interactableList)
        {
            if (closestInteractable == null)
            {
                closestInteractable = interactable;
            }
            else
            {
                if (Vector3.Distance(transform.position, interactable.GetTransform().position) < Vector3.Distance(transform.position, closestInteractable.GetTransform().position))
                {
                    closestInteractable = interactable;
                }
            }
        }

        return closestInteractable;

    }

    public void InteractWithObject()
    {
        IInteractable interactable = GetInteractableObject();
        if (interactable != null)
        {
            interactable.Interact();
            
        }
    }

    //public void InteractWithObject()
    //{
    //    Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactionRange);
    //    foreach (Collider collider in colliderArray)
    //    {
    //        if (collider.TryGetComponent(out IInteractable interactable))
    //        {
    //            interactable.Interact();
    //        }

    //    }
    //}
}
