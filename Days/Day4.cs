using System.Text.RegularExpressions;
using App;
namespace Days;

public class Day4 : ISolve
{
    public string SolvePartOne(string[] input)
    {
        var countH = CountHorizontals(input);
        var countV = CountVerticals(input);
        var countD = CountDiagonal(input);
        var neWinput = ReverseYaxis(input);
        var countDasd = CountDiagonal(neWinput);
        return $"{countH + countV + countD + countDasd}";
    }
    public string SolvePartTwo(string[] input)
    {
        var listAllOccurancesOfA = new List<(int, int)>();
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] == 'A')
                {
                    listAllOccurancesOfA.Add((i, j));
                }
            }
        }
        return $"{listAllOccurancesOfA.Where(x => checkIfShapeX(input, x.Item1, x.Item2)).Count()}";
    }

    public int CountHorizontals(string[] lines)
    {

        var countOccurance = 0;
        foreach (var inputItem in lines)
        {
            var remainingTxt = inputItem;
            while (remainingTxt.Contains("SAMX") || remainingTxt.Contains("XMAS"))
            {

                var firstXMAS = remainingTxt.IndexOf("XMAS") < 0 ? int.MaxValue : remainingTxt.IndexOf("XMAS");
                var firstSAMX = remainingTxt.IndexOf("SAMX") < 0 ? int.MaxValue : remainingTxt.IndexOf("SAMX");
                var firstOccurance = Math.Min(firstSAMX, firstXMAS);
                remainingTxt = remainingTxt.Substring(firstOccurance + 1);
                countOccurance++;
            }

        }
        return countOccurance;
    }
    public int CountVerticals(string[] lines)
    {
        string[] newList = Rotate90(lines);
        return CountHorizontals(newList);
    }

    private static string[] Rotate90(string[] lines)
    {
        var newList = Enumerable.Repeat(string.Empty, lines.Length).ToArray();
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                newList[i] = newList[i] + lines[j][i];
            }
        }
        return newList;
    }

    private static string[] ReverseYaxis(string[] lines)
    {
        var newList = Enumerable.Repeat(string.Empty, lines.Length).ToArray();
        for (int i = 0; i < lines.Length; i++)
        {
            char[] charArray = lines[i].ToCharArray();
            Array.Reverse(charArray);
            newList[i] = new string(charArray);
        }
        return newList;
    }

    public int CountDiagonal(string[] lines)
    {
        var completeList = new List<string>();
        // Horitonzal increasing 
        var tempList = Enumerable.Repeat(string.Empty, lines[0].Length).ToArray();
        for (int i = 0; i < lines[0].Length; i++)
        {
            bool outOfIndexRangeExThrown = false;

            var readingJ = i;
            var readingI = 0;
            while (!outOfIndexRangeExThrown)
            {
                try
                {
                    tempList[i] = tempList[i] + lines[readingI][readingJ];
                    readingJ++;
                    readingI++;
                }
                catch (IndexOutOfRangeException)
                {
                    outOfIndexRangeExThrown = true;
                }
            }
        }
        completeList.AddRange(tempList);


        // Vertical increasing 
        // increasing
        tempList = Enumerable.Repeat(string.Empty, lines.Length).ToArray();
        for (int i = 1; i < lines.Length; i++)
        {
            bool outOfIndexRangeExThrown = false;
            var readingJ = 0;
            var readingI = i;
            while (!outOfIndexRangeExThrown)
            {
                try
                {
                    tempList[i] = tempList[i] + lines[readingI][readingJ];
                    readingJ++;
                    readingI++;
                }
                catch (IndexOutOfRangeException)
                {
                    outOfIndexRangeExThrown = true;
                }
            }
        }

        completeList.AddRange(tempList);
        string[] a = completeList.ToArray();
        return CountHorizontals(a);
    }

    public bool checkIfShapeX(string[] line, int i, int j)
    {
        // check all neigheb
        try
        {
            var d1 = $"{line[i - 1][j - 1]}{line[i][j]}{line[i + 1][j + 1]}";
            var d2 = $"{line[i + 1][j - 1]}{line[i][j]}{line[i - 1][j + 1]}";
            return (d1 == "SAM" || d1 == "MAS") && (d2 == "SAM" || d2 == "MAS");
        }
        catch (IndexOutOfRangeException)
        {
            return false;
        }
    }
}