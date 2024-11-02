using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // Singleton instance
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            Scene currentScene = SceneManager.GetActiveScene();

            if (_instance == null && !currentScene.name.Equals("Main Menu"))
            {
                _instance = FindObjectOfType<AudioManager>();
                if (_instance == null && !currentScene.name.Equals("Main Menu"))
                {
                    GameObject obj = new GameObject("AudioManager");
                    _instance = obj.AddComponent<AudioManager>();
                }
            }
            return _instance;
        }
    }

    // Audio clips and mixer groups
    [Header("UI Audio")]
    public AudioClip[] buttonClickSounds;

    [Header("Music And Ambient Audio")]
    public AudioClip[] musicClips;
    //public AudioClip[] ambientClips;

    [Header("Travel Menu Audio")]
    public AudioClip carDoorOpeningSound;
    public AudioClip carDoorClosingSound;

    [Header("Minigame Generic Audio")]
    public AudioClip genericMinigameWinSound;
    public AudioClip[] genericClickSounds;
    public AudioClip genericFailSound;

    [Header("Letter Puzzle Minigame Audio")]
    public AudioClip[] letterPaperSounds; //paper sound

    [Header("DNA Match, Fingerprint Collect/Match Minigame")]
    public AudioClip[] digitalClickSounds; //digital beep sound
    public AudioClip digitalBeepSound; //digital beep sound
    public AudioClip digitalGameWonSound; //digital success sound
    public AudioClip digitalFailSound; //digital success sound

    [Header("Dna collect Audio")]
    public AudioClip[] dnaCollectSounds;
    public AudioClip dnaCollectBoneSound;

    [Header("Dna Place Audio")]
    public AudioClip[] dnaPlaceClickSounds;

    [Header("Simon Says Minigame Audio")]
    public AudioClip[] simonSaysClickSounds; //digital beep sound
    public AudioClip simonSaysFailSound; //digital fail sound
    public AudioClip simonSaysWonSound; //digital success sound
    public AudioClip simonSaysGameWonSound; //digital fail sound

    [Header("Lockpicking Minigame Audio")]
    public AudioClip lockpickingSound; //lockpicking sound
    public AudioClip lockpickFailSound; //lockpick failed sound
    public AudioClip lockpickSuccessSound; //lock opening sound
    public AudioClip lockpickWonSound; //lock won sound

    [Header("Lock Breaking Minigame Audio")]
    public AudioClip[] lockbreakHitSounds; //metal hit sounds
    public AudioClip lockbreakFailSound; //lockpick failed sound
    public AudioClip[] lockbreakWonSounds;
    public AudioClip lockbreakWonChainSound;

    [Header("Electricity Fix Minigame Audio")]
    public AudioClip[] switchToggleSounds;
    public AudioClip powerEnabledSound;

    [Header("Tire Change Minigame Audio")]
    public AudioClip[] tireChangeClickSounds;

    [Header("Fill Fuel Minigame Audio")]
    public AudioClip[] fillFuelLiquidSounds;
    public AudioClip fuelCanDrag;


    //[Header("Walk Audio")]
    //public AudioClip[] WoodWalkStepClips;
    //public AudioClip[] OutsideWalkStepClips;
    //public AudioClip[] InsideWalkStepClips;
    //public AudioClip[] GrassWalkStepClips;

    [Header("Mixer Groups")]
    public AudioMixerGroup masterGroup;
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup soundEffectsGroup;
    public AudioMixerGroup ambientGroup;
    public AudioMixerGroup UIGroup;

    // Audio sources
    [Header("Audio Sources")]
    public AudioSource ambientSource;
    public AudioSource musicSource;
    public AudioSource stingSource;
    public AudioSource playerSource;
    public AudioSource minigameSource;
    public AudioSource UISource;

    [Header("Temp Sound Level Variables")]
    public float tempMasterVolume;
    public float tempMusicVolume;
    public float tempAmbientVolume;
    public float tempSFXVolume;
    public float tempUIVolume;

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


        StartLevelAudio();
    }

    // Start playing the ambient and music audio at the beginning of the level
    private void StartLevelAudio()
    {
        //ambientSource.clip = ambientClips[0];
        //ambientSource.loop = true;
        //ambientSource.Play();

        musicSource.clip = musicClips[0];
        musicSource.loop = true;
        musicSource.Play();
    }

    #region Generic Audio Methods

    public static void ChangeMusicClip(int index)
    {

        if (Instance.musicSource.clip != Instance.musicClips[index])
        {
            Instance.musicSource.Stop();
            Instance.musicSource.clip = Instance.musicClips[index];
            Instance.musicSource.Play();
        }
    }

    // Play a given audio clip using the minigame audio source
    public static void PlaySound(AudioClip clip)
    {
        if (clip != null)
            Instance.minigameSource.PlayOneShot(clip);
        else
        {
            print("Null audio clip");
        }
    }

    public static void PlayStingSound(AudioClip clip)
    {
        if (clip != null)
            Instance.stingSource.PlayOneShot(clip);
        else
        {
            print("Null audio clip");
        }
    }

    public static void SetMusicClip(int index)
    {
        if (Instance.musicSource.clip != Instance.musicClips[index])
        {
            Instance.musicSource.Stop();
            Instance.musicSource.clip = Instance.musicClips[index];
            Instance.musicSource.Play();
        }
        
    }

    public static void PlayRandomSound(AudioClip[] audioArray)
    {
        int index = Random.Range(0, audioArray.Length);
        Instance.minigameSource.PlayOneShot(audioArray[index]);
    }

    public static void PlayRandomSoundUI(AudioClip[] audioArray)
    {
        int index = Random.Range(0, audioArray.Length);
        Instance.UISource.PlayOneShot(audioArray[index]);
    }

    public void ButtonClick()
    {
        PlayRandomSoundUI(buttonClickSounds);
    }
    #endregion

    #region Audio Setup Methods

    public void SetSoundVolumes()
    {
        tempSFXVolume = Mathf.Log10(StaticVariables.Sound.effectVolume) * 20;
        tempMasterVolume = Mathf.Log10(StaticVariables.Sound.masterVolume) * 20;
        tempMusicVolume = Mathf.Log10(StaticVariables.Sound.musicVolume) * 20;
        tempAmbientVolume = Mathf.Log10(StaticVariables.Sound.ambientVolume) * 20;
        tempUIVolume = Mathf.Log10(StaticVariables.Sound.UIVolume) * 20;


        soundEffectsGroup.audioMixer.SetFloat("Sound Effects", tempSFXVolume);
        masterGroup.audioMixer.SetFloat("Master", tempMasterVolume);
        musicGroup.audioMixer.SetFloat("Music", tempMusicVolume);
        ambientGroup.audioMixer.SetFloat("Ambiance", tempAmbientVolume);
        ambientGroup.audioMixer.SetFloat("UI", tempUIVolume);



    }

    public void StopAudioOnPause()
    {
        soundEffectsGroup.audioMixer.SetFloat("Sound Effects", -80f);

    }

    //public void ResumeAudio()
    //{
    //    soundEffectsGroup.audioMixer.SetFloat("Sound Effects", tempSFXVolume);
    //    masterGroup.audioMixer.SetFloat("Master", tempMasterVolume);
    //    musicGroup.audioMixer.SetFloat("Music", tempMusicVolume);
    //    ambientGroup.audioMixer.SetFloat("Ambiance", tempAmbientVolume);
    //}


    #endregion



}



