using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricitySwitchManager : MonoBehaviour
{
    public ElectricityBoxManager electricityBoxManagerReference;
    public bool switchIsEnabled = false;
    public GameObject switchSprite;
    public GameObject switchLight;


    public void ToggleSwitch()
    {
        AudioManager.Instance.ButtonClick();
        if (switchIsEnabled)
        {
            switchLight.SetActive(false);
            switchSprite.SetActive(false);
            electricityBoxManagerReference.addSwitchPoints(-1);
            switchIsEnabled = false;
        }
        else
        {
            switchLight.SetActive(true);
            switchSprite.SetActive(true);
            electricityBoxManagerReference.addSwitchPoints(1);
            switchIsEnabled = true;


        }

    }
}
