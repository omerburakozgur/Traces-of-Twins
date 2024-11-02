using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{

    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();

    }

    public void DisableFadePanel()
    {
        gameObject.SetActive(false);
    }

    public void PlayCarDoorOpeningSound()
    {
        AudioManager.PlayStingSound(AudioManager.Instance.carDoorOpeningSound);

    }

    public void PlayCarDoorClosingSound()
    {
        AudioManager.PlayStingSound(AudioManager.Instance.carDoorClosingSound);
    }

    public void LoadCredits()
    {

        GameManager.Instance.LoadCredits();
    }

}
