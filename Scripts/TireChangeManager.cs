using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TireChangeManager : MonoBehaviour
{
    [Header("Game Points")]
    [SerializeField] private int targetMatchPoints;
    [SerializeField] private int currentMatchPoints;

    [Header("Bolt Button Points")]
    [SerializeField] private int targetBoltPoints;
    [SerializeField] private int currentBoltPoints;

    [Header("Bolt Button Points")]
    public TireChangePiece firstTirePiece;
    public TireChangePiece secondTirePiece;

    [Header("Second Tire Bolts")]
    [SerializeField] public Button[] secondTireBoltButtons;

    [Header("Tire Models")]
    [SerializeField] private GameObject damagedTire;
    [SerializeField] private GameObject newTire;

    [SerializeField] GameObject TireChangeMinigameCanvas;
    bool gameEndStarted = false;

    private void OnEnable()
    {
        GameManager.Instance.StopPlayerAndCameraMovement();
    }

    private void OnDisable()
    {
        GameManager.Instance.ResumePlayerAndCameraMovement();

    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        GameWon();

    }

    public void AddMatchPoints()
    {
        currentMatchPoints++;
        print("tire change match points " + currentMatchPoints.ToString());

        
    }

    public void AddBoltPoints()
    {
        currentBoltPoints++;
        print("tire change bolt points " + currentBoltPoints.ToString());


    }

    void GameWon()
    {
        if (currentBoltPoints == targetBoltPoints / 2 && currentMatchPoints == 1)
        {
            firstTirePiece.movementEnabled = true;

        }

        if (currentMatchPoints >= 2)
        {
            secondTirePiece.movementEnabled = true;

        }

        if (currentMatchPoints == 3)
        {
            foreach (var item in secondTireBoltButtons)
            {
                item.interactable = true;
            }

        }

        if (!gameEndStarted)
        {
            if (currentMatchPoints >= targetMatchPoints && currentBoltPoints >= targetBoltPoints)
            {
                GameManager.Instance.IncreaseCarFixCounter();
                gameEndStarted = true;
                currentMatchPoints = 0;
                AudioManager.PlayRandomSound(AudioManager.Instance.tireChangeClickSounds);
                GameManager.Instance.WaitForSecondsMethod(StaticVariables.Variables.minigameEndWaitTime, TireChangeMinigameCanvas);
                //newTire.SetActive(true);
                //damagedTire.SetActive(false);


            }
        }
    }
}
