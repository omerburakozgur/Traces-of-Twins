using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SimonManager : MonoBehaviour
{

    public List<Button> buttons;
    public List<Button> shuffledButtons;
    int counter = 0;
    int gameCounter = 0;
    [SerializeField] GameObject SimonSaysCanvas;
    public GameObject[] objectsToEnable;

    [Header("Door References")]
    [SerializeField] Animator doorAnimator;
    [SerializeField] InteractableObjectDoor garageDoor;
    [SerializeField] InteractableObjectDoor mainDoor;
    [SerializeField] InteractableObjectDoor secondaryDoor;


    private void OnEnable()
    {
        GameManager.Instance.StopPlayerAndCameraMovement();
    }

    private void OnDisable()
    {
        GameManager.Instance.ResumePlayerAndCameraMovement();

    }

    void Start()
    {
        RestartTheGame();
    }

    public void RestartTheGame()
    {
        // play start sound
        counter = 0;
        shuffledButtons = buttons.OrderBy(a => Random.Range(0, 100)).ToList();
        for (int i = 1; i < 11; i++)
        {
            shuffledButtons[i - 1].GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();
            shuffledButtons[i - 1].interactable = true;
            shuffledButtons[i - 1].image.color = new Color32(177, 220, 233, 0);
            //
        }
    }

    public void pressButton(Button button)
    {
        AudioManager.PlayRandomSound(AudioManager.Instance.simonSaysClickSounds);

        if (int.Parse(button.GetComponentInChildren<TextMeshProUGUI>().text) - 1 == counter)
        {
            // play round won sound

            counter++;
            button.interactable = false;
            button.image.color = Color.green;
            if (counter == 10)
            {
                StartCoroutine(presentResult(true));
            }
        }
        else
        {
            // play fail sound
            StartCoroutine(presentResult(false));

        }
    }

    public IEnumerator presentResult(bool win)
    {
        if (!win)
        {
            foreach (var button in shuffledButtons)
            {
                button.image.color = Color.red;
                button.interactable = false;
            }
            AudioManager.PlaySound(AudioManager.Instance.simonSaysFailSound);
        }
        yield return new WaitForSeconds(2f);
        if (win)
        {
            print("won");
            gameCounter++;
            if (gameCounter < 3)
            {
                AudioManager.PlaySound(AudioManager.Instance.simonSaysWonSound);
                print($"Game Counter {gameCounter}");
                RestartTheGame();

            }
            else
            {
                foreach (var item in objectsToEnable)
                {
                    item.SetActive(true);
                }
                //doorAnimator.SetTrigger("Trigger");
                garageDoor.EnableAndOpenTheDoor();
                //garageDoor.isAnInteractableDoor = true;
                mainDoor.isAnInteractableDoor = true;
                secondaryDoor.isAnInteractableDoor = true;


                AudioManager.PlaySound(AudioManager.Instance.simonSaysGameWonSound);
                GameManager.Instance.WaitForSecondsMethod(StaticVariables.Variables.minigameEndWaitTime, SimonSaysCanvas);

                print("Game is finished");
            }

        }
        else
        {
            print($"Game Counter {gameCounter}");
            print("lost");
            RestartTheGame();
        }

    }

    // on enable game canvas call restart the game function
}
