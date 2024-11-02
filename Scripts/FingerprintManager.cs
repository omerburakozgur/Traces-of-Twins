using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FingerprintManager : MonoBehaviour
{
    public int dnaMatchCounter = 0;
    public int sequenceCount = 5;
    public Button[] fingerprintButton;
    [SerializeField] GameObject FingerprintMinigameCanvas;
    bool gameEndStarted = false;

    public GameObject[] objectsToEnable;
    public Button buttonToDisable;

    [Header("Event")]
    public UnityEvent gameEndEvent;

    private void OnEnable()
    {
        GameManager.Instance.StopPlayerAndCameraMovement();
    }

    private void OnDisable()
    {
        GameManager.Instance.ResumePlayerAndCameraMovement();

    }

    // Update is called once per frame
    void Update()
    {
        gameEnd();
    }

    public void addFingerprintPoints(int number)
    {
        dnaMatchCounter += number;

    }

    void gameEnd()
    {
        if (!gameEndStarted)
        {
            if (dnaMatchCounter == sequenceCount)
            {
                gameEndStarted = true;
                buttonToDisable.interactable = false;

                AudioManager.PlaySound(AudioManager.Instance.genericMinigameWinSound);

                foreach (Button button in fingerprintButton)
                {
                    button.interactable = false;
                }

                foreach (var item in objectsToEnable)
                {
                    item.SetActive(true);
                }
                GameManager.Instance.WaitForSecondsMethod(StaticVariables.Variables.minigameEndWaitTime, FingerprintMinigameCanvas);
                gameEndEvent.Invoke();
                // do some stuff same as in DNA match
            }
        }
    }
}


