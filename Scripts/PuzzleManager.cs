using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    public int pointsToComplete;
    private int currentPoints;
    public GameObject puzzlePieces;
    public GameObject[] objectsToEnable;

    [SerializeField] GameObject LetterPuzzleMinigameCanvas;

    private void OnEnable()
    {
        GameManager.Instance.StopPlayerAndCameraMovement();
    }

    private void OnDisable()
    {
        GameManager.Instance.ResumePlayerAndCameraMovement();

    }

    public void AddPoints()
    {
        currentPoints++;
        print(currentPoints.ToString());

        if (currentPoints >= pointsToComplete)
        {
            print("You won the letter puzzle game");
            foreach (var item in objectsToEnable)
            {
                item.SetActive(true);
            }

            AudioManager.PlayRandomSound(AudioManager.Instance.letterPaperSounds);

            GameManager.Instance.WaitForSecondsMethod(StaticVariables.Variables.minigameEndWaitTime, LetterPuzzleMinigameCanvas);

            currentPoints = 0;
            //disable canvas, enable the readable letter from UI
        }
    }
}
