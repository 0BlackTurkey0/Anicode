using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class create1 : MonoBehaviour
{
    private GameObject Prefab, Prefab1;
    int Count, record, num, Canrun, open2, open3;
    string story, number, select;
    bool finish, flag;

    // Start is called before the first frame update
    void Start()
    {
        open2 = 0;
        open3 = 0;
        Canrun = 0;
        record = PlayerPrefs.GetInt("Record_First", 0);
        Debug.Log("aaaaaaa");
        Debug.Log(record);
        story = "Assets/prefab/select_" + record.ToString() + ".prefab";
        Prefab = AssetDatabase.LoadAssetAtPath(story, typeof(GameObject)) as GameObject;
        Prefab1 = Instantiate(Prefab, gameObject.GetComponent<Transform>());
        Count = 0;
        flag = true;
        /*number = Count.ToString();
        story = "Assets/prefab/1-" + number + ".prefab";
        Prefab = AssetDatabase.LoadAssetAtPath(story, typeof(GameObject)) as GameObject;
        Prefab1 = Instantiate(Prefab, gameObject.GetComponent<Transform>());
        finish = Prefab1.transform.GetChild(1).gameObject.GetComponent<DialogSystem>().Finished;*/

    }

    public void StoryOnClick()
    {
        Canrun = 1;
        var button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if (button.name == "story1")
        {
            num = 1;
        }
        else if (button.name == "story2")
        {
            num = 2;
        }
        else if (button.name == "story3")
        {
            num = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Prefab1 != null && Canrun == 1)
        {
            switch (Prefab1.tag)
            {
                case "1":
                    finish = Prefab1.transform.GetChild(1).gameObject.GetComponent<DialogSystem>().Finished;
                    if (Input.GetKeyDown(KeyCode.RightArrow) && finish)
                    {
                        Destroy(Prefab1);
                        Count++;
                        number = Count.ToString();
                        story = "Assets/prefab/" + num.ToString() + "_" + number + ".prefab";
                        Prefab = AssetDatabase.LoadAssetAtPath(story, typeof(GameObject)) as GameObject;
                        Prefab1 = Instantiate(Prefab, gameObject.GetComponent<Transform>());
                    }
                    break;
                case "2":
                    if (Input.GetKeyDown(KeyCode.RightArrow) && finish)
                    {
                        Destroy(Prefab1);
                        Count++;
                        number = Count.ToString();
                        story = "Assets/prefab/" + num.ToString() + "_" + number + ".prefab";
                        Prefab = AssetDatabase.LoadAssetAtPath(story, typeof(GameObject)) as GameObject;
                        Prefab1 = Instantiate(Prefab, gameObject.GetComponent<Transform>());
                    }
                    break;
                case "3":
                    if (Input.GetKeyDown(KeyCode.RightArrow) && finish)
                    {
                        Destroy(Prefab1);
                        Count++;
                        number = Count.ToString();
                        story = "Assets/prefab/" + num.ToString() + "_" + number + ".prefab";
                        Prefab = AssetDatabase.LoadAssetAtPath(story, typeof(GameObject)) as GameObject;
                        Prefab1 = Instantiate(Prefab, gameObject.GetComponent<Transform>());
                    }
                    break;
                case "4":
                    if (flag)
                    {
                        flag = false;
                        Destroy(Prefab1);
                        story = "Assets/prefab/1-19.prefab";
                        Prefab = AssetDatabase.LoadAssetAtPath(story, typeof(GameObject)) as GameObject;
                        Prefab1 = Instantiate(Prefab, gameObject.GetComponent<Transform>());
                    }
                    break;
            }
        }
        if (num == 1 && Count == 17 && open2 == 0)
        {
            open2 = 1;
            PlayerPrefs.SetInt("Record_First", 2);
        }
        if (num == 2 && Count == 20 && open3 == 0)
        {
            open3 = 1;
            PlayerPrefs.SetInt("Record_First", 3);
        }
    }
}