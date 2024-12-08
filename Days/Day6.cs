using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using App;
namespace Days;

public class Day6 : ISolve
{
    public string SolvePartOne(string[] input)
    {
        var array = input.Select(x => x.ToArray()).ToArray();
        var moves = Play(array);
        return $"{moves}";
    }

    public string SolvePartTwo(string[] input)
    {
        throw new NotImplementedException();
    }
    private int Play(char[][] map)
    {
        var GaurdPosition = FindGaurd(map);
        var steps = ExitMap(map, GaurdPosition);

        // using (StreamWriter outputFile = new StreamWriter(Path.Combine("c:\\temp", "log.txt")))
        // {
        //     var sb = new StringBuilder();
        //     foreach (char[] line in map)
        //     {
        //         sb.Append(line );
        //         outputFile.WriteLine(sb.ToString());
        //         sb.Clear();
        //     }
        // }
        return steps;
    }

    private int ExitMap(char[][] map, (int, int) gaurdPosition)
    {
        while (!GaurdLeft(map, gaurdPosition))
        {
            gaurdPosition = MoveGaurd(map, gaurdPosition);
        }
        return map.Sum( x => x.Count(y => y == 'x')) +1;
    }


    private (int, int) MoveGaurd(char[][] map, (int, int) gaurdPosition)
    {
        var nextPosition = GetNextMove(map, gaurdPosition);
        if (map[nextPosition.Item1][nextPosition.Item2] == '#')
        {
            var newGuardIcon = Rotate90(map, gaurdPosition);
            map[gaurdPosition.Item1][gaurdPosition.Item2] = newGuardIcon;
            nextPosition = GetNextMove(map, gaurdPosition);
        }
        map[nextPosition.Item1][nextPosition.Item2] = map[gaurdPosition.Item1][gaurdPosition.Item2];
        map[gaurdPosition.Item1][gaurdPosition.Item2] = 'x';
        return nextPosition;
    }


    /// <summary>
    ///  get next position base on guard position
    /// </summary>
    /// <param name="map"></param>
    /// <param name="gaurdPosition"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private static (int, int) GetNextMove(char[][] map, (int, int) gaurdPosition)
    {
        var guard = map[gaurdPosition.Item1][gaurdPosition.Item2];

        switch (guard)
        {
            case 'v':
                return (gaurdPosition.Item1 + 1, gaurdPosition.Item2);
            case '>':
                return (gaurdPosition.Item1, gaurdPosition.Item2 + 1);
            case '<':
                return (gaurdPosition.Item1, gaurdPosition.Item2 - 1);
            case '^':
                return (gaurdPosition.Item1 - 1, gaurdPosition.Item2);
            default:
                throw new ArgumentException("No Gaurd Position");
        }
    }
    private char Rotate90(char[][] map, (int, int) gaurdPosition)
    {
        var guard = map[gaurdPosition.Item1][gaurdPosition.Item2];
        switch (guard)
        {
            case 'v':
                return '<';
            case '>':
                return 'v';
            case '<':
                return '^';
            case '^':
                return '>';
            default:
                throw new ArgumentException("No Gaurd Position");
        }
    }

    private static bool GaurdLeft(char[][] map, (int, int) gaurdPosition)
    {
        var d = map[gaurdPosition.Item1][gaurdPosition.Item2];
        switch (d)
        {
            case 'v':
                return IsOut(map, gaurdPosition.Item1 + 1, gaurdPosition.Item2);
            case '>':
                return IsOut(map, gaurdPosition.Item1, gaurdPosition.Item2 + 1);
            case '<':
                return IsOut(map, gaurdPosition.Item1, gaurdPosition.Item2 - 1);
            case '^':
                return IsOut(map, gaurdPosition.Item1 - 1, gaurdPosition.Item2);
            default:
                throw new ArgumentException("No Gaurd Position");
        }
    }

    private static bool IsOut(char[][] map, int i, int j)
    {
        try
        {
            _ = map[i][j];
            return false;
        }
        catch (IndexOutOfRangeException)
        {
            return true;
        }
    }

    private static (int, int) FindGaurd(char[][] map)
    {

        var arrowList = new List<char>() { '<', '>', '^', 'v' };
        for (var i = 0; i <= map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                var selectedChar = map[i][j];
                if (arrowList.Contains(selectedChar))
                {
                    return (i, j);
                }
            }


        }

        throw new InvalidDataException("THIS NO GAME!");
    }
}