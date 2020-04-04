using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputToken
{
    public bool Jump => Time.realtimeSinceStartup < _jumpTime;
    public bool Attack => Time.realtimeSinceStartup < _attackTime;
    public bool Block => HoldBlock;
    public bool Special => Time.realtimeSinceStartup < _specialTime;
    public float Horizontal { get; private set; }
    public float AbsHor => Mathf.Abs(Horizontal);
    public float Vertical { get; private set; }
    public float AbsVer => Mathf.Abs(Vertical);

    public Vector2 SecondDirection { get; set; }

    public float Buffer { set; get; } = .15f;
    public bool ForceNeutral { set; get; } = true;

    public void PressJump() => _jumpTime = Time.realtimeSinceStartup + Buffer;
    public void PressSpecial() => _specialTime = Time.realtimeSinceStartup + Buffer;
    public bool HoldBlock { get; set; }

    public void ClearJump() => _jumpTime = -12f;
    public void ClearAttack() => _attackTime = -12f;
    public void ClearSpecial() => _specialTime = -12f;


    public int DPDirection { get; private set; }
    public int HalfRollDirection { get; private set; }

    private float _jumpTime = -12f;
    private float _attackTime = -12f;
    private float _specialTime = -12f;

    public void PressAttack()
    {
        _attackTime = Time.realtimeSinceStartup + Buffer;
        DPDirection = DP();
        HalfRollDirection = HalfRoll();
        ClearHistory();
    }

    Vector2Int[] InputHistory = new Vector2Int[10];
    int count = 0;
    float lastHistoryEntry;
    bool ForceEntry => Time.timeSinceLevelLoad > lastHistoryEntry + .3f;

    public Vector2Int CurrentDirection => InputHistory[count];

    public void SetDirection(Vector2 dir)
    {
        var norm = new Vector2Int(
            Mathf.RoundToInt(dir.x),
            Mathf.RoundToInt(dir.y)
            );
        if (ForceNeutral && norm.x != 0 && norm.x == -CurrentDirection.x)
        {
            norm = new Vector2Int(0, norm.y);
            dir.x = 0;
        }
        if (norm != CurrentDirection || ForceEntry)
        {
            count++;
            count %= 10;
            InputHistory[count] = norm;
            lastHistoryEntry = Time.timeSinceLevelLoad;
        }
        Horizontal = dir.x;
        Vertical = dir.y;
    }

    public void SetSecondDirection(Vector2 dir)
    {
        SecondDirection = dir;
    }

    public List<Vector2Int> DirectionHistory(int inputs)
    {
        inputs = inputs > 10 ? 10 : inputs;
        var list = new List<Vector2Int>();
        for (int i = 0; i < inputs; i++)
        {
            int wnat = count - inputs + i + 1;
            wnat = (wnat + 10) % 10;

            list.Add(InputHistory[wnat]);
        }
        return list;
    }

    void ClearHistory()
    {
        InputHistory[0] = Vector2Int.zero;
        InputHistory[1] = Vector2Int.zero;
        InputHistory[2] = Vector2Int.zero;
        InputHistory[3] = Vector2Int.zero;
        count = 4;
    }

    public int DP()
    {
        if (MatchSequence(new Vector2Int[]
                {
                Vector2Int.left,
                Vector2Int.down,
                new Vector2Int(-1, -1),
                }, 1))
                return -1;
        
        if (MatchSequence(new Vector2Int[]
                {
                Vector2Int.right,
                Vector2Int.down,
                new Vector2Int(1, -1),
                }, 1))
            return 1;
        return 0;

    }

    int HalfRoll()
    {
        if (CurrentDirection == Vector2.right)
            return HalfRoll(1) ? 1 : 0;
        if (CurrentDirection == Vector2.left)
            return HalfRoll(-1) ? -1 : 0;
        return 0;
    }

    bool HalfRoll(int direction)
    {
        if (direction != CurrentDirection.x)
            return false;
        int sequence = 0;
        foreach (var input in DirectionHistory(4))
        {
            if (sequence == 0 && input.x == -direction)
                sequence++;
            else if (sequence == 1 && input.y == -1)
                sequence++;
            if (sequence == 2 && input.x == direction)
                return true;
        }
        return false;
    }

    public bool MatchSequence(Vector2Int[] inputs, int allowedMissInputs = 1)
    {
        int matches = 0;
        var history = DirectionHistory(inputs.Length + allowedMissInputs);
        if (inputs[inputs.Length - 1] != history[history.Count - 1])
            return false;
        foreach (Vector2Int input in history)
        {
            Vector2Int fuck = inputs[matches];
            if(input == fuck)
            {
                matches++;
                if (matches == inputs.Length)
                    return true;
            }
        }
        return false;
    }

    public bool Charge(Vector2Int Direction)
    {
        var history = DirectionHistory(4);
        if (Direction.x == 0)
        {
            return history[0].y == Direction.y
                && history[1].y == Direction.y
                && history[2].y == Direction.y
                && history[3].y == Direction.y;
        }
        return history[0].x == Direction.x
                && history[1].x == Direction.x
                && history[2].x == Direction.x
                && history[3].x == Direction.x;
    }
}
