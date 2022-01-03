using System;
using System.Collections.Generic;

public class Code {

    private List<Tuple<Instruction, ushort>> _instructions;
    private Stack<ushort> _records;
    private ushort _programCounter;

    /*public List<Tuple<Instruction, ushort>> Instructions {
        get { return _instructions; }
        set { _instructions = value; } 
    }

    public Stack<ushort> Records {
        get { return _records; }
        set { _records = value; }
    }

    public ushort ProgramCounter {
        get { return _programCounter; }
        set { _programCounter = value; }
    }*/

    public ushort Size {
        get { return (ushort)_instructions.Count; }
    }

    public Instruction this[ushort index] {
        get {
            if (index < _instructions.Count) return _instructions[index].Item1;
            else return null;
        }
    }

    public Code(Code copy = null)
    {
        if (copy != null) _instructions = copy._instructions;
        else _instructions = new List<Tuple<Instruction, ushort>>();
        _records = new Stack<ushort>();
    }

    public void Init()
    {
        _records.Clear();
        _programCounter = 0;
    }

    public void Insert(InstructionType type, ushort position, ushort level, int[] arguments = null)
    {
        if (position > _instructions.Count) position = (ushort)_instructions.Count;
        if (position == 0) level = 0;
        else if (level > _instructions[position - 1].Item2) {
            if (_instructions[position - 1].Item1.Type == InstructionType.Loop || _instructions[position - 1].Item1.Type == InstructionType.If)
                level = (ushort)(_instructions[position - 1].Item2 + 1);
            else
                level = _instructions[position - 1].Item2;
        }
        else if (position < _instructions.Count)
            if (level < _instructions[position].Item2)
                level = _instructions[position].Item2;
        Tuple<Instruction, ushort> New_Instruction = new Tuple<Instruction, ushort>(new Instruction(type, arguments), level);
        _instructions.Insert(position, New_Instruction);
    }

    public void Delete(ushort position)
    {
        if (position >= _instructions.Count) position = (ushort)(_instructions.Count - 1);
        if (_instructions[position].Item1.Type == InstructionType.Loop || _instructions[position].Item1.Type == InstructionType.If) {
            ushort tmp = position;
            while (tmp + 1 < _instructions.Count && _instructions[++tmp].Item2 > _instructions[position].Item2)
                _instructions[tmp] = new Tuple<Instruction, ushort>(_instructions[tmp].Item1, (ushort)(_instructions[tmp].Item2 - 1));
        }
        _instructions.RemoveAt(position);
    }

    public void Change(ushort source, ushort destination, ushort level)
    {
        if (source >= _instructions.Count) source = (ushort)(_instructions.Count - 1);
        if (destination > _instructions.Count) destination = (ushort)_instructions.Count;
        if (_instructions[source].Item1.Type == InstructionType.Loop || _instructions[source].Item1.Type == InstructionType.If) {
            ushort end = source;
            while (end + 1 < _instructions.Count && _instructions[end + 1].Item2 > _instructions[source].Item2) end++;
            if (destination > end || destination <= source) {
                if (source < destination) {
                    Insert(_instructions[source].Item1.Type, destination, level, _instructions[source].Item1.Arguments.ToArray());
                    level = _instructions[destination++].Item2;
                    for (int i = source + 1;i <= end;i++) {
                        int AddLevel = _instructions[i].Item2 - _instructions[source].Item2;
                        Insert(_instructions[i].Item1.Type, destination++, (ushort)(level + AddLevel), _instructions[i].Item1.Arguments.ToArray());
                    }
                }
                else {
                    Insert(_instructions[source].Item1.Type, destination, level, _instructions[source].Item1.Arguments.ToArray());
                    source++;
                    end++;
                    level = _instructions[destination++].Item2;
                    for (int i = source + 1;i <= end;i += 2) {
                        int AddLevel = _instructions[i].Item2 - _instructions[source].Item2;
                        Insert(_instructions[i].Item1.Type, destination++, (ushort)(level + AddLevel), _instructions[i].Item1.Arguments.ToArray());
                        source++;
                        end++;
                    }
                }
                for (int i = source;i <= end;i++)
                    Delete(source);
            }
        }
        else {
            Insert(_instructions[source].Item1.Type, destination, level, _instructions[source].Item1.Arguments.ToArray());
            if (source >= destination) source++;
            Delete(source);
        }
    }

    public ushort Next(bool condition = true)
    {
        if (_programCounter > _instructions.Count) return _programCounter;
        if (!condition) {
            ushort tmp = _records.Pop();
            while (_programCounter < _instructions.Count && _instructions[_programCounter].Item2 > _instructions[tmp].Item2)
                _programCounter++;
            if (_programCounter > _instructions.Count) return _programCounter;
        }
        if (_records.Count > 0) {
            ushort tmp = _records.Peek();
            while (_records.Count > 0 && (_programCounter == _instructions.Count || _instructions[_programCounter].Item2 <= _instructions[tmp].Item2)) {
                if (_instructions[tmp].Item1.Type == InstructionType.Loop) {
                    _programCounter = tmp;
                    return _programCounter++;
                }
                else if (_instructions[tmp].Item1.Type == InstructionType.If) {
                    _records.Pop();
                    if (_records.Count > 0) tmp = _records.Peek();
                }
            }
        }
        if (_programCounter < _instructions.Count && (_instructions[_programCounter].Item1.Type == InstructionType.Loop || _instructions[_programCounter].Item1.Type == InstructionType.If))
            _records.Push(_programCounter);
        return _programCounter++;
    }

    public ushort GetLevel(ushort index)
    {
        if (index < _instructions.Count) return _instructions[index].Item2;
        return 0;
    }

    /*
    public void Display() {
        System.Text.StringBuilder display = new System.Text.StringBuilder();
        foreach (var i in _instructions) {
            for (int x = 0; x < i.Item2; x++)
                display.Append("  ");
            display.AppendLine(i.Item1.Type.ToString());
        }
        Debug.Log(display.ToString());
    }
    */
}
