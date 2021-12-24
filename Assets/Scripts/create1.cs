using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class create1 : MonoBehaviour
{
    private GameObject Prefab,Prefab1;
    int Count,record;
    string story,number,select;
    bool finish,flag;

    // Start is called before the first frame update
    void Start()
    {
        //record = PlayerPrefs.GetInt("Record_First", 0);
        //story = "Assets/prefab/select" + record.ToString() + ".prefab";
        Count = 1;
        flag = true;
        number = Count.ToString();
        story = "Assets/prefab/1-" + number + ".prefab";
        Prefab = AssetDatabase.LoadAssetAtPath(story, typeof(GameObject)) as GameObject;
        Prefab1 = Instantiate(Prefab, gameObject.GetComponent<Transform>());
        finish = Prefab1.transform.GetChild(1).gameObject.GetComponent<DialogSystem>().Finished;

    }

    public void OnClick()
    { 
        var button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject; 
        print(button.name); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Prefab1 != null)
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
                        story = "Assets/prefab/1-" + number + ".prefab";
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
                        story = "Assets/prefab/1-" + number + ".prefab";
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
                        story = "Assets/prefab/1-" + number + ".prefab";
                        Prefab = AssetDatabase.LoadAssetAtPath(story, typeof(GameObject)) as GameObject;
                        Prefab1 = Instantiate(Prefab, gameObject.GetComponent<Transform>());
                    }
                    break;
                case "4":
                    if (flag) {
                        flag = false;
                        Destroy(Prefab1);
                        story = "Assets/prefab/1-19.prefab";
                        Prefab = AssetDatabase.LoadAssetAtPath(story, typeof(GameObject)) as GameObject;
                        Prefab1 = Instantiate(Prefab, gameObject.GetComponent<Transform>());
                    }
                    break;
            }
        }
    }
}
