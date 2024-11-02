using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FingerprintCheck : MonoBehaviour
{
    bool isCorrect = false;
    bool counterAlreadyDecreased = false;
    int placeholderCounter = 0;

    [SerializeField] FingerprintManager FingerprintManagerReference;

    [SerializeField] int CurrentSequence = 0;
    [SerializeField] int CorrectSequence = 0;
    [SerializeField] int SequenceCounter = 0;
    [SerializeField] Image ButtonImage;

    [SerializeField] Sprite[] Fingerprints = new Sprite[3];




    // 0 Adenine 1 Thymine 2 Guanine 3 Cytosine

    void Start()
    {

        changeAppereance();
        checkSequence();
        if (CurrentSequence != CorrectSequence)
        {
            FingerprintManagerReference.addFingerprintPoints(1);

            //placeholderCounter++;
        }

    }

    public void onSequenceChange()
    {
        AudioManager.PlayRandomSound(AudioManager.Instance.digitalClickSounds);
        CurrentSequence++;
        if (CurrentSequence >= 5)
            CurrentSequence = 0;


        print("Current Sequence"+ CurrentSequence);
        checkSequence();
        changeAppereance();

    }

    void checkSequence()
    {
        if (CurrentSequence == CorrectSequence)
        {
            isCorrect = true;
            FingerprintManagerReference.addFingerprintPoints(1);
            //placeholderCounter++;
            counterAlreadyDecreased = false;
        }
        else
        {
            isCorrect = false;
            if (counterAlreadyDecreased)
                return;
            else //(!counterAlreadyDecreased)
            {
                FingerprintManagerReference.addFingerprintPoints(-1);
                //placeholderCounter--;
                counterAlreadyDecreased = true;
            }

        }
        print("dnaMatchCounter : " + FingerprintManagerReference.dnaMatchCounter);
        //print("Counter: " + placeholderCounter);

    }

    void changeImage(int index)
    {
        this.ButtonImage.sprite = Fingerprints[index];
    }

    void changeAppereance()
    {
        switch (CurrentSequence)
        {
            case 0:
                changeImage(0);
                print("0");
                break;
            case 1:
                changeImage(1);
                print("1");
                break;
            case 2:
                changeImage(2);
                print("2");
                break;
            case 3:
                changeImage(3);
                print("3");
                break;
            case 4:
                changeImage(4);
                print("4");
                break;
            case 5:
                changeImage(0);
                print("reset 0");
                break;
        }
    }
}
