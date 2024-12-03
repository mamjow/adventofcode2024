using System.Text.RegularExpressions;
using App;
namespace Days;

public class Day3 : ISolve
{
    string regexPattern = @"mul\((\d{1,3}),(\d{1,3})\)";
    public string SolvePartOne(string[] input)
    {
        var parsedNumbers = new List<int>();

        foreach (var line in input)
        {
            var numbers = Regex.Matches(line, regexPattern);

            foreach (Match item in numbers)
            {
                _ = int.TryParse(item.Groups[1].ToString(), out int num1);
                _ = int.TryParse(item.Groups[2].ToString(), out int num2);
                parsedNumbers.Add(num1 * num2);
            }

        }
        return parsedNumbers.Sum().ToString();
    }
    public string SolvePartTwo(string[] input)
    {
        throw new NotImplementedException();
    }
}