using System.Linq;
using System.Collections.Generic;

public enum InstructionType:ushort { Move, Attack, Assign, If, Loop, Swap }

public class Instruction {

    private InstructionType _type;
    public InstructionType Type {
        get { return _type; }
        set { _type = value; }
    }

    private List<int> _arguments;
    public List<int> Arguments {
        get { return _arguments; }
        set { _arguments = value; }
    }

    // ***Arguments Details***
    // Move {Direction or Color or Both}
    // Attack {}
    // Assign {Arithmetic, Source1, Source2, Constant, Destination}
    // If {Logic, Source1, Source2, Constant}
    // Loop {Logic, Source1, Source2, Constant}
    // Swap {Index1, Constant1, Index2, Constant2}

    public Instruction(InstructionType type, int[] arguments = null) {
        _type = type;
        if (arguments != null) _arguments = arguments.ToList();
        else _arguments = new List<int>();
    }
    
    public ushort GetInstuctionCost() {
        switch (_type) {
            case InstructionType.Move:
                return 2;
            case InstructionType.Attack:
                return 4;
            case InstructionType.Assign:
                return 1;
            case InstructionType.If:
                return 1;
            case InstructionType.Loop:
                return 1;
            case InstructionType.Swap:
                return 1;
            default:
                return 0;
        }
    }
}
