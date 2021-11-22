using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Code {
    private List<Tuple<Instruction, ushort>> Instructions;
    private Stack<ushort> Records;
    private ushort ProgramCounter;

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
        else if (level > Instructions[position-1].Item2) {
            if (Instructions[position - 1].Item1.GetInstuctionType() == InstructionType.Loop || Instructions[position - 1].Item1.GetInstuctionType() == InstructionType.If)
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
        if (Instructions[position].Item1.GetInstuctionType() == InstructionType.Loop || Instructions[position].Item1.GetInstuctionType() == InstructionType.If) {
            ushort tmp = position;
            while (Instructions[++tmp].Item2 > Instructions[position].Item2)
                Instructions[tmp] = new Tuple<Instruction, ushort>(Instructions[tmp].Item1, (ushort)(Instructions[tmp].Item2 - 1));
        }
        Instructions.RemoveAt(position);
    }

    public Instruction Next(bool condition = true) {
        if (ProgramCounter >= Instructions.Count) return new Instruction(InstructionType.None);
        if (!condition) {
            ushort tmp = Records.Pop();
            while (Instructions[ProgramCounter].Item2 > Instructions[tmp].Item2)
                ProgramCounter++;
        }
        if (Instructions[ProgramCounter].Item1.GetInstuctionType() == InstructionType.Loop || Instructions[ProgramCounter].Item1.GetInstuctionType() == InstructionType.If)
            Records.Push(ProgramCounter);
        if (Records.Count > 0) {
            ushort tmp = Records.Peek();
            while (Records.Count > 0 && Instructions[ProgramCounter].Item2 <= Instructions[tmp].Item2) {
                if (Instructions[tmp].Item1.GetInstuctionType() == InstructionType.Loop) {
                    ProgramCounter = tmp;
                    return Instructions[ProgramCounter].Item1;
                }
                else if (Instructions[tmp].Item1.GetInstuctionType() == InstructionType.If) {
                    Records.Pop();
                    if (Records.Count > 0) tmp = Records.Peek();
                }
            }
        }
        return Instructions[ProgramCounter++].Item1;
    }

    public void Display() {
        System.Text.StringBuilder display = new System.Text.StringBuilder();
        foreach (var i in Instructions) {
            for (int x = 0; x < i.Item2; x++)
                display.Append("  ");
            display.AppendLine(i.Item1.GetInstuctionType().ToString());
        }
        Debug.Log(display.ToString());
    }
}
 