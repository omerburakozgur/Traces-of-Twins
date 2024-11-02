using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DnaManager : MonoBehaviour
{
    public int dnaMatchCounter = 0;
    public int sequenceCount = 5;
    [SerializeField] GameObject DNAMatchMinigame;
    public GameObject[] objectsToEnable;
    public Button buttonToDisable;


    bool gameEndStarted = false;

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

    public void addDnaPoints(int number)
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
                foreach (var item in objectsToEnable)
                {
                    item.SetActive(true);
                }
                buttonToDisable.interactable = false;
                GameManager.Instance.WaitForSecondsMethod(StaticVariables.Variables.minigameEndWaitTime, DNAMatchMinigame);
                gameEndEvent.Invoke();
                AudioManager.PlaySound(AudioManager.Instance.genericMinigameWinSound);
                // wait 3 secs, disable game canvas, disable minigame button

            }
        }
    }
}
