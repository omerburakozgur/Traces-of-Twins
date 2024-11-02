using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [Header("Character Toggles")]
    public Toggle[] toggles;

    public Slider musicSlider;
    public Slider effectSlider;
    public TextMeshProUGUI musicSliderText;
    public TextMeshProUGUI effectSliderText;

    [Header("Scene Load References")]
    public GameObject characterPanel;
    public GameObject loadingSliderParent;
    public Slider slider;
    public TextMeshProUGUI progressText;
    public MenuSoundSystem menuSoundSystemReference;

    [Header("Main Menu Canvas References")]
    [SerializeField] GameObject MainMenuCanvas;
    [SerializeField] GameObject CharacterSelectCanvas;
    [SerializeField] GameObject OptionsCanvas;
    [SerializeField] GameObject CreditsCanvas;
    [SerializeField] GameObject HowToCanvas;

    [Header("Main Menu Fade Panel")]
    [SerializeField] GameObject FadePanel;

    public void LoadGame()
    {
        SetCharacterForPlaythrough();
        StartCoroutine(LoadGameSceneAsync());

    }

    IEnumerator LoadGameSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("final2");
        operation.allowSceneActivation = true;
        characterPanel.SetActive(false);
        loadingSliderParent.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progressText.text = (int)(progress * 100f) + "%";
            yield return null;

        }
    }

    public void MainMenuButton()
    {
        PlayClickSound();
        disableAll();
        FadePanel.SetActive(true);
        MainMenuCanvas.SetActive(true);

    }

    public void CharacterSelectMenu()
    {
        PlayClickSound();
        disableAll();
        FadePanel.SetActive(true);
        CharacterSelectCanvas.SetActive(true);
    }

    public void OptionsMenu()
    {
        PlayClickSound();
        disableAll();
        FadePanel.SetActive(true);
        OptionsCanvas.SetActive(true);
    }

    public void CreditsMenu()
    {
        PlayClickSound();
        disableAll();
        FadePanel.SetActive(true);
        CreditsCanvas.SetActive(true);
    }

    public void HowToMenu()
    {
        PlayClickSound();
        disableAll();
        FadePanel.SetActive(true);
        HowToCanvas.SetActive(true);
    }

    public void StartGameButton()
    {
        PlayClickSound();
        LoadGameSceneAsync();
        FadePanel.SetActive(true);
        SceneManager.LoadScene("final2");

    }

    public void Quit()
    {
        Application.Quit();

    }

    public void SetCharacterForPlaythrough()
    {
        if (toggles[0].isOn)
        {
            StaticVariables.Character.selectedCharacter = 0;
        }
        else if (toggles[1].isOn)
        {
            StaticVariables.Character.selectedCharacter = 1;

        }
    }

    public void SetCharacterIndex(int index)
    {
        PlayClickSound();
        StaticVariables.Character.selectedCharacter = index;

    }

    public void UpdateMusicVolume()
    {
        StaticVariables.Sound.musicVolume = musicSlider.value;
        musicSliderText.text = ((int)(musicSlider.value * 100)).ToString();
        menuSoundSystemReference.SetSoundVolumes();

    }

    public void UpdateEffectVolume()
    {
        StaticVariables.Sound.effectVolume = effectSlider.value;
        effectSliderText.text = ((int)(effectSlider.value * 100)).ToString();
        menuSoundSystemReference.SetSoundVolumes();


    }

    void SetSliderStartingValue()
    {
        effectSlider.value = StaticVariables.Sound.effectVolume;
        effectSliderText.text = ((int)(effectSlider.value * 100)).ToString();

        musicSlider.value = StaticVariables.Sound.musicVolume;
        musicSliderText.text = ((int)(musicSlider.value * 100)).ToString();
    }

    public void PlayClickSound()
    {
        menuSoundSystemReference.PlayRandomSound(menuSoundSystemReference.buttonClicks);

    }

    public void _PlayMouseEnter()
    {
        menuSoundSystemReference.PlayRandomSound(menuSoundSystemReference.buttonClicks);


    }

    public void _PlayMouseExit()
    {
        menuSoundSystemReference.PlayRandomSound(menuSoundSystemReference.buttonClicks);


    }

    private void disableAll()
    {
        CharacterSelectCanvas.SetActive(false);
        OptionsCanvas.SetActive(false);
        CreditsCanvas.SetActive(false);
        HowToCanvas.SetActive(false);
        MainMenuCanvas.SetActive(false);

    }




}
