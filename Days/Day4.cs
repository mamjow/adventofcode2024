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

        return "";
    }
    public string SolvePartTwo(string[] input)
    {
        var countOccurance = 0;
        foreach (var inputItem in input)
        {
            var remainingTxt = inputItem;
            while (remainingTxt.Contains("SAMX") || remainingTxt.Contains("XMAS"))
            {

                var firstOccurance = Math.Min(remainingTxt.IndexOf("XMAS"), remainingTxt.IndexOf("SAMX"));
                remainingTxt = remainingTxt.Substring(firstOccurance);
                countOccurance++;
            }

        }
        return $"{countOccurance}";
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

    public int CountDiagonal(string[] lines)
    {
        var completeList = new List<string>();
        // Horitonzal increasing & increasing
        var tempHorizontalList = Enumerable.Repeat(string.Empty, lines[0].Length).ToArray();
        // increasing
        for (int i = 0; i < lines[0].Length; i++)
        {
            bool outOfIndexRangeExThrown = false;

            var readingJ = i;
            var readingI = 0;
            while (!outOfIndexRangeExThrown)
            {
                try
                {
                    tempHorizontalList[i] = tempHorizontalList[i] + lines[readingI][readingJ];
                    readingJ++;
                    readingI++;
                }
                catch (IndexOutOfRangeException)
                {
                    outOfIndexRangeExThrown = true;
                }
            }
        }
        completeList.AddRange(tempHorizontalList);
        // Horitonzal increasing & increasing
        tempHorizontalList = Enumerable.Repeat(string.Empty, lines[0].Length).ToArray();
        // decreasing
        for (int i = lines[0].Length - 1; i >= 0; i--)
        {
            bool outOfIndexRangeExThrown = false;

            var readingJ = i;
            var readingI = lines[0].Length;
            while (!outOfIndexRangeExThrown)
            {
                try
                {
                    tempHorizontalList[i] = tempHorizontalList[i] + lines[readingI][readingJ];
                    readingJ++;
                    readingI--;
                }
                catch (IndexOutOfRangeException)
                {
                    outOfIndexRangeExThrown = true;
                }
            }
        }
        completeList.AddRange(tempHorizontalList);

        // Vertical increasing & dec
        // increasing
        tempHorizontalList = Enumerable.Repeat(string.Empty, lines.Length).ToArray();
        for (int i = 1; i < lines.Length; i++)
        {
            bool outOfIndexRangeExThrown = false;
            var readingJ = 0;
            var readingI = i;
            while (!outOfIndexRangeExThrown)
            {
                try
                {
                    tempHorizontalList[i] = tempHorizontalList[i] + lines[readingI][readingJ];
                    readingJ++;
                    readingI++;
                }
                catch (IndexOutOfRangeException)
                {
                    outOfIndexRangeExThrown = true;
                }
            }
        }

        completeList.AddRange(tempHorizontalList);
        // Horitonzal increasing & increasing
        tempHorizontalList = Enumerable.Repeat(string.Empty, lines[0].Length).ToArray();
        // decreasing
        for (int i = lines[0].Length - 1; i >= 0; i--)
        {
            bool outOfIndexRangeExThrown = false;

            // var readingJ = 0;
            // var readingI = i;
            var readingJ = i;
            var readingI = lines[0].Length;
            while (!outOfIndexRangeExThrown)
            {
                try
                {
                    tempHorizontalList[i] = tempHorizontalList[i] + lines[readingI][readingJ];
                    readingJ++;
                    readingI--;
                }
                catch (IndexOutOfRangeException)
                {
                    outOfIndexRangeExThrown = true;
                }
            }
        }

        return 0;
    }
}