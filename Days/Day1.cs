
using System.Text.RegularExpressions;
using App;
namespace Days;

public class Day1 : ISolve
{
    public string SolvePartOne(string[] input)
    {
        var leftList = new List<int>();
        var rightList = new List<int>();
        foreach (var line in input)
        {
            var numbers = Regex.Matches(line, @"\b\d+\b");
            _ = int.TryParse(numbers[0].Value, out var Leftdig);
            _ = int.TryParse(numbers[1].Value, out var rightDig);
            leftList.Add(Leftdig);
            rightList.Add(rightDig);
        }
        leftList.Sort();
        rightList.Sort();
        var distanceList = new List<int>();
        for (int i = 0; i < leftList.Count; i++)
        {
            distanceList.Add(Math.Abs(leftList[i] - rightList[i]));
        }
        return distanceList.Sum().ToString();
    }

    public string SolvePartTwo(string[] input)
    {

        var leftList = new List<int>();
        var rightList = new List<int>();
        foreach (var line in input)
        {
            var numbers = Regex.Matches(line, @"\b\d+\b");
            _ = int.TryParse(numbers[0].Value, out var Leftdig);
            _ = int.TryParse(numbers[1].Value, out var rightDig);
            leftList.Add(Leftdig);
            rightList.Add(rightDig);
        }
        leftList.Sort();
        rightList.Sort();

        var distanceList = new List<int>();
        for (int i = 0; i < leftList.Count; i++)
        {
            var countOccurance = rightList.Count(x => x == leftList[i]);
            distanceList.Add(leftList[i] * countOccurance);
        }
        return distanceList.Sum().ToString();
    }
}


