using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //public GameObject progress;
    public Slider slider;
    public Text progressText;
    private int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timer());
    }

    IEnumerator timer()
    {
        while (i < 99) {
            i += 1;
            slider.value = i;
            progressText.text = i.ToString() + " %";
            yield return null;
        }
    }
}
