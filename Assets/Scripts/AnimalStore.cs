using UnityEngine;
using UnityEngine.UI;

public class AnimalStore : MonoBehaviour {
    [SerializeField] GameObject ConfirmSurface;
    [SerializeField] GameObject FailSurface;
    [SerializeField] GameObject Content;

    private GameObject Chosen;
    private int Money;
    private int Price = 100;
    private int index;
    private ApplicationHandler applicationHandler;

    void Awake() {
        applicationHandler = GameObject.Find("ApplicationHandler").GetComponent<ApplicationHandler>();
    }

    void Start() {
        Money = applicationHandler.GameData.Money;
        for (int i = 0;i < 34;i++) {
            if (applicationHandler.GameData.Items[i]) {
                Chosen = Content.transform.GetChild(i).gameObject;
                if (Chosen.TryGetComponent<Button>(out Button button)) {
                    button.enabled = false;
                    Chosen.transform.GetChild(0).gameObject.SetActive(false);
                    Chosen.transform.GetChild(1).gameObject.SetActive(false);
                    Chosen.transform.GetChild(2).gameObject.SetActive(true);
                }
            }
        }
    }

    public void Btn_OnClick(int index_tmp) {
        index = index_tmp;
        Chosen = Content.transform.GetChild(index).gameObject;
        ConfirmSurface.SetActive(true);
    }

    public void ConfirmBtn_OnClick() {
        if (Money > Price) {
            ConfirmSurface.SetActive(false);
            Chosen.GetComponent<Button>().enabled = false;
            Chosen.transform.GetChild(0).gameObject.SetActive(false);
            Chosen.transform.GetChild(1).gameObject.SetActive(false);
            Chosen.transform.GetChild(2).gameObject.SetActive(true);
            applicationHandler.GameData.Money -= Price;
            applicationHandler.GameData.Items[index] = true;
            applicationHandler.GameData.SaveData();
        }
        else {
            FailSurface.SetActive(true);
            ConfirmSurface.SetActive(false);
        }
    }
}