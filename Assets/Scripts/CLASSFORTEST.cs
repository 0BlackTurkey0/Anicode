using System.Text.Json;
using UnityEngine;

public class CLASSFORTEST : MonoBehaviour {
    Code code1;
    Code code2;
    
    void Start()
    {
        code1 = new Code();
        code1.Insert(InstructionType.Attack, 0, 0);

        code1.Insert(InstructionType.Move, 0, 0, new int[1] { 1 });

        code1.Insert(InstructionType.Move, 0, 0, new int[1] { 1 });

        code2 = new Code(code1);
        code2.Delete(0);

        string str = JsonSerializer.Serialize(code2);
        Debug.Log(str);

        Code data = JsonSerializer.Deserialize<Code>(str);
        data.Display();

        //code1.Display();
        //code2.Display();
    }
}