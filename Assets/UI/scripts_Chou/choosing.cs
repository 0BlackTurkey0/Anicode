using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class choosing : MonoBehaviour
{
    GameObject redKangaroo;
    GameObject redWhale;
    GameObject redOwl;
    GameObject redKoala;
    GameObject redFox;
    GameObject blueKangaroo;
    GameObject blueWhale;
    GameObject blueOwl;
    GameObject blueKoala;
    GameObject blueFox;
    GameObject greenKangaroo;
    GameObject greenWhale;
    GameObject greenOwl;
    GameObject greenKoala;
    GameObject greenFox;

    Button brk;
    Button brw;
    Button bro;
    Button brko;
    Button brf;
    Button bbk;
    Button bbw;
    Button bbo;
    Button bbko;
    Button bbf;
    Button bgk;
    Button bgw;
    Button bgo;
    Button bgko;
    Button bgf;
    // Start is called before the first frame update
    void Start()
    {
        redKangaroo = GameObject.Find("red-previex-button");
        redWhale = GameObject.Find("red-previex-button (1)");
        redOwl = GameObject.Find("red-previex-button (2)");
        redKoala = GameObject.Find("red-previex-button (3)");
        redFox = GameObject.Find("red-previex-button (4)");

        blueKangaroo = GameObject.Find("blue-previex-button");
        blueWhale = GameObject.Find("blue-previex-button (1)");
        blueOwl = GameObject.Find("blue-previex-button (2)");
        blueKoala = GameObject.Find("blue-previex-button (3)");
        blueFox = GameObject.Find("blue-previex-button (4)");

        greenKangaroo = GameObject.Find("green-previex-button");
        greenWhale = GameObject.Find("green-previex-button (1)");
        greenOwl = GameObject.Find("green-previex-button (2)");
        greenKoala = GameObject.Find("green-previex-button (3)");
        greenFox = GameObject.Find("green-previex-button (4)");

        brk = redKangaroo.GetComponent<Button>();
        brw = redWhale.GetComponent<Button>();
        bro = redOwl.GetComponent<Button>();
        brko = redKoala.GetComponent<Button>();
        brf = redFox.GetComponent<Button>();

        bbk = blueKangaroo.GetComponent<Button>();
        bbw = blueWhale.GetComponent<Button>();
        bbo = blueOwl.GetComponent<Button>();
        bbko = blueKoala.GetComponent<Button>();
        bbf = blueFox.GetComponent<Button>();

        bgk = greenKangaroo.GetComponent<Button>();
        bgw = greenWhale.GetComponent<Button>();
        bgo = greenOwl.GetComponent<Button>();
        bgko = greenKoala.GetComponent<Button>();
        bgf = greenFox.GetComponent<Button>();

     /*   brk.onClick.AddListener(clickRedKangaroo);
        brw.onClick.AddListener(clickRedWhale);
        bro.onClick.AddListener(clickRedOwl);
        brko.onClick.AddListener(clickRedKoala);
        brf.onClick.AddListener(clickRedFox);

        bbk.onClick.AddListener(clickblueKangaroo);
        bbw.onClick.AddListener(clickblueWhale);
        bbo.onClick.AddListener(clickblueOwl);
        bbko.onClick.AddListener(clickblueKoala);
        bbf.onClick.AddListener(clickblueFox);

        bgk.onClick.AddListener(clickgreenKangaroo);
        bgw.onClick.AddListener(clickgreenWhale);
        bgo.onClick.AddListener(clickgreenOwl);
        bgko.onClick.AddListener(clickgreenKoala);
        bgf.onClick.AddListener(clickgreenFox);*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void choosebuy()
    {

    }
}
