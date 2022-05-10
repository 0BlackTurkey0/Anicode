using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimpleStory : MonoBehaviour {
    private GameObject Prefab;
    private int ind = 0, storyNum, pressStatus;
    private bool isFinish, isRun, isSelect; // isFinal;
    private GameObject buttonParent;
    private ApplicationHandler applicationHandler;

    void Awake() {
        applicationHandler = GameObject.Find("ApplicationHandler").GetComponent<ApplicationHandler>();
    }

    void Start() {
        applicationHandler.IsSimple = true;

        storyNum = applicationHandler.GameData.Schedule_Simple;
        ind = applicationHandler.GameData.Schedule_SimpleChange;
        if (ind == 0) { //一個story結束後下一次載入select場景
            if (applicationHandler.GameData.SimpleIsFinish == true) //已經玩過一輪
                Prefab = Instantiate(Resources.Load<GameObject>("select_3"), gameObject.transform);
            else
                Prefab = Instantiate(Resources.Load<GameObject>("select_" + storyNum.ToString()), gameObject.transform);
            isSelect = true;
        }
        else { //只結束小關卡
            string tag = Resources.Load<GameObject>(storyNum.ToString() + "-" + ind.ToString()).tag;
            if (tag == "1" || tag == "4") {
                Prefab = Instantiate(Resources.Load<GameObject>(storyNum.ToString() + "-" + ind.ToString()), gameObject.transform);
                isRun = false;
                isFinish = true;
            }
            else if (tag == "2") {
                if (applicationHandler.GameData.IswinForSimple) { //打贏AI
                    ind++;
                    Prefab = Instantiate(Resources.Load<GameObject>(storyNum.ToString() + "-" + ind.ToString()), gameObject.transform);
                    isRun = false;
                    isFinish = true;
                }
                else { //打輸AI
                    Debug.Log("001");
                    ind++;
                    Prefab = Instantiate(Resources.Load<GameObject>(storyNum.ToString() + "-" + ind.ToString() + "-2"), gameObject.transform);
                    isRun = false;
                    isFinish = true;
                }
            }
        }
    }

    void Update() {
        if (Prefab != null) {
            if (pressStatus == 0 && Keyboard.current.rightArrowKey.isPressed) {
                pressStatus = 1;
                StartCoroutine(PressWait());
            }
            switch (Prefab.tag) {
                case "1":   //有對話
                    isFinish = Prefab.GetComponent<SimpleStory_DialogSystem>().Finished;
                    if (applicationHandler.GameData.Schedule_SimpleChange != ind) {
                        applicationHandler.GameData.Schedule_SimpleChange = ind;
                        applicationHandler.GameData.SaveData();
                    }
                    if (pressStatus == 1 && isFinish) {
                        pressStatus = 2;
                        Destroy(Prefab);
                        ind++;
                        Prefab = Instantiate(Resources.Load<GameObject>(storyNum.ToString() + "-" + ind.ToString()), gameObject.transform);
                    }
                    break;
                case "2":   //進入關卡前畫面
                    if (!isRun && isFinish) {
                        StartCoroutine(UpdateIntoGame());
                        isRun = true;
                    }
                    break;
                case "3":   //關卡結束時畫面
                    if (!isRun && isFinish) {
                        StartCoroutine(UpdateExitGame());
                        isRun = true;
                    }
                    break;
                case "4":   //結束故事返回選擇畫面
                    if (applicationHandler.GameData.Schedule_SimpleChange != ind) {
                        applicationHandler.GameData.Schedule_SimpleChange = ind;
                        applicationHandler.GameData.SaveData();
                    }
                    if (pressStatus == 1 && isFinish) {
                        pressStatus = 2;
                        if (storyNum < 4)
                            storyNum++;
                        if (storyNum == 4)
                            applicationHandler.GameData.SimpleIsFinish = true;
                        if (applicationHandler.GameData.Schedule_Simple < storyNum && storyNum < 5)
                            applicationHandler.GameData.Schedule_Simple = storyNum;
                        ind = 0;
                        applicationHandler.GameData.Schedule_SimpleChange = ind;
                        applicationHandler.GameData.SaveData();
                        Destroy(Prefab);
                        if (applicationHandler.GameData.SimpleIsFinish == true) //已經玩過一輪
                            Prefab = Instantiate(Resources.Load<GameObject>("select_3"), gameObject.transform);
                        else
                            Prefab = Instantiate(Resources.Load<GameObject>("select_" + storyNum.ToString()), gameObject.transform);
                        isSelect = true;
                    }
                    break;
                case "5":
                    if (isSelect) {
                        isSelect = false;
                        buttonParent = gameObject.transform.GetChild(0).gameObject;
                        for (int i = 1;i <= storyNum;i += 1)
                            buttonParent.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(delegate () { StoryOnClick(); });
                    }
                    break;
            }
        }
    }

    private void StoryOnClick() {
        ind = 1;
        var button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if (button.name == "story1")
            storyNum = 1;
        else if (button.name == "story2")
            storyNum = 2;
        else if (button.name == "story3")
            storyNum = 3;
        applicationHandler.GameData.Schedule_Simple = storyNum;
        applicationHandler.GameData.Schedule_SimpleChange = ind;
        applicationHandler.GameData.SaveData();
        Destroy(Prefab);
        Prefab = Instantiate(Resources.Load<GameObject>(storyNum.ToString() + "-1"), gameObject.transform);
        isFinish = false;
    }

    private IEnumerator UpdateIntoGame()    // 進入AI
    {
        applicationHandler.GameData.Schedule_SimpleChange = ind;
        applicationHandler.GameData.IswinForSimple = false;
        applicationHandler.GameData.SaveData();
        applicationHandler.IsDuel = false;
        ConnectAI();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Battle");
    }

    private IEnumerator UpdateExitGame() {
        yield return new WaitForSeconds(2);
        Destroy(Prefab);
        if (applicationHandler.GameData.IswinForSimple) //成功
            ind++;
        else  //失敗
            ind--;
        Prefab = Instantiate(Resources.Load<GameObject>(storyNum.ToString() + "-" + ind.ToString()), gameObject.transform);
        applicationHandler.GameData.Schedule_SimpleChange = ind;
        applicationHandler.GameData.SaveData();
        isRun = false;
    }

    private IEnumerator PressWait() {
        yield return new WaitForSeconds(1);
        pressStatus = 0;
    }

    public void OnClick() {
        SceneManager.LoadScene("Lobby");
    }

    private void ConnectAI() {
        if (storyNum == 1 && ind == 2) {
            applicationHandler.CharaType[0] = CharacterType.Fox;
            applicationHandler.CharaType[1] = CharacterType.Customized;
            applicationHandler.DiffiType = DifficultyType.Start;
        }
        else if (storyNum == 1 && ind == 6) {
            applicationHandler.CharaType[0] = CharacterType.Fox;
            applicationHandler.CharaType[1] = CharacterType.Customized;
            applicationHandler.DiffiType = DifficultyType.Start;
        }
        else if (storyNum == 1 && ind == 9) {
            applicationHandler.CharaType[0] = CharacterType.Fox;
            applicationHandler.CharaType[1] = CharacterType.Customized;
            applicationHandler.DiffiType = DifficultyType.Start;
        }
        else if (storyNum == 1 && ind == 12) {
            applicationHandler.CharaType[0] = CharacterType.Fox;
            applicationHandler.CharaType[1] = CharacterType.Customized;
            applicationHandler.DiffiType = DifficultyType.Start;
        }
        else if (storyNum == 1 && ind == 16) {
            applicationHandler.CharaType[0] = CharacterType.Fox;
            applicationHandler.CharaType[1] = CharacterType.Customized;
            applicationHandler.DiffiType = DifficultyType.Start;
        }
        else if (storyNum == 2 && ind == 3) {
            applicationHandler.CharaType[0] = CharacterType.Kangaroo;
            applicationHandler.CharaType[1] = CharacterType.Customized;
            applicationHandler.DiffiType = DifficultyType.Easy;
        }
        else if (storyNum == 2 && ind == 8) {
            applicationHandler.CharaType[0] = CharacterType.Kangaroo;
            applicationHandler.CharaType[1] = CharacterType.Customized;
            applicationHandler.DiffiType = DifficultyType.Easy;
        }
        else if (storyNum == 2 && ind == 11) {
            applicationHandler.CharaType[0] = CharacterType.Kangaroo;
            applicationHandler.CharaType[1] = CharacterType.Customized;
            applicationHandler.DiffiType = DifficultyType.Easy;
        }
        else if (storyNum == 2 && ind == 15) {
            applicationHandler.CharaType[0] = CharacterType.Kangaroo;
            applicationHandler.CharaType[1] = CharacterType.Customized;
            applicationHandler.DiffiType = DifficultyType.Easy;
        }
        else if (storyNum == 2 && ind == 19) {
            applicationHandler.CharaType[0] = CharacterType.Kangaroo;
            applicationHandler.CharaType[1] = CharacterType.Customized;
            applicationHandler.DiffiType = DifficultyType.Easy;
        }
        else if (storyNum == 3 && ind == 2) {
            applicationHandler.CharaType[0] = CharacterType.Koala;
            applicationHandler.CharaType[1] = CharacterType.Customized;
            applicationHandler.DiffiType = DifficultyType.Normal;
        }
        else if (storyNum == 3 && ind == 7) {
            applicationHandler.CharaType[0] = CharacterType.Koala;
            applicationHandler.CharaType[1] = CharacterType.Customized;
            applicationHandler.DiffiType = DifficultyType.Normal;
        }
        else if (storyNum == 3 && ind == 11) {
            applicationHandler.CharaType[0] = CharacterType.Koala;
            applicationHandler.CharaType[1] = CharacterType.Customized;
            applicationHandler.DiffiType = DifficultyType.Normal;
        }
        else if (storyNum == 3 && ind == 15) {
            applicationHandler.CharaType[0] = CharacterType.Koala;
            applicationHandler.CharaType[1] = CharacterType.Customized;
            applicationHandler.DiffiType = DifficultyType.Normal;
        }
        else if (storyNum == 3 && ind == 19) {
            applicationHandler.CharaType[0] = CharacterType.Koala;
            applicationHandler.CharaType[1] = CharacterType.Customized;
            applicationHandler.DiffiType = DifficultyType.Normal;
        }
    }
}