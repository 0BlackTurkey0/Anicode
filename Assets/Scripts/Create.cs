using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Create : MonoBehaviour
{
    private GameObject Prefab;
    private int record {
        get {
            int temp = PlayerPrefs.GetInt("Record_First", 0);
            if (temp == 4)
                temp = 3;
            return temp;
        }
        set { PlayerPrefs.SetInt("Record_First", value); }
    }
    private int ind, storyNum;
    private bool isClick, isFinish, isRun, isEnd;// isFinal;
    private GameObject buttonParent;

    // Start is called before the first frame update
    void Start()
    {
        //if (PlayerPrefs.GetInt("Record_First", 0) == 4)
        //    isFinal = true;
        isClick = false;
        Prefab = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/select_" + record.ToString() + ".prefab", typeof(GameObject)) as GameObject, gameObject.GetComponent<Transform>());
        buttonParent = gameObject.transform.GetChild(0).gameObject;
        for (int i = 1;i <= 3;i += 1)
            buttonParent.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(delegate () { StoryOnClick(); });
    }

    // Update is called once per frame
    void Update()
    {
        if (Prefab != null && isClick == true) {
            switch (Prefab.tag) {
                case "1":   //有對話
                    isFinish = Prefab.transform.GetChild(1).gameObject.GetComponent<DialogSystem>().Finished;
                    if (Input.GetKeyDown(KeyCode.RightArrow) && isFinish) {
                        Destroy(Prefab);
                        ind++;
                        Prefab = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/" + storyNum.ToString() + "-" + ind.ToString() + ".prefab", typeof(GameObject)) as GameObject, gameObject.GetComponent<Transform>());
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
                        StartCoroutine(UpdateIntoGame());
                        isRun = true;
                    }
                    break;
                case "4":   //結束故事返回選擇畫面
                    if (isEnd) {
                        isEnd = false;
                        buttonParent = gameObject.transform.GetChild(0).gameObject;
                        for (int i = 1;i <= 3;i += 1)
                            buttonParent.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(delegate () { StoryOnClick(); });
                    }
                    if (Input.GetKeyDown(KeyCode.RightArrow) && isFinish) {
                        if (storyNum < 4)
                            storyNum++;
                        if (storyNum > record && storyNum < 5)
                            record = storyNum;
                        Destroy(Prefab);
                        Prefab = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/select_" + record.ToString() + ".prefab", typeof(GameObject)) as GameObject, gameObject.GetComponent<Transform>());
                        isEnd = true;
                    }
                    /*if (!isFinal && storyNum == 3) {
                        StartCoroutine(UpdateFinal());
                    }*/
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
        Destroy(Prefab);
        Prefab = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/" + storyNum.ToString() + "-1.prefab", typeof(GameObject)) as GameObject, gameObject.GetComponent<Transform>());
        isFinish = Prefab.transform.GetChild(1).gameObject.GetComponent<DialogSystem>().Finished;
    }

    private IEnumerator UpdateIntoGame()
    {
        yield return new WaitForSecondsRealtime(1);
        // 進入AI

        bool isSuccess = true;  //判斷對戰結果
        if (Prefab.tag == "3")
            isSuccess = true;
        if (isSuccess) {
            Destroy(Prefab);
            ind++;
            Prefab = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/" + storyNum.ToString() + "-" + ind.ToString() + ".prefab", typeof(GameObject)) as GameObject, gameObject.GetComponent<Transform>());
            yield return null;
            isRun = false;
        }
        else {
            Destroy(Prefab);
            Prefab = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/" + storyNum.ToString() + "-" + ind.ToString() + "-2.prefab", typeof(GameObject)) as GameObject, gameObject.GetComponent<Transform>());
            isRun = false;
        }
    }

    /*private IEnumerator UpdateFinal()
    {
        storyNum = 4;
        Destroy(Prefab);
        Prefab = Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/3-22.prefab", typeof(GameObject)) as GameObject, gameObject.GetComponent<Transform>());
        yield return new WaitForSecondsRealtime(3);
        isFinal = true;
    }*/
}