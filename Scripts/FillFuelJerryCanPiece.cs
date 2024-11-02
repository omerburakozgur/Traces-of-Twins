using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FillFuelJerryCanPiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [SerializeField] GameObject correctForm;

    private bool moving;
    private bool finish = false;

    private Vector2 _offset, _originalPosition;
    [SerializeField] private FillFuelManager fillFuelManagerReference;
    public bool movementEnabled = false;

    private void Awake()
    {
        _originalPosition = transform.position;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (movementEnabled)
            AudioManager.PlaySound(AudioManager.Instance.fuelCanDrag);



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
            fillFuelManagerReference.AddMatchPoints();
            AudioManager.PlaySound(AudioManager.Instance.fuelCanDrag);
            finish = true;
            Destroy(this);
        }
        else
        {
            transform.position = _originalPosition;
        }

    }
}
