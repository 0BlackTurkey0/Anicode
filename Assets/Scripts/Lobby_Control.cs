using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby_Control : MonoBehaviour {
    [SerializeField] GameObject ReviseNameWindow;
    [SerializeField] GameObject SingleMotionBtn;
    [SerializeField] GameObject DoubleMotionBtn;
    [SerializeField] GameObject StoreBtn;
    [SerializeField] GameObject ReviseNameBtn;
    [SerializeField] GameObject BookBtn;
    [SerializeField] GameObject AchievementBtn;
    [SerializeField] GameObject SettingBtn;
    [SerializeField] GameObject WarningOnPlayerNameWindow;
    [SerializeField] GameObject PlayerName;
    [SerializeField] Text MoneyText;
    [SerializeField] Text LevelText;
    [SerializeField] Text RevisePlayerName;

    private ApplicationHandler applicationHandler;

    void Awake() {
        applicationHandler = GameObject.Find("ApplicationHandler").GetComponent<ApplicationHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerName.GetComponent<Text>().text = applicationHandler.GameData.Name;
        LevelText.text = "階級 : ";
        switch (applicationHandler.GameData.Rank) {
            case DifficultyType.NULL:
                LevelText.text += "無";
                break;
            case DifficultyType.Start:
                LevelText.text += "入門";
                break;
            case DifficultyType.Easy:
                LevelText.text += "簡單";
                break;
            case DifficultyType.Normal:
                LevelText.text += "普通";
                break;
            case DifficultyType.Hard:
                LevelText.text += "困難";
                break;
        }
        
        MoneyText.text = applicationHandler.GameData.Money.ToString();
        ReviseNameWindow.SetActive(false);
        WarningOnPlayerNameWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SingleMotionOnclick()
    {
        SceneManager.LoadScene("SingleMode");
    }

    public void DoubleMotionOnClick()
    {
        SceneManager.LoadScene("DuelMode");
    }

    public void StoreOnClick()
    {
        SceneManager.LoadScene("AnimalStore");
    }

    public void AchievementOnclick()
    {
        SceneManager.LoadScene("Achievement");
    }

    public void BookOnClick()
    {
        SceneManager.LoadScene("IllustratedBook");
    }

    public void SettingOnClick()
    {
        SceneManager.LoadScene("Setting");
    }

    public void ReviseNameOnClick()
    {
        ReviseNameWindow.SetActive(true);
        ReviseNameWindow.transform.GetChild(2).GetComponent<InputField>().text = applicationHandler.GameData.Name;
    }

    public void ReviseNameConfirm()
    {
        if (RevisePlayerName.text.Length > 8 || RevisePlayerName.text.Length == 0) {
            WarningOnPlayerNameWindow.SetActive(true);
        }
        else {
            applicationHandler.GameData.Name = RevisePlayerName.text;
            applicationHandler.GameData.SaveData();
            PlayerName.GetComponent<Text>().text = RevisePlayerName.text;
            RevisePlayerName.GetComponent<Text>().text = PlayerName.GetComponent<Text>().text;
            RevisePlayerName.text.Remove(0, PlayerName.GetComponent<Text>().text.Length);
            ReviseNameWindow.SetActive(false);
            SingleMotionBtn.SetActive(true);
            DoubleMotionBtn.SetActive(true);
            StoreBtn.SetActive(true);
            ReviseNameBtn.SetActive(true);
            BookBtn.SetActive(true);
            AchievementBtn.SetActive(true);
            SettingBtn.SetActive(true);
        }
    }

    public void ReviseNameCancel()
    {
        RevisePlayerName.text = "";
        ReviseNameWindow.SetActive(false);
        SingleMotionBtn.SetActive(true);
        DoubleMotionBtn.SetActive(true);
        StoreBtn.SetActive(true);
        ReviseNameBtn.SetActive(true);
        BookBtn.SetActive(true);
        AchievementBtn.SetActive(true);
        SettingBtn.SetActive(true);
    }
    public void WarningOnPlayerNameRealizeOnClick()
    {
        WarningOnPlayerNameWindow.SetActive(false);
        RevisePlayerName.text = "";
    }
    public void ExceptForLobbyReturnOnClick()
    {
        SceneManager.LoadScene("Lobby");
    }
}
