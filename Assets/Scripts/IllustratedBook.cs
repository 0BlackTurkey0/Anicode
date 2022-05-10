using UnityEngine;
using UnityEngine.UI;

public class IllustratedBook : MonoBehaviour {
    [SerializeField] private GameObject preview_image;
    [SerializeField] private GameObject ani_detail;
    [SerializeField] private GameObject att_detail;
    [SerializeField] private GameObject ins_detail;
    [SerializeField] private GameObject intro_frame;
    [SerializeField] private GameObject Content;

    public void ChangeToAnimal() {
        ani_detail.SetActive(true);
        att_detail.SetActive(false);
        ins_detail.SetActive(false);
        Content.GetComponent<Text>().text = "";
        intro_frame.GetComponent<RectTransform>().localPosition = new Vector3(-250, -170, 0);
    }

    public void ChangeToAttribute() {
        ani_detail.SetActive(false);
        att_detail.SetActive(true);
        ins_detail.SetActive(false);
        Content.GetComponent<Text>().text = "";
        intro_frame.GetComponent<RectTransform>().localPosition = new Vector3(-250, -170, 0);
    }

    public void ChangeToInstruction() {
        ani_detail.SetActive(false);
        att_detail.SetActive(false);
        ins_detail.SetActive(true);
        Content.GetComponent<Text>().text = "";
        intro_frame.GetComponent<RectTransform>().localPosition = new Vector3(400, -60, 0);
    }

    public void ChangeToCodeChallenge() {
        ani_detail.SetActive(false);
        att_detail.SetActive(false);
        ins_detail.SetActive(false);
        Content.GetComponent<Text>().text = Resources.Load<TextAsset>("challenge_Text").ToString();
        intro_frame.GetComponent<RectTransform>().localPosition = new Vector3(-120, -100, 0);
    }

    public void ShowKanText() {
        Content.GetComponent<Text>().text = Resources.Load<TextAsset>("kangaroo_Text").ToString();
    }

    public void ShowWhaText() {
        Content.GetComponent<Text>().text = Resources.Load<TextAsset>("whale_Text").ToString();
    }

    public void ShowOwlText() {
        Content.GetComponent<Text>().text = Resources.Load<TextAsset>("owl_Text").ToString();
    }

    public void ShowKoaText() {
        Content.GetComponent<Text>().text = Resources.Load<TextAsset>("koala_Text").ToString();
    }

    public void ShowFoxText() {
        Content.GetComponent<Text>().text = Resources.Load<TextAsset>("fox_Text").ToString();
    }

    public void ShowPhyAtkText() {
        Content.GetComponent<Text>().text = Resources.Load<TextAsset>("phy_atk_Text").ToString();
    }

    public void ShowPhyDefText() {
        Content.GetComponent<Text>().text = Resources.Load<TextAsset>("phy_def_Text").ToString();
    }

    public void ShowSpeAtkText() {
        Content.GetComponent<Text>().text = Resources.Load<TextAsset>("spe_atk_Text").ToString();
    }

    public void ShowSpeDefText() {
        Content.GetComponent<Text>().text = Resources.Load<TextAsset>("spe_def_Text").ToString();
    }

    public void ShowSpeedText() {
        Content.GetComponent<Text>().text = Resources.Load<TextAsset>("speed_Text").ToString();
    }

    public void ShowIfText() {
        Content.GetComponent<Text>().text = Resources.Load<TextAsset>("if_Text").ToString();
    }

    public void ShowLoopText() {
        Content.GetComponent<Text>().text = Resources.Load<TextAsset>("loop_Text").ToString();
    }

    public void ShowMoveText() {
        Content.GetComponent<Text>().text = Resources.Load<TextAsset>("move_Text").ToString();
    }

    public void ShowAssignText() {
        Content.GetComponent<Text>().text = Resources.Load<TextAsset>("assign_Text").ToString();
    }

    public void ShowAttackText() {
        Content.GetComponent<Text>().text = Resources.Load<TextAsset>("attack_Text").ToString();
    }

    public void ShowSwapText() {
        Content.GetComponent<Text>().text = Resources.Load<TextAsset>("swap_Text").ToString();
    }

    public void ShowKangaroo() {
        preview_image.SetActive(true);
        preview_image.GetComponent<Image>().sprite = Resources.Load("kangaroo", typeof(Sprite)) as Sprite;
    }

    public void ShowWhale() {
        preview_image.SetActive(true);
        preview_image.GetComponent<Image>().sprite = Resources.Load("whale", typeof(Sprite)) as Sprite;
    }

    public void ShowOwl() {
        preview_image.SetActive(true);
        preview_image.GetComponent<Image>().sprite = Resources.Load("owl", typeof(Sprite)) as Sprite;
    }

    public void ShowKoala() {
        preview_image.SetActive(true);
        preview_image.GetComponent<Image>().sprite = Resources.Load("koala", typeof(Sprite)) as Sprite;
    }

    public void ShowFox() {
        preview_image.SetActive(true);
        preview_image.GetComponent<Image>().sprite = Resources.Load("fox", typeof(Sprite)) as Sprite;
    }
}