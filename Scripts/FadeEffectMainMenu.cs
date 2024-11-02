using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffectMainMenu : MonoBehaviour
{

    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Trigger");
    }

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Trigger");

    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Trigger");
    }

    public void DisableFadePanel()
    {
        gameObject.SetActive(false);
    }


}
