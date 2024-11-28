
using System.Text.RegularExpressions;
using App;
namespace Days;

public class Day1 : ISolve
{
    public string SolvePartOne(string[] input)
    {
        var listOfDig = new List<int>();
        foreach (var line in input)
        {
            var numbers = Regex.Matches(line, "[\\d]");
            _ = int.TryParse(numbers[0].Value + numbers[^1].Value, out var dig);
            listOfDig.Add(dig);
        }

        return listOfDig.Sum().ToString();
    }

    public string SolvePartTwo(string[] input)
    {
        var result = new CustomList(input).SumNumbers().ToString();
        return result;
    }
}

public class CustomList : List<string>
{
    public CustomList(IEnumerable<string> collection) : base(collection)
    {
    }

    public int SumNumbers()
    {
        var listOfDig = new List<string>();
        foreach (var line in this)
        {
            var digitMatches = Regex.Matches(line, "(?=(one|two|three|four|five|six|seven|eight|nine|\\d))");
            var listOfMatches = new List<string>();

            foreach (var digit in digitMatches.Cast<Match>())
            {
                listOfMatches.Add(digit.Groups[1].Value);
            }

            listOfDig.Add(NodmilizedNumber(listOfMatches[0]) + NodmilizedNumber(listOfMatches[^1]));
        }

       return listOfDig.Select(x =>
        {
            var yes = int.TryParse(x, out var parsedInt);
            return parsedInt;
        }).ToList().Sum();
    }

    private string NodmilizedNumber(string input)
    {
        return input switch
        {
            "one" => "1",
            "two" => "2",
            "three" => "3",
            "four" => "4",
            "five" => "5",
            "six" => "6",
            "seven" => "7",
            "eight" => "8",
            "nine" => "9",
            _ => input,
        };
    }
}