using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class button : MonoBehaviour
{
    
    public GameObject kangarooImage;
    public GameObject whaleImage;
    public GameObject owlImage;
    public GameObject koalaImage;
    public GameObject foxImage;
    
    public GameObject preview_iamge;
    public Sprite picture_kan;

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
    /*   public void openWhale()
       {
           whaleImage.SetActive(true);
       }*/

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(clickKangaroo);
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    void clickKangaroo()
    {
        kangarooImage.SetActive(true);
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

        redOwlImage.SetActive(false);
        blueOwlImage.SetActive(false);
        greenOwlImage.SetActive(false);

        redKoalaImage.SetActive(false);
        blueKoalaImage.SetActive(false);
        greenKoalaImage.SetActive(false);

        redFoxImage.SetActive(false);
        blueFoxImage.SetActive(false);
        greenFoxImage.SetActive(false);
    }
/*
    void clickWhale()
    {
        kangarooImage.SetActive(false);
        whaleImage.SetActive(true);
        owlImage.SetActive(false);
        koalaImage.SetActive(false);
        foxImage.SetActive(false);
    }

    void clickOwl()
    {
        kangarooImage.SetActive(false);
        whaleImage.SetActive(false);
        owlImage.SetActive(true);
        koalaImage.SetActive(false);
        foxImage.SetActive(false);
    }

    void clickKoala()
    {
        kangarooImage.SetActive(false);
        whaleImage.SetActive(false);
        owlImage.SetActive(false);
        koalaImage.SetActive(true);
        foxImage.SetActive(false);
    }

    void clickFox()
    {
        kangarooImage.SetActive(false);
        whaleImage.SetActive(false);
        owlImage.SetActive(false);
        koalaImage.SetActive(false);
        foxImage.SetActive(true);
    }*/




 /*   public void ShowKangaroo()
    {
        preview_iamge.SetActive(true);
        picture_kan = Resources.Load("kangaroo", typeof(Sprite)) as Sprite;
        preview_iamge.GetComponent<Image>().sprite = picture_kan;
    }
    public void ShowWhale()
    {
        preview_iamge.SetActive(true);
        picture_kan = Resources.Load("whale", typeof(Sprite)) as Sprite;
        preview_iamge.GetComponent<Image>().sprite = picture_kan;
    }
    public void ShowOwl()
    {
        preview_iamge.SetActive(true);
        picture_kan = Resources.Load("owl", typeof(Sprite)) as Sprite;
        preview_iamge.GetComponent<Image>().sprite = picture_kan;
    }
    public void ShowKoala()
    {
        preview_iamge.SetActive(true);
        picture_kan = Resources.Load("koala", typeof(Sprite)) as Sprite;
        preview_iamge.GetComponent<Image>().sprite = picture_kan;
    }
    public void ShowFox()
    {
        preview_iamge.SetActive(true);
        picture_kan = Resources.Load("fox", typeof(Sprite)) as Sprite;
        preview_iamge.GetComponent<Image>().sprite = picture_kan;
    }*/

}
