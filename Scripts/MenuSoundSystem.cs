using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSoundSystem : MonoBehaviour
{
    // Audio clips and mixer groups
    [Header("Main Menu Audio Sources")]
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource videoSource;


    [Header("Main Menu Music Audio")]
    public AudioClip mainMenuMusic;

    [Header("Main Menu Button Click Sounds")]
    public AudioClip[] buttonClicks;
    public AudioClip gameStart;

    private void Start()
    {
        StartMainMenuMusic();

    }

    public void FixedUpdate()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name.Equals("final 2"))
        {
            Destroy(gameObject);
        }
    }

    // Start playing the ambient and music audio at the beginning of the level
    private void StartMainMenuMusic()
    {
        musicSource.clip = mainMenuMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySound(AudioClip audio)
    {
        soundSource.PlayOneShot(audio);
    }

    public void PlayRandomSound(AudioClip[] audioArray)
    {
        //Pick a random paper sound
        int index = Random.Range(0, audioArray.Length);
        //Play the randomly selected audio clip
        soundSource.PlayOneShot(audioArray[index]);
    }

    public void SetSoundVolumes()
    {
        
        musicSource.volume = StaticVariables.Sound.musicVolume;
        soundSource.volume = StaticVariables.Sound.effectVolume;
        videoSource.volume = StaticVariables.Sound.effectVolume;


    }
}
