using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum InstructionType:ushort { Move, Attack, Assign, If, Loop, Swap }

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
    // ***Arguments***
    // Move {Direction and Color}
    // Attack {}
    // Assign {Arithmetic, Source1, Source2, Constant, Destination}
    // If {Logic, Source1, Source2, Constant}
    // Loop {Logic, Source1, Source2, Constant}
    // Swap {Index1, Constant1, Index2, Constant2}
    public Instruction(InstructionType type, int[] arguments = null) {
        _Type = type;
        if (arguments != null) _Arguments = arguments.ToList();
        else _Arguments = new List<int>();
    }
    
    public ushort GetInstuctionCost() {
        switch (Type) {
            case InstructionType.Move:
                return 2;
            case InstructionType.Loop:
                return 1;
            case InstructionType.If:
                return 1;
            case InstructionType.Assign:
                return 1;
            case InstructionType.Attack:
                return 4;
            default:
                return 0;
        }
    }
}
