using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DnaCheck : MonoBehaviour
{
    bool isCorrect = false;
    bool counterAlreadyDecreased = false;
    int placeholderCounter = 0;
    [SerializeField] DnaManager DNAManagerReference;

    [SerializeField] int CurrentSequence = 0;
    [SerializeField] int CorrectSequence = 0;
    [SerializeField] int SequenceCounter = 0;
    [SerializeField] Image ButtonImage;
    [SerializeField] TextMeshProUGUI ButtonText;

    [SerializeField] Sprite AdenineImage, ThymineImage, GuanineImage, CythosineImage;

    Image dnaImage;

    // 0 Adenine 1 Thymine 2 Guanine 3 Cytosine

    void Start()
    {
        changeAppereance();
        checkSequence();
        if (CurrentSequence != CorrectSequence)
        {
            DNAManagerReference.addDnaPoints(1);

            //placeholderCounter++;
        }

    }

    public void onSequenceChange()
    {
        CurrentSequence++;
        if (CurrentSequence == 4)
            CurrentSequence = 0;
        AudioManager.PlayRandomSound(AudioManager.Instance.digitalClickSounds);
        print(CurrentSequence);
        checkSequence();
        changeAppereance();

    }

    void checkSequence()
    {
        if (CurrentSequence == CorrectSequence)
        {
            isCorrect = true;
            DNAManagerReference.addDnaPoints(1);
            //placeholderCounter++;
            counterAlreadyDecreased = false;
        }
        else
        {
            isCorrect = false;
            if (counterAlreadyDecreased)
                return;
            if (!counterAlreadyDecreased)
            {
                DNAManagerReference.addDnaPoints(-1);
                //placeholderCounter--;
                counterAlreadyDecreased = true;
            }

        }
        print("Counter: " + DNAManagerReference.dnaMatchCounter);
        //print("Counter: " + placeholderCounter);

    }

    void changeText(string text)
    {
        ButtonText.text = text;
    }

    void changeAppereance()
    {
        dnaImage = GetComponent<Image>();

        switch (CurrentSequence)
        {
            case 0:
                dnaImage.sprite = AdenineImage;
                changeText("A");
                break;
            case 1:
                dnaImage.sprite = ThymineImage;
                changeText("T");
                break;
            case 2:
                dnaImage.sprite = GuanineImage;
                changeText("G");
                break;
            case 3:
                dnaImage.sprite = CythosineImage;
                changeText("C");
                break;
        }
    }
}
