using UnityEngine;
using UnityEngine.UI;

public class Setting_UI_Control : MonoBehaviour {
    public GameObject full_screen;
    public GameObject windows;
    public Slider slider;

    private ApplicationHandler applicationHandler;

    void Awake()
    {
        applicationHandler = GameObject.Find("ApplicationHandler").GetComponent<ApplicationHandler>();
    }
    // Start is called before the first frame update
    void Start()
    {
        slider.value = applicationHandler.GameData.VoiceVolume;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToFullScreen()
    {
        applicationHandler.GameData.IsFullScreen = true;
        applicationHandler.GameData.SaveData();
        Screen.fullScreen = true;
    }

    public void ToWindows()
    {
        applicationHandler.GameData.IsFullScreen = false;
        applicationHandler.GameData.SaveData();
        Screen.fullScreen = false;
    }
    public void Volume()
    {
        applicationHandler.GameData.VoiceVolume = slider.value;
        applicationHandler.GameData.SaveData();
        applicationHandler.transform.GetChild(2).GetComponent<AudioSource>().volume = slider.value;
    }

    public void ToReset()
    {
        applicationHandler.GameData.VoiceVolume = 0.5f;
        applicationHandler.GameData.IsFullScreen = true;
        applicationHandler.GameData.SaveData();
        slider.value = 0.5f;
        Screen.fullScreen = true;
    }
}
