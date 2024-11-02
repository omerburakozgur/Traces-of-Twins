using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class UIManager : MonoBehaviour
{

    [Header("UI Panels References")]
    public GameObject inventoryPanel;
    public GameObject mapPanel;
    public GameObject fadePanel;
    public GameObject pauseMenuPanel;
    public GameObject howToPanel;
    public GameObject optionsPanel;
    public GameObject pauseButtonsPanel;


    [Header("UI Element References")]
    public GameObject inventoryButton;
    public GameObject mapButton;
    public GameObject lightmapToggleButton;

    [Header("Travel Menu References")]
    public Button graveyardTravelButton;
    public GameObject graveyardButtonHighlightSprite;
    public Button PSTravelButton;
    public GameObject PSButtonHighlightSprite;
    public Button houseTravelButton;
    public GameObject houseButtonHighlightSprite;

    [Header("Sound Slider References")]
    public Slider musicSlider;
    public Slider effectSlider;
    public Slider ambientSlider;
    public Slider masterSlider;
    public Slider UISlider;

    public TextMeshProUGUI musicSliderText;
    public TextMeshProUGUI effectSliderText;
    public TextMeshProUGUI masterSliderText;
    public TextMeshProUGUI ambientSliderText;
    public TextMeshProUGUI UISliderText;

    [Header("Quest System References")]
    [SerializeField] private TextMeshProUGUI primaryQuestObjective;
    [SerializeField] private TextMeshProUGUI secondaryQuestObjective;

    [Header("Flashlight References")]
    public FlashlightManager flashlightManagerReference;
    public FlashlightManager flashlightManagerReferenceMobile;

    public Slider flashlightBatterySlider;

    [Header("Inventory Camera Reference")]
    [SerializeField] private GameObject inventoryTrunkCamera;
    #region UI Manager Singleton Instance

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("UIManager");
                    _instance = obj.AddComponent<UIManager>();
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

    #endregion

    #region Usable Items
    public void UseFlashlightKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            UseFlashlight();

        }
    }

    public void UseFlashlight()
    {
        if (GameManager.Instance.collectedFLashlight)
        {
            if (flashlightManagerReference.isActiveAndEnabled)
                flashlightManagerReference.ToggleFlashlight();
            else
                flashlightManagerReferenceMobile.ToggleFlashlight();
        }


    }

    #endregion

    #region Quest System Functions

    public void _UpdatePrimaryQuestObjective(string objective)
    {
        primaryQuestObjective.text = objective;
    }

    public void _UpdateSecondaryQuestObjective(string objective)
    {
        secondaryQuestObjective.text = objective;
    }

    #endregion

    #region Pause and Menu Navigation

    private void Start()
    {
        SetSliderStartingValue();
    }

    private void Update()
    {
        UseFlashlightKeyboard();
    }

    public void OnPauseGame(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (GameManager.Instance.GameIsPaused)
            {
                AudioManager.Instance.ButtonClick(); // resume sound
                Resume();

            }
            else
            {
                AudioManager.Instance.ButtonClick(); // pause sound
                Pause();

            }
        }
    }
    public void OnPauseGameButton()
    {
        if (GameManager.Instance.GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void EnableTimeScale()
    {
        GameManager.Instance.GameIsPaused = false;
        Time.timeScale = 1f;
        AudioManager.Instance.SetSoundVolumes();

    }

    public void Resume()
    {
        pauseMenuPanel.SetActive(false);
        howToPanel.SetActive(false);
        optionsPanel.SetActive(false);
        pauseButtonsPanel.SetActive(true);
        Time.timeScale = 1f;
        GameManager.Instance.GameIsPaused = false;
        AudioManager.Instance.SetSoundVolumes();
        GameManager.Instance._EnableCameraMovement();

    }

    void Pause()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        GameManager.Instance.GameIsPaused = true;
        AudioManager.Instance.StopAudioOnPause();
        GameManager.Instance._DisableCameraMovement();


    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayFadeEffect()
    {
        fadePanel.SetActive(true);
    }
    #endregion

    #region Audio Functions

    public void UpdateMusicVolume()
    {
        StaticVariables.Sound.musicVolume = musicSlider.value;
        musicSliderText.text = ((int)(musicSlider.value * 100)).ToString();
        AudioManager.Instance.SetSoundVolumes();

    }

    public void UpdateEffectVolume()
    {
        StaticVariables.Sound.effectVolume = effectSlider.value;
        effectSliderText.text = ((int)(effectSlider.value * 100)).ToString();
        AudioManager.Instance.SetSoundVolumes();
    }

    public void UpdateMasterVolume()
    {
        StaticVariables.Sound.masterVolume = masterSlider.value;
        masterSliderText.text = ((int)(masterSlider.value * 100)).ToString();
        AudioManager.Instance.SetSoundVolumes();
    }

    public void UpdateAmbientVolume()
    {
        StaticVariables.Sound.ambientVolume = ambientSlider.value;
        ambientSliderText.text = ((int)(ambientSlider.value * 100)).ToString();
        AudioManager.Instance.SetSoundVolumes();
    }

    public void UpdateUIVolume()
    {
        StaticVariables.Sound.UIVolume = UISlider.value;
        UISliderText.text = ((int)(UISlider.value * 100)).ToString();
        AudioManager.Instance.SetSoundVolumes();
    }


    void SetSliderStartingValue()
    {

        effectSlider.value = StaticVariables.Sound.effectVolume;
        effectSliderText.text = ((int)(effectSlider.value * 100)).ToString();

        musicSlider.value = StaticVariables.Sound.musicVolume;
        musicSliderText.text = ((int)(musicSlider.value * 100)).ToString();

        masterSlider.value = StaticVariables.Sound.masterVolume;
        masterSliderText.text = ((int)(masterSlider.value * 100)).ToString();

        ambientSlider.value = StaticVariables.Sound.ambientVolume;
        ambientSliderText.text = ((int)(ambientSlider.value * 100)).ToString();

        UISlider.value = StaticVariables.Sound.UIVolume;
        UISliderText.text = ((int)(UISlider.value * 100)).ToString();

    }
    #endregion

    #region Inventory and Map

    public void InventoryButton()
    {
        if (inventoryPanel.active == false)
        {
            inventoryPanel.active = true;
            inventoryTrunkCamera.SetActive(true);

        }
        else
        {
            inventoryPanel.active = false;
            inventoryTrunkCamera.SetActive(false);

        }
    }

    public void InventoryButtonKeyboard(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            InventoryButton();

        }

    }

    public void MapButton()
    {
        if (!mapPanel.active)
            mapPanel.SetActive(true);
        else
            mapPanel.SetActive(false);
    }

    public void CheckInteractableForHover(Button button, GameObject sprite)
    {
        if (button.interactable)
        {
            sprite.SetActive(true);
        }
    }

    public void CheckGraveyardTravelButton()
    {
        CheckInteractableForHover(graveyardTravelButton, graveyardButtonHighlightSprite);
    }

    public void CheckPSTravelButton()
    {
        CheckInteractableForHover(PSTravelButton, PSButtonHighlightSprite);
    }

    public void CheckHouseTravelButton()
    {
        CheckInteractableForHover(houseTravelButton, houseButtonHighlightSprite);
    }

    #endregion

    #region GameManager Function References

    public void ToggleLightmaps()
    {
        // Debug purposes only
        GameManager.Instance.ToggleLightmaps();
    }

    #endregion
}
