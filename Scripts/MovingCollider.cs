using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MovingCollider : MonoBehaviour
{
    [SerializeField] public GameObject posAObject, posBObject;

    [SerializeField] Transform posA, posB;

    Vector2 targetPos;
    private Vector2 startPosition;
    [Header("Minigame Variables")]

    int speed;
    [SerializeField] int gameCounter;
    [SerializeField] int pinCount;
    [SerializeField] RectTransform targetCollider;
    [SerializeField] RectTransform movingCollider;
    [SerializeField] int secondsToWait;
    [SerializeField] int secondsToWaitEnd;
    [SerializeField] float BtoAPixelDifference = 500;
    [SerializeField] Button interactionButton;


    private bool waitingForRestart = false;

    [Header("Animation References")]
    [SerializeField] LockpickManager lockpickManagerReference;
    [SerializeField] Animator firstPinAnim;
    [SerializeField] Animator secondPinAnim;
    [SerializeField] Animator thirdPinAnim;
    [SerializeField] Animator lockOpenAnim;
    [SerializeField] Animator chestAnimator;
    [SerializeField] GameObject LockpickMinigameCanvas;

    [Header("Event")]
    public UnityEvent Event;

    private string trigger = "LockTrigger";
    private string GateAnimatorTrigger = "Trigger";

    private void OnEnable()
    {
        GameManager.Instance.StopPlayerAndCameraMovement();
    }

    private void OnDisable()
    {
        GameManager.Instance.ResumePlayerAndCameraMovement();

    }

    // Start is called before the first frame update
    void Start()
    {
        targetPos = posBObject.transform.position;
        startPosition = this.transform.position;
        //BtoAPixelDifference = posB.transform.position.y - posA.transform.position.y;
        //speed = (int)BtoAPixelDifference / 2;

    }
    private void FixedUpdate()
    {
        FixTargetPos();
        SetSpeed();
        AdjustColliders();
        if (waitingForRestart)
        {
            interactionButton.interactable = false;
        }
        else
        {
            interactionButton.interactable = true;

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!waitingForRestart) MoveThePlatform();

    }

    private void MoveThePlatform()
    {

        // select target position for collider
        if (Vector2.Distance(transform.position, posAObject.transform.position) < 1f) targetPos = posBObject.transform.position;

        if (Vector2.Distance(transform.position, posBObject.transform.position) < 1f) targetPos = posAObject.transform.position;

        // move collider to target position which is either posA or posB
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        //print("Pixel difference between A and B: " + BtoAPixelDifference);
        //print("Target pos: " + targetPos);
        //print("Current pos: " + transform.position);
        //print("PosA: " + posAObject.transform.position);
        //print("PosB: " + posBObject.transform.position);

    }

    private void FixTargetPos()
    {
        if (targetPos.y != posBObject.transform.position.y && targetPos.y != posAObject.transform.position.y)
        {

            targetPos = posBObject.transform.position;
            transform.position = posAObject.transform.position;

        }
    }

    private void AdjustColliders()
    {
        print($"Moving collider X: {movingCollider.rect.width} Y:{movingCollider.rect.height}");
        print($"Target collider X: {targetCollider.rect.width} Y:{targetCollider.rect.height}");

        BoxCollider2D movingCollider2D = movingCollider.GetComponent<BoxCollider2D>();
        BoxCollider2D targetCollider2D = targetCollider.GetComponent<BoxCollider2D>();
        movingCollider2D.size = new Vector2(movingCollider.rect.width, movingCollider.rect.height);
        targetCollider2D.size = new Vector2(targetCollider.rect.width, targetCollider.rect.height);

    }


    private void SetSpeed()
    {
        BtoAPixelDifference = posB.transform.position.y - posA.transform.position.y;

        switch (gameCounter)
        {
            case 0:
                speed = (int)BtoAPixelDifference; // 1x

                break;
            case 1:
                speed = (int)((BtoAPixelDifference / 2) * 3); // 1.5x

                break;
            case 2:
                speed = (int)(BtoAPixelDifference * 2); // 2x

                break;

        }
    }

    public void GameWon()
    {

        waitingForRestart = true; // stop moving collider movement by changing waitingForRestart boolean to true 
        print("Finished Ienumerator");
        gameCounter++; // increase to game counter
        print("Position is set to start pos"); // debug
        print($"gamecounter: {gameCounter}"); // debug

        switch (gameCounter)
        {
            case 1:
                AudioManager.PlaySound(AudioManager.Instance.lockpickSuccessSound);
                firstPinAnim.SetTrigger(trigger);
                speed = (int)BtoAPixelDifference;
                break;
            case 2:
                AudioManager.PlaySound(AudioManager.Instance.lockpickSuccessSound);
                secondPinAnim.SetTrigger(trigger);
                speed = (int)((BtoAPixelDifference / 2) * 3);

                break;
            case 3:
                secondsToWait = secondsToWaitEnd;
                thirdPinAnim.SetTrigger(trigger);
                lockOpenAnim.SetTrigger(trigger);
                Event.Invoke();
                AudioManager.PlaySound(AudioManager.Instance.lockpickWonSound);
                AudioManager.Instance.stingSource.clip = null;
                if (chestAnimator != null)
                    chestAnimator.SetTrigger(GateAnimatorTrigger);
                // end the game disable the canvas
                break;

        }

        StartCoroutine(WaitForSeconds()); // wait three seconds

    }

    public void FailedToHit()
    {
        waitingForRestart = true; // stop moving collider movement by changing waitingForRestart boolean to true 
        StartCoroutine(WaitForSeconds()); // wait three seconds
        AudioManager.PlaySound(AudioManager.Instance.lockbreakFailSound);


    }

    public IEnumerator WaitForSeconds()
    {

        yield return new WaitForSeconds(secondsToWait);


        if (!(gameCounter == pinCount))
        {
            this.transform.position = posA.transform.position; // set moving collider position to default
            waitingForRestart = false; // restart the moving collider movement by changing waitingForRestart boolean to false 
            lockpickManagerReference.RandomizePosition();

        }
        else
        {
            LockpickMinigameCanvas.SetActive(false);
        }

    }
}
