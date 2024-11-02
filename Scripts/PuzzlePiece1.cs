using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece1 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [SerializeField] GameObject correctForm;

    private bool moving;
    private bool finish = false;
    private Vector2 _offset, _originalPosition;

    private void Awake()
    {
        _originalPosition = transform.position;
    }

    
    public void OnBeginDrag(PointerEventData eventData)
    {
        AudioManager.PlayRandomSound(AudioManager.Instance.letterPaperSounds);

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("end drag");

        //play drop clip from audio manager
        AudioManager.PlayRandomSound(AudioManager.Instance.letterPaperSounds);


        if (Mathf.Abs(this.transform.localPosition.x - correctForm.transform.localPosition.x) <= 30f &&
            Mathf.Abs(this.transform.localPosition.y - correctForm.transform.localPosition.y) <= 30f)
        {
            transform.localPosition = correctForm.transform.localPosition;
            GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>().AddPoints();
            finish = true;
            Destroy(this);
        }
        else
        {
            transform.position = _originalPosition;
        }

    }
}
