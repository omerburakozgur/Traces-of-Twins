using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using FirstPersonMobileTools.DynamicFirstPerson;

public class GameManager : MonoBehaviour
{
    [Header("Game Variables")]
    public bool GameIsPaused = false;
    public int levelCounter = 0;

    [Header("UI References")] // get these from UIManager

    public TextMeshProUGUI characterName;
    public CanvasGroup background;

    [Header("Lightmap Variables")]
    //reference to existing scene lightmap data.
    LightmapData[] lightmap_data;
    [SerializeField] bool lightmapsEnabled = true;
    [SerializeField] Material PSFloorMaterial;
    [SerializeField] Material PSDoorMaterial;
    [SerializeField] Material PSMetalDoorMaterial;
    [SerializeField] Material PSTechnicianNPCMaterial;
    private Color PSFloorMaterialOriginalColor;
    private Color PSDoorMaterialOriginalColor;
    private Color PSMetalDoorMaterialOriginalColor;
    private Color PSTechnicianNPCMaterialOriginalColor;
    public Color TargetColor;
    public Color NPCTargetColor;

    [Header("Player References")]
    public GameObject playerGameObject;
    public FirstPersonController playerMovementControllerScript;
    public MovementController mobilePlayerMovementControllerScript;
    public CharacterController mobileCharacterController;
    public GameObject mobileControllerPanel;

    private float tempWalkSpeed = 0.0f;
    private float tempRunSpeed = 0.0f;
    public bool collectedFLashlight = false;

    [Header("Travel References")]
    public GameObject crimeScenePosition;
    public GameObject policeStationPosition;
    public GameObject graveyardPosition;
    public GameObject travelMenuCanvas;
    public GameObject travelFadePanel;
    public Vector3 vector;

    public float travelSeconds;

    [Header("Fingerprint Place References")]
    public int fingerprintPlaceCounter = 0;
    public int fingerprintPlaceTargetCounter = 3;

    public GameObject fingerprintsCollectedDialogue;

    [Header("Car Fix References")]
    public int carFixedCounter = 0;
    public int carFixedTargetCounter = 2;
    public GameObject carFixedDialogue;

    [Header("Car Fix Collectible Item References")]
    public int carItemCounter = 0;
    public int carItemTargetCounter = 3;
    public GameObject carItemsCollectedDialogue;

    [Header("DNA Collect References")]
    public int dnaCollectCounter = 0;
    public int dnaCollectTargetCounter = 3;
    public GameObject dnaCollectDialogue;

    [Header("Securing Police Station References")]
    public int psSecurityCounter = 0;
    public int psSecurityTargetCounter = 3;
    public GameObject psSecuredDialogue;

    [Header("DNA and Fingerprint Minigame References")]
    public GameObject computerCanvas;
    public int dnaMinigameCounter = 0;
    public int dnaMinigameTargetCounter = 4;
    public int fingerprintMinigameCounter = 0;
    public int fingerprintMinigameTargetCounter = 3;
    public GameObject labsFirstDialogue;
    public GameObject labsSecondDialogue;
    public bool labFirstReviewIsDone = false;
    public bool labSecondReviewIsDone = false;
    public UnityEngine.UI.Button[] DNAMatchButtons;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    _instance = obj.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {


        // Save reference to existing scene lightmap data.
        lightmap_data = LightmapSettings.lightmaps;
        PSFloorMaterialOriginalColor = PSFloorMaterial.color;
        PSDoorMaterialOriginalColor = PSDoorMaterial.color;
        PSMetalDoorMaterialOriginalColor = PSMetalDoorMaterial.color;
        PSTechnicianNPCMaterialOriginalColor = PSTechnicianNPCMaterial.color;

        if (StaticVariables.Character.selectedCharacter == 0)
        {
            print("Birinci Karakter");
            // set the default character bame and image to the selected one
        }
        else
        {
            print("Ikinci Karakter");
        }
    }

    public void ToggleMaterialColorsElectricity()
    {
        if (!lightmapsEnabled)
        {
            PSFloorMaterial.color = TargetColor;
            PSDoorMaterial.color = TargetColor;
            PSMetalDoorMaterial.color = TargetColor;
            PSTechnicianNPCMaterial.color = NPCTargetColor;
        }
        else
        {
            PSFloorMaterial.color = PSFloorMaterialOriginalColor;
            PSDoorMaterial.color = PSDoorMaterialOriginalColor;
            PSMetalDoorMaterial.color = PSMetalDoorMaterialOriginalColor;
            PSTechnicianNPCMaterial.color = PSTechnicianNPCMaterialOriginalColor;
        }
        

    }

    public void StopPlayerAndCameraMovement()
    {
        _StopPlayerMovement();
        _DisableCameraMovement();
        playerMovementControllerScript.enableZoom = false;
        mobileControllerPanel.SetActive(false);
    }

    public void ResumePlayerAndCameraMovement()
    {
        _ResumePlayerMovement();
        _EnableCameraMovement();
        playerMovementControllerScript.enableZoom = true;
        mobileControllerPanel.SetActive(true);

    }

    public void SetCollectedFlashlightTrue()
    {
        collectedFLashlight = true;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Main Menu Credits");
    }

    public void SetAnimationTrigger(Animator anim)
    {
        anim.SetTrigger("Trigger");
    }

    public void _StopPlayerMovement()
    {
        playerMovementControllerScript.playerCanMove = false;
        playerMovementControllerScript.enableHeadBob = false;
        playerMovementControllerScript.unlimitedSprint = true;
        mobileControllerPanel.SetActive(false);

        tempWalkSpeed = mobilePlayerMovementControllerScript.m_WalkSpeed;
        tempRunSpeed = mobilePlayerMovementControllerScript.m_RunSpeed;
        mobilePlayerMovementControllerScript.m_WalkSpeed = 0.0f;
        mobilePlayerMovementControllerScript.m_RunSpeed = 0.0f;
    }

    public void _ResumePlayerMovement()
    {
        playerMovementControllerScript.playerCanMove = true;
        playerMovementControllerScript.enableHeadBob = true;
        playerMovementControllerScript.unlimitedSprint = false;
        mobileControllerPanel.SetActive(true);

        mobilePlayerMovementControllerScript.m_WalkSpeed = tempWalkSpeed;
        mobilePlayerMovementControllerScript.m_RunSpeed = tempRunSpeed;
    }

    public void _DisableCameraMovement()
    {
        playerMovementControllerScript.cameraCanMove = false;
    }

    public void _EnableCameraMovement()
    {
        playerMovementControllerScript.cameraCanMove = true;

    }

    public void CheckEvidenceMinigameCounters()
    {
        if (fingerprintMinigameCounter >= fingerprintMinigameTargetCounter && dnaMinigameCounter == 1 && !labFirstReviewIsDone)
        {
            labsFirstDialogue.SetActive(true);
            computerCanvas.SetActive(false);
            labFirstReviewIsDone = true;

        }

        if (fingerprintMinigameCounter >= fingerprintMinigameTargetCounter && dnaMinigameCounter == dnaMinigameTargetCounter && labFirstReviewIsDone)
        {
            labsSecondDialogue.SetActive(true);
            computerCanvas.SetActive(false);

        }
    }

    public void IncreasePSSecurityCounter()
    {
        psSecurityCounter++;
        if (psSecurityCounter >= psSecurityTargetCounter)
            psSecuredDialogue.SetActive(true);
    }

    public void EnableDNAMatchButtons()
    {
        foreach (var button in DNAMatchButtons)
        {
            button.interactable = true;
        }
    }

    public void IncreaseFingerprintMinigameCounter()
    {
        fingerprintMinigameCounter++;
        CheckEvidenceMinigameCounters();
    }
    public void IncreaseDNAMinigameCounter()
    {
        dnaMinigameCounter++;
        CheckEvidenceMinigameCounters();
    }

    public void IncreaseFingerprintCounter()
    {
        fingerprintPlaceCounter++;
        if (fingerprintPlaceCounter >= fingerprintPlaceTargetCounter)
            fingerprintsCollectedDialogue.SetActive(true);
    }

    public void IncreaseCarFixCounter()
    {
        carFixedCounter++;
        if (carFixedCounter >= carFixedTargetCounter)
            carFixedDialogue.SetActive(true);
    }

    public void IncreaseCarItemCounter()
    {
        carItemCounter++;
        if (carItemCounter >= carItemTargetCounter)
            carItemsCollectedDialogue.SetActive(true);
    }

    public void IncreaseDNACollectCounter()
    {
        dnaCollectCounter++;
        if (dnaCollectCounter >= dnaCollectTargetCounter)
            dnaCollectDialogue.SetActive(true);
    }


    public void ToggleLightmaps()
    {
        if (lightmapsEnabled)
        {
            LightmapData[] turnedOffLightmaps = new LightmapData[lightmap_data.Length];

            for (int i = 0; i < turnedOffLightmaps.Length; i++)
            {
                var thisOriginalLightmap = lightmap_data[i];
                var thisTurnedOffLightmap = new LightmapData();

                thisTurnedOffLightmap.lightmapDir = thisOriginalLightmap.lightmapDir;
                thisTurnedOffLightmap.shadowMask = thisOriginalLightmap.shadowMask;
                thisTurnedOffLightmap.lightmapColor = Texture2D.blackTexture;

                turnedOffLightmaps[i] = thisTurnedOffLightmap;
            }

            //PSFloorMaterial.color = Color.black;
            LightmapSettings.lightmaps = turnedOffLightmaps;
            lightmapsEnabled = false;
        }
        else if (!lightmapsEnabled)
        {
            //PSFloorMaterial.color = PSFloorMaterialOriginalColor;
            //Reenable lightmap data in scene.
            LightmapSettings.lightmaps = lightmap_data;
            lightmapsEnabled = true;
        }
        ToggleMaterialColorsElectricity();
    }

    public void DisableLightmaps()
    {
        LightmapData[] turnedOffLightmaps = new LightmapData[lightmap_data.Length];

        for (int i = 0; i < turnedOffLightmaps.Length; i++)
        {
            var thisOriginalLightmap = lightmap_data[i];
            var thisTurnedOffLightmap = new LightmapData();

            thisTurnedOffLightmap.lightmapDir = thisOriginalLightmap.lightmapDir;
            thisTurnedOffLightmap.shadowMask = thisOriginalLightmap.shadowMask;
            thisTurnedOffLightmap.lightmapColor = Texture2D.blackTexture;

            turnedOffLightmaps[i] = thisTurnedOffLightmap;
        }

        LightmapSettings.lightmaps = turnedOffLightmaps;
    }

    public void TeleportThePlayer(Vector3 position)
    {
        StartCoroutine(WaitForSecondsTeleport(position, travelSeconds, travelMenuCanvas));
        travelFadePanel.SetActive(true);

    }

    public IEnumerator WaitForSecondsTeleport(Vector3 position, float secondsToWait, GameObject canvas)
    {
        yield return new WaitForSecondsRealtime(secondsToWait);
        canvas.SetActive(false);
        mobileCharacterController.gameObject.SetActive(false);
        playerGameObject.transform.position = position;
        mobileCharacterController.gameObject.SetActive(true);


    }
    public void TravelToCrimeScene()
    {
        TeleportThePlayer(crimeScenePosition.transform.position);
    }

    public void TravelToPoliceStation()
    {
        TeleportThePlayer(policeStationPosition.transform.position);
    }

    public void TravelToGraveyard()
    {
        TeleportThePlayer(graveyardPosition.transform.position);
    }

    public void WaitForSecondsMethod(float seconds, GameObject canvas)
    {
        //maybe enable fade in fade out effect
        StartCoroutine(WaitForSeconds(seconds, canvas));

    }

    public void WaitForSecondsMethod(float seconds)
    {
        StartCoroutine(WaitForSeconds(seconds));

    }

    public IEnumerator WaitForSeconds(float secondsToWait, GameObject canvas)
    {

        yield return new WaitForSecondsRealtime(secondsToWait);
        canvas.SetActive(false);

    }



    public IEnumerator WaitForSeconds(float secondsToWait)
    {

        yield return new WaitForSecondsRealtime(secondsToWait);

    }

    public void SetObjectPositionToPlayers(GameObject gameobject)
    {
        gameobject.transform.position = playerGameObject.transform.position;
    }

}

