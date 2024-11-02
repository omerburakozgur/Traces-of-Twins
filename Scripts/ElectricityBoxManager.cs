using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ElectricityBoxManager : MonoBehaviour
{
    public int openSwitchCounter = 0;
    public int totalSwitchCount = 10;
    [SerializeField] GameObject ElectricityBoxMinigame;
    bool gameEndStarted = false;
    public GameObject[] objectsToEnable;

    [Header("Event")]
    public UnityEvent toggleLightmapEvent;

    private void OnEnable()
    {
        GameManager.Instance.StopPlayerAndCameraMovement();
    }

    private void OnDisable()
    {
        GameManager.Instance.ResumePlayerAndCameraMovement();

    }

    // Update is called once per frame
    void Update()
    {
        gameEnd();
    }

    public void addSwitchPoints(int number)
    {
        openSwitchCounter += number;
        Debug.Log("switch counter" + openSwitchCounter);
    }

    void gameEnd()
    {

        if (!gameEndStarted)
        {
            if (openSwitchCounter == totalSwitchCount)
            {
                Debug.Log("open switch counter is " + openSwitchCounter + " which equals total switch count of " + totalSwitchCount);

                foreach (var item in objectsToEnable)
                {
                    item.SetActive(true);
                }

                gameEndStarted = true;
                GameManager.Instance.WaitForSecondsMethod(StaticVariables.Variables.minigameEndWaitTime, ElectricityBoxMinigame);

                if (toggleLightmapEvent != null)
                    toggleLightmapEvent.Invoke();

                AudioManager.PlaySound(AudioManager.Instance.powerEnabledSound);
                // wait 3 secs, disable game canvas, disable minigame button

            }
        }
    }
}
