
using System;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using App;
namespace Days;

public class Day8 : ISolve
{
    Dictionary<char, List<(int, int)>> _availbleFrequencies = new Dictionary<char, List<(int, int)>>();
    HashSet<(int, int)> _uniquePairs = new();

    public string SolvePartOne(string[] input)
    {
        _availbleFrequencies.Clear();
        _uniquePairs.Clear();
        var array = input.Select(x => x.ToArray()).ToArray();

        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array[i].Length; j++)
            {
                var mathedFrequency = Regex.Match(array[i][j].ToString(), @"\w");

                if (mathedFrequency.Success)
                {
                    AddFreqLocation(array[i][j], (i, j));
                }
            }
        }
        FindPairsAndAddAntinodes();

        // remove margin
        // lenght of b
        var res = this._uniquePairs.Where(x => x.Item1 >= 0 && x.Item2 >= 0 && x.Item1 < input.Length && x.Item2 < input[0].Length).Count();
        return $"{res}";
    }


    public string SolvePartTwo(string[] input)
    {
        _availbleFrequencies.Clear();
        _uniquePairs.Clear();
        var array = input.Select(x => x.ToArray()).ToArray();

        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array[i].Length; j++)
            {
                var mathedFrequency = Regex.Match(array[i][j].ToString(), @"\w");

                if (mathedFrequency.Success)
                {
                    AddFreqLocation(array[i][j], (i, j));
                }
            }
        }

        var xLimit = input[0].Length;
        var yLimit = input.Length;

        FindPairsAndAddAntinodesExtended(xLimit, yLimit);

        // remove margin
        // lenght of b
        var res = this._uniquePairs.Where(x => x.Item1 >= 0 && x.Item2 >= 0 && x.Item1 < xLimit && x.Item2 < yLimit).Count();
        var print = false;
        if (print)
        {
            PrintLog(array);
        }
        return $"{res}";
    }

    private void PrintLog(char[][] array)
    {
        using StreamWriter outputFile = new StreamWriter(Path.Combine("c:\\temp", "log.txt"));
        var sb = new StringBuilder();
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array[i].Length; j++)
            {

                if (_uniquePairs.Contains((i, j)))
                {
                    sb.Append('#');
                }
                else
                {
                    sb.Append('.');
                }

            }
            outputFile.WriteLine(sb.ToString());
            sb.Clear();
        }
    }

    private void FindPairsAndAddAntinodes()
    {
        foreach (KeyValuePair<char, List<(int, int)>> entry in _availbleFrequencies)
        {
            var coordiantes = entry.Value;
            for (int i = 0; i < coordiantes.Count; i++)
            {
                for (int j = 0; j < coordiantes.Count; j++)
                {
                    if (i == j) continue;
                    var a = coordiantes[i];
                    var b = coordiantes[j];
                    var antinode = FindAlineVector(a, b);
                    _uniquePairs.Add((b.Item1 + antinode.Item1, b.Item2 + antinode.Item2));
                    _uniquePairs.Add((a.Item1 - antinode.Item1, a.Item2 - antinode.Item2));
                }
            }
        }
    }
    private void FindPairsAndAddAntinodesExtended(int xLimit, int yLimit)
    {
        foreach (KeyValuePair<char, List<(int, int)>> entry in _availbleFrequencies)
        {
            var coordiantes = entry.Value;
            for (int i = 0; i < coordiantes.Count; i++)
            {
                for (int j = 0; j < coordiantes.Count; j++)
                {
                    if (i == j) continue;
                    var a = coordiantes[i];
                    var b = coordiantes[j];
                    var antinode = FindAlineVector(a, b);
                    _uniquePairs.Add((b.Item1 + antinode.Item1, b.Item2 + antinode.Item2));
                    _uniquePairs.Add((a.Item1 - antinode.Item1, a.Item2 - antinode.Item2));
                    var multiplier = 1;
                    while (multiplier < xLimit && multiplier < yLimit)
                    {
                        multiplier++;
                        var extendedAntiNode = FindAlineVector(a, b, multiplier);
                        _uniquePairs.Add((b.Item1 + extendedAntiNode.Item1, b.Item2 + extendedAntiNode.Item2));
                        _uniquePairs.Add((a.Item1 - extendedAntiNode.Item1, a.Item2 - extendedAntiNode.Item2));
                    }

                }
                // plus the antenna itself
                if (coordiantes.Count > 1)
                {
                    this._uniquePairs.Add(coordiantes[i]);
                }
            }
        }
    }
    int MultiplyWithoutChangingPolarity(int a, int b)
    {
        int result = Math.Abs(a) * Math.Abs(b);
        return a < 0 ? -result : result;
    }
    public (int, int) FindAlineVector((int, int) a, (int, int) b, int multiplier = 1)
    {
        var x = b.Item1 - a.Item1;
        var y = b.Item2 - a.Item2;
        return (MultiplyWithoutChangingPolarity(x, multiplier), MultiplyWithoutChangingPolarity(y, multiplier));
    }
    public void AddFreqLocation(char frequency, (int, int) location)
    {
        if (_availbleFrequencies.ContainsKey(frequency))
        {
            _availbleFrequencies[frequency].Add(location);
        }
        else
        {
            _availbleFrequencies.Add(frequency, new() { location });
        }
    }
}
