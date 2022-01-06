using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Create : MonoBehaviour {
    private GameObject Prefab;
    private int ind, storyNum, lastTag;
    private bool isClick, isFinish, isRun, isEnd;// isFinal;
    private GameObject buttonParent;
    private ApplicationHandler applicationHandler;

    void Awake() {
        applicationHandler = GameObject.Find("ApplicationHandler").GetComponent<ApplicationHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        applicationHandler.IsSimple = true;
        isClick = false;
        ind = applicationHandler.GameData.Schedule_SimpleChange;
        storyNum = applicationHandler.GameData.Schedule_Simple;
        if (ind == 0) //一個story結束後下一次載入select場景
        {
            if (applicationHandler.GameData.SimpleIsFinish == true) //已經玩過一輪
                Prefab = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/select_3.prefab", typeof(GameObject)) as GameObject, gameObject.GetComponent<Transform>());
            else
                Prefab = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/select_" + applicationHandler.GameData.Schedule_Simple.ToString() + ".prefab", typeof(GameObject)) as GameObject, gameObject.GetComponent<Transform>());
            buttonParent = gameObject.transform.GetChild(0).gameObject;
            for (int i = 1; i <= 3; i += 1)
                buttonParent.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(delegate () { StoryOnClick(); });
        }
        else //只結束小關卡
        {
            if (applicationHandler.GameData.IswinForSimple) //打贏AI
            {
                ind++;
                Prefab = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/" + storyNum.ToString() + "-" + ind.ToString() + ".prefab", typeof(GameObject)) as GameObject, gameObject.GetComponent<Transform>());
                isClick = true;
                isRun = false;
                isFinish = true;
            }
            else //打輸AI
            {
                ind++;
                Prefab = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/" + storyNum.ToString() + "-" + ind.ToString() + "-2" + ".prefab", typeof(GameObject)) as GameObject, gameObject.GetComponent<Transform>());
                isClick = true;
                isRun = false;
                isFinish = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Prefab != null && isClick == true) {
            //Debug.Log(2222);
            switch (Prefab.tag) {
                case "1":   //有對話
                    //isFinish = Prefab.transform.GetChild(1).gameObject.GetComponent<DialogSystem>().Finished;
                    isFinish = Prefab.transform.GetComponent<DialogSystem>().Finished;
                    if (Input.GetKeyDown(KeyCode.RightArrow) && isFinish) {
                        Destroy(Prefab);
                        ind++;
                        Prefab = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/" + storyNum.ToString() + "-" + ind.ToString() + ".prefab", typeof(GameObject)) as GameObject, gameObject.GetComponent<Transform>());
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
                    if (Input.GetKeyDown(KeyCode.RightArrow) && isFinish) {
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
                        if(applicationHandler.GameData.SimpleIsFinish == true) //已經玩過一輪
                            Prefab = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/select_3.prefab", typeof(GameObject)) as GameObject, gameObject.GetComponent<Transform>());
                        else
                            Prefab = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/select_" + applicationHandler.GameData.Schedule_Simple.ToString() + ".prefab", typeof(GameObject)) as GameObject, gameObject.GetComponent<Transform>());
                        isEnd = true;
                    }
                    break;
                case "5":
                    if (isEnd)
                    {
                        isEnd = false;
                        buttonParent = gameObject.transform.GetChild(0).gameObject;
                        for (int i = 1; i <= 3; i += 1)
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
        if(storyNum >= 2)
        {
            applicationHandler.GameData.SimpleIsFinish = true;
            applicationHandler.GameData.SaveData();
        }
        applicationHandler.GameData.Schedule_Simple = storyNum;
        applicationHandler.GameData.Schedule_SimpleChange = ind;
        applicationHandler.GameData.SaveData();
        Destroy(Prefab);
        //lastTag = 4;
        Prefab = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/" + storyNum.ToString() + "-1.prefab", typeof(GameObject)) as GameObject, gameObject.GetComponent<Transform>());
        //isFinish = Prefab.transform.GetChild(1).gameObject.GetComponent<DialogSystem>().Finished;
        isFinish = Prefab.transform.GetComponent<DialogSystem>().Finished;
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
        if (applicationHandler.GameData.IswinForSimple)
        {//成功
            Destroy(Prefab);
            applicationHandler.GameData.Schedule_SimpleChange = ind;
            applicationHandler.GameData.SaveData();
            ind++;
            Prefab = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/" + storyNum.ToString() + "-" + ind.ToString() + ".prefab", typeof(GameObject)) as GameObject, gameObject.GetComponent<Transform>());
            isRun = false;
        }
        else
        {//失敗
            Destroy(Prefab);
            ind --;
            Prefab = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/" + storyNum.ToString() + "-" + ind.ToString() + ".prefab", typeof(GameObject)) as GameObject, gameObject.GetComponent<Transform>());
            applicationHandler.GameData.Schedule_SimpleChange = ind;
            applicationHandler.GameData.SaveData();
            isRun = false;
        }
    }

    private void ConnectAI()
    {
        if (storyNum == 1 && ind == 2)
        {
            applicationHandler.CharaType[0] = CharacterType.Fox;
            applicationHandler.CharaType[1] = CharacterType.Kangaroo;
            applicationHandler.DiffiType = DifficultyType.Start;
        }
        else if (storyNum == 1 && ind == 6)
        {
            applicationHandler.CharaType[0] = CharacterType.Fox;
            applicationHandler.CharaType[1] = CharacterType.Kangaroo;
            applicationHandler.DiffiType = DifficultyType.Start;
        }
        else if (storyNum == 1 && ind == 9)
        {
            applicationHandler.CharaType[0] = CharacterType.Fox;
            applicationHandler.CharaType[1] = CharacterType.Kangaroo;
            applicationHandler.DiffiType = DifficultyType.Start;
        }
        else if (storyNum == 1 && ind == 12)
        {
            applicationHandler.CharaType[0] = CharacterType.Fox;
            applicationHandler.CharaType[1] = CharacterType.Kangaroo;
            applicationHandler.DiffiType = DifficultyType.Start;
        }
        else if (storyNum == 1 && ind == 16)
        {
            applicationHandler.CharaType[0] = CharacterType.Fox;
            applicationHandler.CharaType[1] = CharacterType.Kangaroo;
            applicationHandler.DiffiType = DifficultyType.Start;
        }
        else if (storyNum == 2 && ind == 3)
        {
            applicationHandler.CharaType[0] = CharacterType.Kangaroo;
            applicationHandler.CharaType[1] = CharacterType.Kangaroo;
            applicationHandler.DiffiType = DifficultyType.Easy;
        }
        else if (storyNum == 2 && ind == 8)
        {
            applicationHandler.CharaType[0] = CharacterType.Kangaroo;
            applicationHandler.CharaType[1] = CharacterType.Kangaroo;
            applicationHandler.DiffiType = DifficultyType.Easy;
        }
        else if (storyNum == 2 && ind == 11)
        {
            applicationHandler.CharaType[0] = CharacterType.Kangaroo;
            applicationHandler.CharaType[1] = CharacterType.Kangaroo;
            applicationHandler.DiffiType = DifficultyType.Easy;
        }
        else if (storyNum == 2 && ind == 15)
        {
            applicationHandler.CharaType[0] = CharacterType.Kangaroo;
            applicationHandler.CharaType[1] = CharacterType.Kangaroo;
            applicationHandler.DiffiType = DifficultyType.Easy;
        }
        else if (storyNum == 2 && ind == 19)
        {
            applicationHandler.CharaType[0] = CharacterType.Kangaroo;
            applicationHandler.CharaType[1] = CharacterType.Kangaroo;
            applicationHandler.DiffiType = DifficultyType.Easy;
        }
        else if (storyNum == 3 && ind == 2)
        {
            applicationHandler.CharaType[0] = CharacterType.Koala;
            applicationHandler.CharaType[1] = CharacterType.Kangaroo;
            applicationHandler.DiffiType = DifficultyType.Normal;
        }
        else if (storyNum == 3 && ind == 7)
        {
            applicationHandler.CharaType[0] = CharacterType.Koala;
            applicationHandler.CharaType[1] = CharacterType.Kangaroo;
            applicationHandler.DiffiType = DifficultyType.Normal;
        }
        else if (storyNum == 3 && ind == 11)
        {
            applicationHandler.CharaType[0] = CharacterType.Koala;
            applicationHandler.CharaType[1] = CharacterType.Kangaroo;
            applicationHandler.DiffiType = DifficultyType.Normal;
        }
        else if (storyNum == 3 && ind == 15)
        {
            applicationHandler.CharaType[0] = CharacterType.Koala;
            applicationHandler.CharaType[1] = CharacterType.Kangaroo;
            applicationHandler.DiffiType = DifficultyType.Normal;
        }
        else if (storyNum == 3 && ind == 19)
        {
            applicationHandler.CharaType[0] = CharacterType.Koala;
            applicationHandler.CharaType[1] = CharacterType.Kangaroo;
            applicationHandler.DiffiType = DifficultyType.Normal;
        }
    }
}

