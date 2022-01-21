using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SimpleStory : MonoBehaviour {
    private GameObject Prefab;
    private int ind, storyNum, lastTag;
    private bool isClick, isFinish, isRun, isEnd;// isFinal;
    private GameObject buttonParent;
    private ApplicationHandler applicationHandler;

    void Awake()
    {
        applicationHandler = GameObject.Find("ApplicationHandler").GetComponent<ApplicationHandler>();
    }

    void Start()
    {
        applicationHandler.IsSimple = true;
        isClick = false;
        ind = applicationHandler.GameData.Schedule_SimpleChange;
        storyNum = applicationHandler.GameData.Schedule_Simple;
        if (ind == 0) //一個story結束後下一次載入select場景
        {
            if (applicationHandler.GameData.SimpleIsFinish == true) //已經玩過一輪
                Prefab = Instantiate(Resources.Load<GameObject>("select_3"), gameObject.transform);
            else
                Prefab = Instantiate(Resources.Load<GameObject>("select_" + applicationHandler.GameData.Schedule_Simple.ToString()), gameObject.transform);
            buttonParent = gameObject.transform.GetChild(0).gameObject;
            for (int i = 1;i <= 3;i += 1)
                buttonParent.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(delegate () { StoryOnClick(); });
        }
        else //只結束小關卡
        {
            if (applicationHandler.GameData.IswinForSimple) //打贏AI
            {
                ind++;
                Prefab = Instantiate(Resources.Load<GameObject>(storyNum.ToString() + "-" + ind.ToString()), gameObject.transform);
                isClick = true;
                isRun = false;
                isFinish = true;
            }
            else //打輸AI
            {
                ind++;
                Prefab = Instantiate(Resources.Load<GameObject>(storyNum.ToString() + "-" + ind.ToString() + "-2"), gameObject.transform);
                isClick = true;
                isRun = false;
                isFinish = true;
            }
        }
    }

    void Update()
    {
        if (Prefab != null && isClick == true) {
            //Debug.Log(2222);
            switch (Prefab.tag) {
                case "1":   //有對話
                            //isFinish = Prefab.transform.GetChild(1).gameObject.GetComponent<DialogSystem>().Finished;
                    isFinish = Prefab.transform.GetComponent<SimpleStory_DialogSystem>().Finished;
                    if (Keyboard.current.rightArrowKey.isPressed && isFinish) {
                        Destroy(Prefab);
                        ind++;
                        Prefab = Instantiate(Resources.Load<GameObject>(storyNum.ToString() + "-" + ind.ToString()), gameObject.transform);
                    }
                    break;
                case "2":   //進入關卡前畫面
                    if (!isRun && isFinish) {
                        applicationHandler.GameData.Schedule_SimpleChange = ind;
                        applicationHandler.GameData.SaveData();
                        StartCoroutine(UpdateIntoGame());
                        isRun = true;
                    }
                    break;
                case "3":   //關卡結束時畫面
                    if (!isRun && isFinish) {
                        //Debug.Log(1);
                        StartCoroutine(UpdateExitGame());
                        isRun = true;
                    }
                    break;
                case "4":   //結束故事返回選擇畫面
                    /*if (isEnd) {
                        isEnd = false;
                        buttonParent = gameObject.transform.GetChild(0).gameObject;
                        for (int i = 1;i <= 3;i += 1)
                            buttonParent.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(delegate () { StoryOnClick(); });

                    }*/
                    if (Keyboard.current.rightArrowKey.isPressed && isFinish) {
                        if (storyNum < 4)
                            storyNum++;
                        if (storyNum > applicationHandler.GameData.Schedule_Simple && storyNum < 5) {
                            applicationHandler.GameData.Schedule_Simple = storyNum;
                            applicationHandler.GameData.SaveData();
                        }
                        Destroy(Prefab);
                        ind = 0;
                        applicationHandler.GameData.Schedule_SimpleChange = ind;
                        applicationHandler.GameData.SaveData();
                        if (applicationHandler.GameData.SimpleIsFinish == true) //已經玩過一輪
                            Prefab = Instantiate(Resources.Load<GameObject>("select_3"), gameObject.transform);
                        else
                            Prefab = Instantiate(Resources.Load<GameObject>("select_" + applicationHandler.GameData.Schedule_Simple.ToString()), gameObject.transform);
                        isEnd = true;
                    }
                    break;
                case "5":
                    if (isEnd) {
                        isEnd = false;
                        buttonParent = gameObject.transform.GetChild(0).gameObject;
                        for (int i = 1;i <= 3;i += 1)
                            buttonParent.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(delegate () { StoryOnClick(); });

                    }
                    break;
            }
        }
    }

    private void StoryOnClick()
    {
        ind = 1;
        isClick = true;
        var button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if (button.name == "story1")
            storyNum = 1;
        else if (button.name == "story2")
            storyNum = 2;
        else if (button.name == "story3")
            storyNum = 3;
        if (storyNum >= 2) {
            applicationHandler.GameData.SimpleIsFinish = true;
            applicationHandler.GameData.SaveData();
        }
        applicationHandler.GameData.Schedule_Simple = storyNum;
        applicationHandler.GameData.Schedule_SimpleChange = ind;
        applicationHandler.GameData.SaveData();
        Destroy(Prefab);
        //lastTag = 4;
        Prefab = Instantiate(Resources.Load<GameObject>(storyNum.ToString() + "-1"), gameObject.transform);
        //isFinish = Prefab.transform.GetChild(1).gameObject.GetComponent<DialogSystem>().Finished;
        isFinish = Prefab.transform.GetComponent<SimpleStory_DialogSystem>().Finished;
    }

    private IEnumerator UpdateIntoGame()
    {
        yield return new WaitForSecondsRealtime(2);
        // 進入AI
        applicationHandler.GameData.IswinForSimple = false;
        applicationHandler.GameData.SaveData();
        ConnectAI();
        applicationHandler.IsDuel = false;
        SceneManager.LoadScene("Battle");
    }

    public void OnClick()
    {
        SceneManager.LoadScene("Lobby");
    }

    private IEnumerator UpdateExitGame()
    {
        yield return new WaitForSecondsRealtime(2);
        if (applicationHandler.GameData.IswinForSimple) {//成功
            Destroy(Prefab);
            applicationHandler.GameData.Schedule_SimpleChange = ind;
            applicationHandler.GameData.SaveData();
            ind++;
            Prefab = Instantiate(Resources.Load<GameObject>(storyNum.ToString() + "-" + ind.ToString()), gameObject.transform);
            isRun = false;
        }
        else {//失敗
            Destroy(Prefab);
            ind--;
            Prefab = Instantiate(Resources.Load<GameObject>("Assets/Prefabs/" + storyNum.ToString() + "-" + ind.ToString()), gameObject.transform);
            applicationHandler.GameData.Schedule_SimpleChange = ind;
            applicationHandler.GameData.SaveData();
            isRun = false;
        }
    }

    private void ConnectAI()
    {
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