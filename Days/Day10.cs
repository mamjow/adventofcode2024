using App;

namespace Days;
public class Day10 : ISolve
{
    List<(int, int)> _trailHeads = new List<(int, int)>();
    char[][] _charArray;

    public string SolvePartOne(string[] input)
    {
        _charArray = input.Select(x => x.ToArray()).ToArray();
        FindTrailHeads(_charArray);
        var score = GetTotalScore();
        return $"{score}";
    }


    public string SolvePartTwo(string[] input)
    {
        _charArray = input.Select(x => x.ToArray()).ToArray();
        FindTrailHeads(_charArray);
        var score = GetTotalScore(false);
        return $"{score}";
    }


    private void FindTrailHeads(char[][] charArray)
    {
        _trailHeads.Clear();
        for (int i = 0; i < charArray.Length; i++)
        {
            for (int j = 0; j < charArray[i].Length; j++)
            {
                if (charArray[i][j] == '0')
                {
                    _trailHeads.Add((i, j));
                }
            }
        }
    }
    private int GetTotalScore(bool distinctDestination = true)
    {
        var totoalScore = 0;
        Dictionary<(int, int), List<(int, int)>> ends = new();
        foreach (var trailHead in _trailHeads)
        {
            ends.Add(trailHead, InvestigateCordinate(trailHead));
        }

        if (distinctDestination)
        {
            return ends.Sum(x => x.Value.Distinct().Count());
        }
        else
        {
            return ends.Sum(x => x.Value.Count());
        }
    }

    private List<(int, int)> InvestigateCordinate((int, int) trailHead)
    {
        var heightChar = _charArray[trailHead.Item1][trailHead.Item2];
        _ = int.TryParse(heightChar.ToString(), out int height);
        List<(int, int)> ends = new List<(int, int)>();

        // if we are at 9 then its a win, return 1;
        if (height == 9)
        {
            ends.Add(trailHead);
        }
        // top
        // check edge
        var nextTop = (trailHead.Item1 - 1, trailHead.Item2);
        if (nextTop.Item1 >= 0 && IsGoodStep(trailHead, nextTop))
        {

            ends.AddRange(InvestigateCordinate(nextTop));
        }

        // right
        var nextRight = (trailHead.Item1, trailHead.Item2 + 1);
        if (nextRight.Item2 < _charArray[nextRight.Item1].Length && IsGoodStep(trailHead, nextRight))
        {
            ends.AddRange(InvestigateCordinate(nextRight));
        }

        // donw
        var nextBot = (trailHead.Item1 + 1, trailHead.Item2);
        if (nextBot.Item1 < _charArray.Length && IsGoodStep(trailHead, nextBot))
        {

            ends.AddRange(InvestigateCordinate(nextBot));
        }

        // left
        var nextLeft = (trailHead.Item1, trailHead.Item2 - 1);
        if (nextLeft.Item2 >= 0 && IsGoodStep(trailHead, nextLeft))
        {

            ends.AddRange(InvestigateCordinate(nextLeft));
        }
        return ends;
    }

    private bool IsGoodStep((int, int) current, (int, int) next)
    {
        var currentChar = _charArray[current.Item1][current.Item2];
        var nextChar = _charArray[next.Item1][next.Item2];
        _ = int.TryParse(currentChar.ToString(), out int currentHeight);
        _ = int.TryParse(nextChar.ToString(), out int nextHeight);
        return nextHeight - currentHeight == 1;
    }
}