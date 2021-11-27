using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum InstructionType:ushort {Move, Loop, If, Assign, Attack}

public class Instruction {
    private InstructionType _Type;
    public InstructionType Type {
        get { return _Type; }
        set { _Type = value; }
    }
    private List<int> _Arguments;
    public List<int> Arguments {
        get { return _Arguments; }
        set { _Arguments = value; }
    }
    /* Move arguments {Diffculty, Direction, Color}
     * Loop arguments {Loop Times}
     * If arguments {Judge Type}
     */
    public Instruction(InstructionType type, int[] arguments = null) {
        _Type = type;
        if (arguments != null) _Arguments = arguments.ToList();
        else _Arguments = new List<int>();
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
