using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Code {
    private List<Tuple<Instruction, ushort>> Instructions;
    public ushort Size {
        get { return (ushort)Instructions.Count; }
    }

    private Stack<ushort> Records;
    private ushort ProgramCounter;

    public Instruction this[ushort index] {
        get {
            if (index < Instructions.Count) return Instructions[index].Item1;
            else return null;
        }
    }
    
    public Code(Code copy = null) {
        if (copy != null) Instructions = copy.Instructions;
        else Instructions = new List<Tuple<Instruction, ushort>>();
        Records = new Stack<ushort>();
    }

    public void Init() {
        Records.Clear();
        ProgramCounter = 0;
    }

    public void Insert(InstructionType type, ushort position, ushort level, int[] arguments = null) {
        if (position > Instructions.Count) position = (ushort)Instructions.Count;
        if (position == 0) level = 0;
        else if (level > Instructions[position - 1].Item2) {
            if (Instructions[position - 1].Item1.Type == InstructionType.Loop || Instructions[position - 1].Item1.Type == InstructionType.If)
                level = (ushort)(Instructions[position - 1].Item2 + 1);
            else
                level = Instructions[position - 1].Item2;
        }
        else if (position < Instructions.Count)
            if (level < Instructions[position].Item2)
                level = Instructions[position].Item2;
        Tuple<Instruction, ushort> New_Instruction = new Tuple<Instruction, ushort>(new Instruction(type, arguments), level);
        Instructions.Insert(position, New_Instruction);
    }

    public void Delete(ushort position) {
        if (position >= Instructions.Count) position = (ushort)(Instructions.Count - 1);
        if (Instructions[position].Item1.Type == InstructionType.Loop || Instructions[position].Item1.Type == InstructionType.If) {
            ushort tmp = position;
            while (tmp + 1 < Instructions.Count && Instructions[++tmp].Item2 > Instructions[position].Item2)
                Instructions[tmp] = new Tuple<Instruction, ushort>(Instructions[tmp].Item1, (ushort)(Instructions[tmp].Item2 - 1));
        }
        Instructions.RemoveAt(position);
    }

    public void Change(ushort source, ushort destination, ushort level) {
        if (source >= Instructions.Count) source = (ushort)(Instructions.Count - 1);
        if (destination > Instructions.Count) destination = (ushort)Instructions.Count;
        if (Instructions[source].Item1.Type == InstructionType.Loop || Instructions[source].Item1.Type == InstructionType.If) {
            ushort end = source;
            while (end + 1 < Instructions.Count && Instructions[end + 1].Item2 > Instructions[source].Item2) end++;
            if (destination > end || destination <= source) {
                if (source < destination) {
                    Insert(Instructions[source].Item1.Type, destination, level, Instructions[source].Item1.Arguments.ToArray());
                    level = Instructions[destination++].Item2;
                    for (int i = source + 1; i <= end; i++) {
                        int AddLevel = Instructions[i].Item2 - Instructions[source].Item2;
                        Insert(Instructions[i].Item1.Type, destination++, (ushort)(level + AddLevel), Instructions[i].Item1.Arguments.ToArray());
                    }
                }
                else {
                    Insert(Instructions[source].Item1.Type, destination, level, Instructions[source].Item1.Arguments.ToArray());
                    source++;
                    end++;
                    level = Instructions[destination++].Item2;
                    for (int i = source + 1; i <= end; i += 2) {
                        int AddLevel = Instructions[i].Item2 - Instructions[source].Item2;
                        Insert(Instructions[i].Item1.Type, destination++, (ushort)(level + AddLevel), Instructions[i].Item1.Arguments.ToArray());
                        source++;
                        end++;
                    }
                }
                for (int i = source; i <= end; i++)
                    Delete(source);
            }
        }
        else {
            Insert(Instructions[source].Item1.Type, destination, level, Instructions[source].Item1.Arguments.ToArray());
            if (source >= destination) source++;
            Delete(source);
        }
    }

    public ushort Next(bool condition = true) {
        if (ProgramCounter > Instructions.Count) return ProgramCounter;
        if (!condition) {
            ushort tmp = Records.Pop();
            while (ProgramCounter < Instructions.Count && Instructions[ProgramCounter].Item2 > Instructions[tmp].Item2)
                ProgramCounter++;
            if (ProgramCounter > Instructions.Count) return ProgramCounter;
        }
        if (Records.Count > 0) {
            ushort tmp = Records.Peek();
            while (Records.Count > 0 && (ProgramCounter == Instructions.Count || Instructions[ProgramCounter].Item2 <= Instructions[tmp].Item2)) {
                if (Instructions[tmp].Item1.Type == InstructionType.Loop) {
                    ProgramCounter = tmp;
                    return ProgramCounter++;
                }
                else if (Instructions[tmp].Item1.Type == InstructionType.If) {
                    Records.Pop();
                    if (Records.Count > 0) tmp = Records.Peek();
                }
            }
        }
        if (ProgramCounter < Instructions.Count && (Instructions[ProgramCounter].Item1.Type == InstructionType.Loop || Instructions[ProgramCounter].Item1.Type == InstructionType.If))
            Records.Push(ProgramCounter);
        return ProgramCounter++;
    }

    public ushort GetLevel(ushort index) {
        if (index < Instructions.Count) return Instructions[index].Item2;
        return 0;
    }

    public void Display() {
        System.Text.StringBuilder display = new System.Text.StringBuilder();
        foreach (var i in Instructions) {
            for (int x = 0; x < i.Item2; x++)
                display.Append("  ");
            display.AppendLine(i.Item1.Type.ToString());
        }
        Debug.Log(display.ToString());
    }
}
 