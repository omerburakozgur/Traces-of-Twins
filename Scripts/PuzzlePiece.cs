using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{

    [SerializeField] GameObject correctForm;
    public AudioManager AudioManagerReference;

    private bool moving;
    private bool finish = false;
    private Vector2 _offset, _originalPosition;

    private void Awake()
    {
        _originalPosition = transform.position;
    }

    private void Update()
    {
        if (!finish)
        {
            if (!moving) return;

            var mousePosition = GetMousePos();
            transform.position = mousePosition - _offset;
        }

    }

    void OnMouseDown()
    {
        moving = true;
        //play pick up clip from audio manager
        _offset = GetMousePos() - (Vector2)transform.position;
    }

    private void OnMouseUp()
    {
        moving = false;
        //play drop clip from audio manager

        if (Mathf.Abs(this.transform.localPosition.x - correctForm.transform.localPosition.x) <= 0.5f &&
            Mathf.Abs(this.transform.localPosition.y - correctForm.transform.localPosition.y) <= 0.5f)
        {
            transform.localPosition = correctForm.transform.localPosition;
            print("asd");
            GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>().AddPoints();
            finish = true;
            GameObject.Destroy(this);
        }
        else
        {
            transform.position = _originalPosition;
        }




    }

    Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}
