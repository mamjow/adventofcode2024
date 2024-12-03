using System.Text.RegularExpressions;
using App;
namespace Days;

public class Day3 : ISolve
{
    string mulPattern = @"mul\((\d{1,3}),(\d{1,3})\)";
    string controlPattern = @"do\(\)|don't\(\)";
    public string SolvePartOne(string[] input)
    {
        var parsedNumbers = new List<int>();

        foreach (var line in input)
        {
            var numbers = Regex.Matches(line, mulPattern);

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
        var parsedNumbers = new List<int>();
        bool acceptMatches = true;
        foreach (var line in input)
        {
            // Find matches for both mul() and control patterns
            MatchCollection controls = Regex.Matches(line, controlPattern);
            MatchCollection mulMatches = Regex.Matches(line, mulPattern);
            // Process matches
            int lastControlIndex = 0;
            foreach (Match mulMatch in mulMatches)
            {
                // Check if there was a control match before the current mul()
                while (lastControlIndex < controls.Count && controls[lastControlIndex].Index < mulMatch.Index)
                {
                    string controlValue = controls[lastControlIndex].Value;

                    if (controlValue == "do()")
                    {
                        acceptMatches = true;
                    }
                    else if (controlValue == "don't()")
                    {
                        acceptMatches = false;
                    }

                    lastControlIndex++;
                }

                if (acceptMatches)
                {
                    _ = int.TryParse(mulMatch.Groups[1].ToString(), out int num1);
                    _ = int.TryParse(mulMatch.Groups[2].ToString(), out int num2);
                    parsedNumbers.Add(num1 * num2);
                }
            }

        }
        return parsedNumbers.Sum().ToString();
    }
}