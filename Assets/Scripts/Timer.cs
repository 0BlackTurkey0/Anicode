using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    [SerializeField] Slider slider;
    [SerializeField] Text progressText;
    private int i = 0;

    void Start() {
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer() {
        while (i < 100) {
            i += 1;
            slider.value = i;
            progressText.text = i.ToString() + " %";
            yield return null;
        }
    }
}