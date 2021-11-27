using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum InstructionType:ushort {Move, Loop, If, Assign, Attack}

public class Instruction {
    public InstructionType Type;
    public List<int> Arguments;
    /* Move arguments {Diffculty, Direction, Color}
     * Loop arguments {Loop Times}
     * If arguments {Judge Type}
     */
    public Instruction(InstructionType type, int[] arguments = null) {
        Type = type;
        if (arguments != null) Arguments = arguments.ToList();
        else Arguments = new List<int>();
    }

    public InstructionType GetInstuctionType() {
        return Type;
    }

    public ushort GetInstuctionCost() {
        switch (Type) {
            case InstructionType.Move:
                return 1;
            case InstructionType.Loop:
                return 1;
            case InstructionType.If:
                return 1;
            case InstructionType.Assign:
                return 3;
            case InstructionType.Attack:
                return 1;
            default:
                return 0;
        }
    }
}
