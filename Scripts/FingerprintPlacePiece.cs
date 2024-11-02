using UnityEngine;
using UnityEngine.EventSystems;

public class FingerprintPlacePiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [SerializeField] GameObject correctForm;

    private bool moving;
    private bool finish = false;
    private Vector2 _offset, _originalPosition;
    [SerializeField] private FingerprintPlaceManager FingerprintPlaceManagerReference;

    private void Awake()
    {
        _originalPosition = transform.position;
    }

    
    public void OnBeginDrag(PointerEventData eventData)
    {
        AudioManager.PlayRandomSound(AudioManager.Instance.digitalClickSounds);


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
            FingerprintPlaceManagerReference.AddPoints();
            AudioManager.PlayRandomSound(AudioManager.Instance.digitalClickSounds);
            finish = true;
            Destroy(this);
        }
        else
        {
            transform.position = _originalPosition;
        }

    }
}
