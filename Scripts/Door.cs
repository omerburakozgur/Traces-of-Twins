using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeneGames.DialogueSystem
{
    public class Door : MonoBehaviour
    {
        private bool open;
        private Vector3 doorLocalStartPosition;

        [SerializeField] private Transform doorTransform;
        [SerializeField] private Transform openTransform;

        private void Start()
        {
            doorLocalStartPosition = doorTransform.localPosition;
        }

        private void Update()
        {
            if(open)
            {
                doorTransform.localPosition = Vector3.Lerp(doorTransform.localPosition, openTransform.localPosition, 1f * Time.deltaTime);
            }
            else
            {
                doorTransform.localPosition = Vector3.Lerp(doorTransform.localPosition, doorLocalStartPosition, 1f * Time.deltaTime);
            }
        }

        public void OpenDoor(bool _value)
        {
            open = _value;
        }
    }
}