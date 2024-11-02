using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DnaPlaceManager : MonoBehaviour
{

    public int pointsToComplete;
    private int currentPoints;
    public GameObject dnaPiece;
    [SerializeField] GameObject DnaPlaceMinigameCanvas;
    bool gameEndStarted = false;
    public GameObject[] objectsToEnable;

    [Header("Event")]
    public UnityEvent Event;

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
        GameWon();
    }

    public void AddPoints()
    {
        currentPoints++;
        print(currentPoints.ToString());
    }

    void GameWon()
    {
        if (!gameEndStarted)
        {
            if (currentPoints >= pointsToComplete)
            {
                Event.Invoke();
                gameEndStarted = true;
                currentPoints = 0;
                foreach (var item in objectsToEnable)
                {
                    item.SetActive(true);
                }
                AudioManager.PlaySound(AudioManager.Instance.genericMinigameWinSound);
                GameManager.Instance.WaitForSecondsMethod(StaticVariables.Variables.minigameEndWaitTime, DnaPlaceMinigameCanvas);
            }
        }
    }
}
