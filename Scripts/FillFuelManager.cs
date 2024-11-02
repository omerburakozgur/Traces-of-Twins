using UnityEngine;


public class FillFuelManager : MonoBehaviour
{
    [Header("Game Points")]
    [SerializeField] private int targetMatchPoints;
    [SerializeField] private int currentMatchPoints;

    [Header("Lid Points")]
    [SerializeField] private int lidTargetPoints;
    [SerializeField] private int lidCurrentPoints;

    [Header("Button and Image References")]
    public GameObject fillFuelButton;
    public GameObject fuelLidButton;
    public GameObject fuelLidImage;
    public GameObject coverClosedImage;
    public GameObject coverOpenImage;
    [SerializeField] public UnityEngine.UI.Slider fuelProgressSlider;

    [Header("Jerry Can References")]
    public FillFuelJerryCanPiece jerryCanReference;

    [SerializeField] GameObject FillFuelMinigameCanvas;
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
        AudioManager.PlayRandomSound(AudioManager.Instance.fillFuelLiquidSounds);

        switch (currentMatchPoints)
        {
            case 1:
                fillFuelButton.SetActive(true);
                break;
            case 2:
                fuelProgressSlider.value = 0.2f;
                break;
            case 3:
                fuelProgressSlider.value = 0.4f;
                break;
            case 4:
                fuelProgressSlider.value = 0.5f;
                break;
            case 5:
                fuelProgressSlider.value = 0.8f;
                break;
            case 6:
                fuelProgressSlider.value = 1f;
                fillFuelButton.SetActive(false);
                print("fuel is filled up");
                fuelLidButton.SetActive(true);
                fuelLidImage.SetActive(true);
                break;
        }


    }

    public void AddFuelPoints()
    {
        lidCurrentPoints++;
        print("fuel points " + lidCurrentPoints.ToString());
        AudioManager.PlayRandomSound(AudioManager.Instance.fillFuelLiquidSounds);


        switch (lidCurrentPoints)
        {
            case 1:
                coverClosedImage.SetActive(false);
                coverOpenImage.SetActive(true);
                break;
            case 5:
                fuelLidButton.SetActive(false);
                fuelLidImage.SetActive(false);
                jerryCanReference.movementEnabled = true;
                break;
            case 10:
                fuelLidButton.SetActive(false);
                coverOpenImage.SetActive(false);
                coverClosedImage.SetActive(true);
                break;
        }

    }

    void GameWon()
    {

        if (!gameEndStarted)
        {
            if (currentMatchPoints >= targetMatchPoints && lidCurrentPoints >= lidTargetPoints)
            {
                GameManager.Instance.IncreaseCarFixCounter();
                gameEndStarted = true;
                currentMatchPoints = 0;
                AudioManager.PlaySound(AudioManager.Instance.genericMinigameWinSound);
                GameManager.Instance.WaitForSecondsMethod(StaticVariables.Variables.minigameEndWaitTime, FillFuelMinigameCanvas);

            }
        }
    }
}
