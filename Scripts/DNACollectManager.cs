using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNACollectManager : MonoBehaviour
{
    [Header("Game Points")]
    [SerializeField] private int targetMatchPoints;
    [SerializeField] private int currentMatchPoints;

    [Header("Button and Image References")]
    public GameObject[] dirtPieces;
    //public GameObject[] dirtSlots;

    [SerializeField] public UnityEngine.UI.Slider fuelProgressSlider;

    [Header("Collectibe DNA Reference")]
    public GameObject CollectibleDNAPiece;

    [Header("DNA Collect Backround Reference")]
    public GameObject grave6;
    public GameObject grave5;
    public GameObject grave4;
    public GameObject grave3;
    public GameObject grave2;
    public GameObject grave1;

    [SerializeField] GameObject DNACollectManagerCanvas;
    bool gameEndStarted = false;

    private void OnEnable()
    {
        GameManager.Instance.StopPlayerAndCameraMovement();
    }

    private void OnDisable()
    {
        GameManager.Instance.ResumePlayerAndCameraMovement();

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        GameWon();

    }

    public void AddMatchPoints()
    {
        currentMatchPoints++;
        print("fill fuel match points " + currentMatchPoints.ToString());
        AudioManager.PlayRandomSound(AudioManager.Instance.dnaCollectSounds);

        switch (currentMatchPoints)
        {
            case 1:
                dirtPieces[0].SetActive(true);
                break;
            case 2:
                grave6.SetActive(false);
                dirtPieces[0].SetActive(false);
                dirtPieces[1].SetActive(true);
                fuelProgressSlider.value = 0.8f;
                break;
            case 3:
                grave5.SetActive(false);
                dirtPieces[1].SetActive(false);
                dirtPieces[2].SetActive(true);
                fuelProgressSlider.value = 0.6f;
                break;
            case 4:
                grave4.SetActive(false);
                dirtPieces[2].SetActive(false);
                dirtPieces[3].SetActive(true);
                fuelProgressSlider.value = 0.4f;
                break;
            case 5:
                grave3.SetActive(false);
                dirtPieces[3].SetActive(false);
                dirtPieces[4].SetActive(true);
                fuelProgressSlider.value = 0.2f;
                break;
            case 6:
                grave2.SetActive(false);
                dirtPieces[4].SetActive(false);
                CollectibleDNAPiece.SetActive(true);
                fuelProgressSlider.value = 0.0f;
                break;
            case 7:
                dirtPieces[0].SetActive(false);
                dirtPieces[5].SetActive(true);
                AudioManager.PlaySound(AudioManager.Instance.dnaCollectBoneSound);

                break;
            case 8:
                dirtPieces[5].SetActive(false);
                dirtPieces[6].SetActive(true);
                fuelProgressSlider.value = 0.2f;
                grave2.SetActive(true);
                break;
            case 9:
                dirtPieces[6].SetActive(false);
                dirtPieces[7].SetActive(true);
                fuelProgressSlider.value = 0.4f;
                grave3.SetActive(true);
                break;
            case 10:
                dirtPieces[7].SetActive(false);
                dirtPieces[8].SetActive(true);
                fuelProgressSlider.value = 0.6f;
                grave4.SetActive(true);
                break;
            case 11:
                dirtPieces[8].SetActive(false);
                dirtPieces[9].SetActive(true);
                fuelProgressSlider.value = 0.8f;
                grave5.SetActive(true);
                break;
            case 12:
                dirtPieces[9].SetActive(false);
                grave6.SetActive(true);
                fuelProgressSlider.value = 1f;
                break;
        }


    }
    void GameWon()
    {

        if (!gameEndStarted)
        {
            if (currentMatchPoints >= targetMatchPoints)
            {
                gameEndStarted = true;
                currentMatchPoints = 0;
                GameManager.Instance.IncreaseDNACollectCounter();
                AudioManager.PlaySound(AudioManager.Instance.genericMinigameWinSound);
                GameManager.Instance.WaitForSecondsMethod(StaticVariables.Variables.minigameEndWaitTime, DNACollectManagerCanvas);

            }
        }
    }
}
