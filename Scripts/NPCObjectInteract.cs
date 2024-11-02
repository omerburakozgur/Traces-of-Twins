using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCObjectInteract : MonoBehaviour, IInteractable {
    [SerializeField] private string interactText;

    public void Interact()
    {
        Debug.Log(interactText);
    }

    public string GetInteractText()
    {
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
