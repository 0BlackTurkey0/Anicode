using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class create1 : MonoBehaviour
{
    private GameObject Prefab,Prefab1;
    public GameObject AspectRatio;
    int Count;
    string story,number,select;
    bool finish;

    // Start is called before the first frame update
    void Start()
    {
        Count = 1;
        number = Count.ToString();
        story = "Assets/prefab/1-" + number + ".prefab";
        Prefab = AssetDatabase.LoadAssetAtPath(story, typeof(GameObject)) as GameObject;
        //Prefab1=Instantiate(Prefab, Canvas);
        Prefab1 = Instantiate(Prefab, AspectRatio.GetComponent<Transform>());
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
                        //Prefab1 = Instantiate(Prefab, Canvas);
                        Prefab1 = Instantiate(Prefab, AspectRatio.GetComponent<Transform>());
                        //finish = Prefab1.transform.GetChild(1).gameObject.GetComponent<DialogSystem>().Finished;
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
                        //Prefab1 = Instantiate(Prefab, Canvas);
                        Prefab1 = Instantiate(Prefab, AspectRatio.GetComponent<Transform>());
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
                        //Prefab1 = Instantiate(Prefab, Canvas);
                        Prefab1 = Instantiate(Prefab, AspectRatio.GetComponent<Transform>());
                    }
                    break;
            }
        }
    }
}
