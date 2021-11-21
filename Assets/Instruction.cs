using System.Linq;
using System.Collections.Generic;

public enum InstructionType:ushort {None, Move, Loop, If, Assign, Attack}

public class Instruction {
    private InstructionType Type;
    private List<int> Arguments;
    /* Move arguments {Diffculty, Direction, Color}
     * Loop arguments {Loop Times}
     * If arguments {Judge Type}
     */
    public Instruction(InstructionType type, int[] arguments = null) {
        Type = type;
        Arguments = arguments.ToList();
    }

    public InstructionType GetInstuctionType() {
        return Type;
    }

    public int GetInstuctionArgument(ushort index) {
        return Arguments[index];
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
