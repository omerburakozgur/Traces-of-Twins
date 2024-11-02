using UnityEngine;

public class FlashlightManager : MonoBehaviour
{
    [SerializeField] float flashlightBatteryCapacity = 1f;
    [SerializeField] float flashlightCurrentBattery = 1f;
    bool isFlashlightOn = false;
    [SerializeField] GameObject flashlightObject;
    [SerializeField] GameObject flashlightSliderSprite;


    private void Start()
    {
        flashlightObject.SetActive(false);

    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        ManageFlashlight();

    }

    void ManageFlashlight()
    {
        if (isFlashlightOn && flashlightCurrentBattery > 0f)
        {
            flashlightCurrentBattery -= Time.deltaTime / 2;

            flashlightSliderSprite.SetActive(true);

            if (flashlightCurrentBattery <= 0f)
                DisableFlashlight();

        }
        else if (!isFlashlightOn)
        {
            if (flashlightCurrentBattery < flashlightBatteryCapacity)
                flashlightCurrentBattery += 2 * Time.deltaTime;
        }

        if (!isFlashlightOn && flashlightCurrentBattery >= flashlightBatteryCapacity)
        {
            flashlightSliderSprite.SetActive(false);
        }
        UIManager.Instance.flashlightBatterySlider.value = flashlightCurrentBattery / 10;
    }

    public void ToggleFlashlight()
    {

        isFlashlightOn = !isFlashlightOn;
        flashlightObject.SetActive(isFlashlightOn);
        print("click");
        AudioManager.PlayRandomSound(AudioManager.Instance.genericClickSounds);
    }

    void EnableFlashlight()
    {
        isFlashlightOn = true;
        flashlightObject.SetActive(true);

    }

    void DisableFlashlight()
    {
        isFlashlightOn = false;
        flashlightObject.SetActive(false);
        AudioManager.PlayRandomSound(AudioManager.Instance.genericClickSounds);

    }

}
