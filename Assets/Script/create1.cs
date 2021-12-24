using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class create1 : MonoBehaviour
{
    private GameObject Prefab,Prefab1;
    int Count,flag;
    string story,number,select;
    bool finish,flag2;

    // Start is called before the first frame update
    void Start()
    {
        Count = 1;
        flag2 = true;
        number = Count.ToString();
        story = "Assets/prefab/1-" + number + ".prefab";
        Prefab = AssetDatabase.LoadAssetAtPath(story, typeof(GameObject)) as GameObject;
        Prefab1 = Instantiate(Prefab, gameObject.GetComponent<Transform>());
        finish = Prefab1.transform.GetChild(1).gameObject.GetComponent<DialogSystem>().Finished;

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
                    if (flag2) {
                        flag = 0;
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
