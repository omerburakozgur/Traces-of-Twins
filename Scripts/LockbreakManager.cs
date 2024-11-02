using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockbreakManager : MonoBehaviour
{
    private bool inZone = false;
    public LockbreakMovingCollider LockbreakMovingColliderReference;
    [SerializeField] public GameObject posAObject, posBObject;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        inZone = true;
        print("ontriggerenter");

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inZone = false;
        print("ontriggerexit");

    }

    public void CheckIfPickable()
    {
        if (inZone)
        {
            LockbreakMovingColliderReference.GameWon();
            // stop moving platform
            // update game counter
        }
        else
        {
            // play lockpick break sound
            LockbreakMovingColliderReference.FailedToHit();
            print("lockpick is broken, reset the position");
        }

    }

    public void RandomizePosition()
    {
        float verticalPos = Random.Range(posAObject.transform.position.y, posBObject.transform.position.y);
        this.transform.position = new Vector2(posAObject.transform.position.x, verticalPos);

    }


}
