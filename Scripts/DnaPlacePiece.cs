using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DnaPlacePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [SerializeField] GameObject correctForm;

    private bool moving;
    private bool finish = false;
    private Vector2 _offset, _originalPosition;
    [SerializeField] private DnaPlaceManager DnaPlaceManagerReference;

    private void Awake()
    {
        _originalPosition = transform.position;
    }

    
    public void OnBeginDrag(PointerEventData eventData)
    {
        AudioManager.PlayRandomSound(AudioManager.Instance.dnaPlaceClickSounds);



    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (Mathf.Abs(this.transform.localPosition.x - correctForm.transform.localPosition.x) <= 30f &&
            Mathf.Abs(this.transform.localPosition.y - correctForm.transform.localPosition.y) <= 30f)
        {
            transform.localPosition = correctForm.transform.localPosition;
            DnaPlaceManagerReference.AddPoints();
            AudioManager.PlayRandomSound(AudioManager.Instance.dnaPlaceClickSounds);

            finish = true;
            Destroy(this);
        }
        else
        {
            transform.position = _originalPosition;
        }

    }
}
