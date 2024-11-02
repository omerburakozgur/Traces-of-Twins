using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DnaCollectPiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] GameObject correctForm;

    private bool moving;
    private bool finish = false;

    private Vector2 _offset, _originalPosition;
    [SerializeField] private DNACollectManager dnaCollectManagerReference;
    public bool movementEnabled = false;

    private void Awake()
    {
        _originalPosition = transform.position;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (movementEnabled)
            AudioManager.PlayRandomSound(AudioManager.Instance.dnaCollectSounds);


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
            dnaCollectManagerReference.AddMatchPoints();
            AudioManager.PlayRandomSound(AudioManager.Instance.dnaCollectSounds);
            finish = true;
            Destroy(this);
        }
        else
        {
            transform.position = _originalPosition;
        }

    }
}
