using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodelockButton : MonoBehaviour
{
    public static event Action<String> ButtonPressed = delegate { };
    private int deviderPosition;
    private string buttonName, buttonValue;

    void Start()
    {
        buttonName = gameObject.name;
        deviderPosition = buttonName.IndexOf("_");
        buttonValue = buttonName.Substring(0, deviderPosition);
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonClicked);


    }

    private void ButtonClicked()
    {
        AudioManager.PlaySound(AudioManager.Instance.digitalBeepSound);
        ButtonPressed(buttonValue);
    }
}
