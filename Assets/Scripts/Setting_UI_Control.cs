using UnityEngine;
using UnityEngine.UI;

public class Setting_UI_Control : MonoBehaviour {
    public GameObject full_screen;
    public GameObject windows;
    public Slider slider;
    public AudioSource BGM;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToFullScreen()
    {
        Screen.fullScreen = true;
    }

    public void ToWindows()
    {
        Screen.fullScreen = false;
    }
    public void Volume()
    {
        BGM.volume = slider.value;
    }

    public void ToRest()
    {
        slider.value = (float)0.25;
        BGM.volume = slider.value;
        Screen.fullScreen = true;
    }
}
