using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class DigitalDisplay : MonoBehaviour
{
    [SerializeField] private Sprite[] digits;

    [SerializeField] private Image[] characters;
    [SerializeField] private string codelockPinCode = "5051";

    private string codeSequence;
    [SerializeField] private GameObject CodelockMinigameCanvas;
    [SerializeField] private Animator PSDoorAnimator;

    [Header("Event")]
    public InteractableObjectDoor utilityDoor;

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
        codeSequence = string.Empty;

        for (int i = 0; i < characters.Length - 1; i++)
        {
            characters[i].sprite = digits[10];
        }
        CodelockButton.ButtonPressed += AddDigitToCodeSequence;
    }

    private void AddDigitToCodeSequence(string digitEntered)
    {
        if (codeSequence.Length < 4)
        {
            switch (digitEntered)
            {
                case "Zero":
                    codeSequence += "0";
                    DisplayCodeSequence(0);
                    break;
                case "One":
                    codeSequence += "1";
                    DisplayCodeSequence(1);
                    break;
                case "Two":
                    codeSequence += "2";
                    DisplayCodeSequence(2);
                    break;
                case "Three":
                    codeSequence += "3";
                    DisplayCodeSequence(3);
                    break;
                case "Four":
                    codeSequence += "4";
                    DisplayCodeSequence(4);
                    break;
                case "Five":
                    codeSequence += "5";
                    DisplayCodeSequence(5);
                    break;
                case "Six":
                    codeSequence += "6";
                    DisplayCodeSequence(6);
                    break;
                case "Seven":
                    codeSequence += "7";
                    DisplayCodeSequence(7);
                    break;
                case "Eight":
                    codeSequence += "8";
                    DisplayCodeSequence(8);
                    break;
                case "Nine":
                    codeSequence += "9";
                    DisplayCodeSequence(9);
                    break;

            }
        }

        switch (digitEntered)
        {
            case "Star":
                ResetDisplay();
                break;
            case "Hash":
                if (codeSequence.Length > 0)
                {
                    CheckResults();
                }
                break;
        }
    }

    private void DisplayCodeSequence(int digitJustEntered)
    {
        switch (codeSequence.Length)
        {
            case 1:
                characters[0].sprite = digits[10];
                characters[1].sprite = digits[10];
                characters[2].sprite = digits[10];
                characters[3].sprite = digits[digitJustEntered];
                break;
            case 2:
                characters[0].sprite = digits[10];
                characters[1].sprite = digits[10];
                characters[2].sprite = characters[3].sprite;
                characters[3].sprite = digits[digitJustEntered];
                break;
            case 3:
                characters[0].sprite = digits[10];
                characters[1].sprite = characters[2].sprite;
                characters[2].sprite = characters[3].sprite;
                characters[3].sprite = digits[digitJustEntered];
                break;
            case 4:
                characters[0].sprite = characters[1].sprite;
                characters[1].sprite = characters[2].sprite;
                characters[2].sprite = characters[3].sprite;
                characters[3].sprite = digits[digitJustEntered];
                break;
        }
    }

    private void CheckResults()
    {
        if (codeSequence == codelockPinCode)
        {
            print("Correct");
            //Play Sound
            //Success
            AudioManager.PlaySound(AudioManager.Instance.digitalGameWonSound);
            GameManager.Instance.WaitForSecondsMethod(StaticVariables.Variables.minigameEndWaitTime, CodelockMinigameCanvas);
            PSDoorAnimator.SetTrigger("Trigger");
            utilityDoor.isAnInteractableDoor = true;


        }
        else
        {
            print("Wrong");
            ResetDisplay();
            //Play Fail Sound
            AudioManager.PlaySound(AudioManager.Instance.digitalFailSound);


        }
    }

    private void ResetDisplay()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].sprite = digits[10];
        }
        // characters[3].sprite = digits[10];
        codeSequence = "";
    }

    private void OnDestroy()
    {
        CodelockButton.ButtonPressed -= AddDigitToCodeSequence;
    }
}
