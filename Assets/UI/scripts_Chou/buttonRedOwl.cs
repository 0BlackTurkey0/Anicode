using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonRedOwl : MonoBehaviour
{
    public GameObject kangarooImage;
    public GameObject whaleImage;
    public GameObject owlImage;
    public GameObject koalaImage;
    public GameObject foxImage;

    public GameObject redKangarooImage;
    public GameObject blueKangarooImage;
    public GameObject greenKangarooImage;

    public GameObject redWhaleImage;
    public GameObject blueWhaleImage;
    public GameObject greenWhaleImage;

    public GameObject redOwlImage;
    public GameObject blueOwlImage;
    public GameObject greenOwlImage;

    public GameObject redKoalaImage;
    public GameObject blueKoalaImage;
    public GameObject greenKoalaImage;

    public GameObject redFoxImage;
    public GameObject blueFoxImage;
    public GameObject greenFoxImage;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(clickRedOwl);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void clickRedOwl()
    {
        kangarooImage.SetActive(false);
        whaleImage.SetActive(false);
        owlImage.SetActive(false);
        koalaImage.SetActive(false);
        foxImage.SetActive(false);

        redKangarooImage.SetActive(false);
        blueKangarooImage.SetActive(false);
        greenKangarooImage.SetActive(false);

        redWhaleImage.SetActive(false);
        blueWhaleImage.SetActive(false);
        greenWhaleImage.SetActive(false);

        redOwlImage.SetActive(true);
        blueOwlImage.SetActive(false);
        greenOwlImage.SetActive(false);

        redKoalaImage.SetActive(false);
        blueKoalaImage.SetActive(false);
        greenKoalaImage.SetActive(false);

        redFoxImage.SetActive(false);
        blueFoxImage.SetActive(false);
        greenFoxImage.SetActive(false);
    }
}
