using UnityEngine;

public class FingerprintPlaceManager : MonoBehaviour
{

    public int pointsToComplete;
    private int currentPoints;
    public GameObject fingerprintPiece;
    [SerializeField] GameObject FingerprintPlaceMinigameCanvas;
    bool gameEndStarted = false;
    public GameObject[] objectsToEnable;

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
        GameWon();
    }

    public void AddPoints()
    {
        currentPoints++;
        print(currentPoints.ToString());
    }

    void GameWon()
    {
        if (!gameEndStarted)
        {
            if (currentPoints >= pointsToComplete)
            {
                gameEndStarted = true;
                currentPoints = 0;
                foreach (var item in objectsToEnable)
                {
                    item.SetActive(true);
                }
                GameManager.Instance.IncreaseFingerprintCounter();
                AudioManager.PlaySound(AudioManager.Instance.genericMinigameWinSound);
                GameManager.Instance.WaitForSecondsMethod(StaticVariables.Variables.minigameEndWaitTime, FingerprintPlaceMinigameCanvas);

            }
        }
    }
}
