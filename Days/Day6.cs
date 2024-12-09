using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using App;
namespace Days;

public class Day6 : ISolve
{
    const string TEMP_EXCEPTION_REP = "Repetitive_order";
    const string TEMP_EXCEPTION_NO_GUARD = "No Gaurd Position";
    const string TEMP_EXCEPTION_GUARD_BLOCKED = "blocked";
    public string SolvePartOne(string[] input)
    {
        var array = input.Select(x => x.ToArray()).ToArray();
        var moves = Play(array);
        return $"{moves}";
    }

    public string SolvePartTwo(string[] input)
    {
        var array = input.Select(x => x.ToArray()).ToArray();
        var goodCoordinates = FindGoodPotentialObstacles(array);
        var count = 0;
        foreach (var good in goodCoordinates)
        {
            var map = input.Select(x => x.ToArray()).ToArray();
            var newMap = AddObstacleToMap(map, good);
            if (ObstacleCauseLoops(newMap, good))
            {
                count++;
            }
        }
        return $"{count}";
    }

    private List<(int, int)> FindGoodPotentialObstacles(char[][] map)
    {
        var gaurdPosition = FindGaurd(map);
        ExitMap(map, gaurdPosition);
        var listVisitedCordinates = new List<(int, int)>();
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == 'x')
                {
                    listVisitedCordinates.Add((i, j));
                }
            }
        }
        return listVisitedCordinates;
    }

    private char[][] AddObstacleToMap(char[][] input, (int, int) coordinate)
    {
        var map = input.Select(x => x.ToArray()).ToArray();
        map[coordinate.Item1][coordinate.Item2] = '#';
        return map;
    }

    private bool ObstacleCauseLoops(char[][] input, (int, int) coordinate)
    {
        var map = AddObstacleToMap(input, coordinate);
        try
        {
            Play(map, true);
        }
        catch (Exception ex)
        {
            if (ex.Message == TEMP_EXCEPTION_REP)
            {
                return true;
            }

        }
        return false;
    }

    private int Play(char[][] map, bool print = false)
    {
        var GaurdPosition = FindGaurd(map);
        var steps = ExitMap(map, GaurdPosition);
        if (print)
        {
            using StreamWriter outputFile = new StreamWriter(Path.Combine("c:\\temp", "log.txt"));
            var sb = new StringBuilder();
            foreach (char[] line in map)
            {
                sb.Append(line);
                outputFile.WriteLine(sb.ToString());
                sb.Clear();
            }
        }
        return steps;
    }

    private int ExitMap(char[][] map, (int, int) gaurdPosition)
    {
        var visitedCoordinates = new Dictionary<(int, int), int>();
        while (!GaurdLeft(map, gaurdPosition))
        {
            var newposition = MoveGaurd(map, gaurdPosition);
            if (visitedCoordinates.TryGetValue(gaurdPosition, out _))
            {
                visitedCoordinates[gaurdPosition]++;
            
                if (visitedCoordinates[gaurdPosition] > 4)
                {
                    throw new Exception(TEMP_EXCEPTION_REP);
                }
            }
            else
            {
                visitedCoordinates[gaurdPosition] = 1; // Initialize with 1 if the key does not exist
            }
            gaurdPosition = newposition;

        }
        return map.Sum(x => x.Count(y => y == 'x'));
    }


    private (int, int) MoveGaurd(char[][] map, (int, int) gaurdPosition)
    {
        var nextPosition = GetNextMove(map, gaurdPosition);
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
    private (int, int) GetNextMove(char[][] map, (int, int) gaurdPosition)
    {
        var guard = map[gaurdPosition.Item1][gaurdPosition.Item2];
        var nextMove = guard switch
        {
            'v' => (gaurdPosition.Item1 + 1, gaurdPosition.Item2),
            '>' => (gaurdPosition.Item1, gaurdPosition.Item2 + 1),
            '<' => (gaurdPosition.Item1, gaurdPosition.Item2 - 1),
            '^' => (gaurdPosition.Item1 - 1, gaurdPosition.Item2),
            _ => throw new ArgumentException(TEMP_EXCEPTION_NO_GUARD),
        };
        if (map[nextMove.Item1][nextMove.Item2] == '#')
        {
            var newGuardIcon = Rotate90(map, gaurdPosition);
            map[gaurdPosition.Item1][gaurdPosition.Item2] = newGuardIcon;
            return GetNextMove(map, gaurdPosition);
        }
        return nextMove;
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
                throw new ArgumentException(TEMP_EXCEPTION_NO_GUARD);
        }
    }

    private static bool GaurdLeft(char[][] map, (int, int) gaurdPosition)
    {
        var d = map[gaurdPosition.Item1][gaurdPosition.Item2];
        var isOut = false;
        switch (d)
        {
            case 'v':
                isOut = IsOut(map, gaurdPosition.Item1 + 1, gaurdPosition.Item2);
                break;
            case '>':
                isOut = IsOut(map, gaurdPosition.Item1, gaurdPosition.Item2 + 1);
                break;
            case '<':
                isOut = IsOut(map, gaurdPosition.Item1, gaurdPosition.Item2 - 1);
                break;
            case '^':
                isOut = IsOut(map, gaurdPosition.Item1 - 1, gaurdPosition.Item2);
                break;
            default:
                break;
        }
        if (isOut)
        {
            map[gaurdPosition.Item1][gaurdPosition.Item2] = 'x';
        }
        return isOut;
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