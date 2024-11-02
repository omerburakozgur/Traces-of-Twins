using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.UI;

public class ElectricitySwitchPiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [SerializeField] GameObject correctForm;

    private bool moving;
    private bool finish = false;

    private Vector2 _offset, _originalPosition;
    [SerializeField] private ElectricityBoxManager ElectricityBoxManagerReference;
    public bool movementEnabled = false;
    public GameObject[] objectsToEnable;
    public GameObject[] objectsToDisable;

    public ElectricitySwitchPiece draggableObjectToEnable;



    private void Awake()
    {
        _originalPosition = transform.position;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (movementEnabled)
            AudioManager.PlayRandomSound(AudioManager.Instance.switchToggleSounds);


    }

    public void OnDrag(PointerEventData eventData)
    {
        if (movementEnabled)
            transform.position = Input.mousePosition;

    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (Mathf.Abs(this.transform.localPosition.x - correctForm.transform.localPosition.x) <= 30f &&
            Mathf.Abs(this.transform.localPosition.y - correctForm.transform.localPosition.y) <= 30f)
        {
            transform.localPosition = correctForm.transform.localPosition;
            foreach (var item in objectsToEnable)
            {
                item.SetActive(true);
            }
            foreach (var item in objectsToDisable)
            {
                item.SetActive(false);
            }

            if (draggableObjectToEnable != null)
                draggableObjectToEnable.movementEnabled = true;

            AudioManager.PlayRandomSound(AudioManager.Instance.switchToggleSounds);
            finish = true;
            Destroy(this);
        }
        else
        {
            transform.position = _originalPosition;
        }

    }
}
