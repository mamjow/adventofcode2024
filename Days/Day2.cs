
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using App;
namespace Days;

public class Day2 : ISolve
{
    public string SolvePartOne(string[] input)
    {

        var reports = new List<bool>();
        foreach (var line in input)
        {
            var numbers = Regex.Matches(line, @"\b\d+\b");
            var parsedNumbers = new List<int>();
            foreach (var item in numbers)
            {
                _ = int.TryParse(item.ToString(), out int num);
                parsedNumbers.Add(num);
            }
            reports.Add(AreLevelsValid(parsedNumbers));
        }
        return reports.Count(x => x == true).ToString();
    }

    public string SolvePartTwo(string[] input)
    {
        var reports = new List<bool>();
        foreach (var line in input)
        {
            var numbers = Regex.Matches(line, @"\b\d+\b");
            var parsedNumbers = new List<int>();
            foreach (var item in numbers)
            {
                _ = int.TryParse(item.ToString(), out int num);
                parsedNumbers.Add(num);
            }
            reports.Add(AreLevelsValid(parsedNumbers, 1));
        }
        return reports.Count(x => x == true).ToString();
    }

    /// <summary>
    // 7 6 4 2 1: Safe without removing any level.
    // 1 2 7 8 9: Unsafe regardless of which level is removed.
    // 9 7 6 2 1: Unsafe regardless of which level is removed.
    // 1 3 2 4 5: Safe by removing the second level, 3.
    // 8 6 4 4 1: Safe by removing the third level, 4.
    // 1 3 6 7 9: Safe without removing any level.
    /// </summary>
    /// <param name="levels"></param>
    /// <param name="AcceptableTolerance"></param>
    /// <returns></returns>
    static bool AreLevelsValid(List<int> levels, int AcceptableTolerance)
    {
        
        if (levels == null || levels.Count < 2)
            return false;

        bool isIncreasing = levels[1] > levels[0];   
        for (int i = 1; i < levels.Count; i++)
        {
            int diff = levels[i] - levels[i - 1];
            if(AcceptableTolerance == 0){
                diff = levels[i] - levels[i - 2];
                isIncreasing =  levels[i] > levels[i - 2];
            }
            
            int diffAbs = Math.Abs(diff);
            bool currentFailed = false;
            // if dif is not between 1 and 3 then soweiso negative
            if (diffAbs < 1 || diffAbs > 3)
            {
                currentFailed = true;
            }

            // if increasing and but somwhere deff is negative then its not safe
            if (isIncreasing && diff < 0)
            {
                currentFailed = true;
            }

            // ir def is postitive
            if (!isIncreasing && diff > 0)
            {
                currentFailed = true;
            }

            if (currentFailed)
            {
                --AcceptableTolerance;
                if (AcceptableTolerance == 0)
                {
                    continue;
                }
                return !currentFailed;
            }

        }
        return true;
    }

    static bool AreLevelsValid(List<int> levels)
    {
        if (levels == null || levels.Count < 2)
            return false;

        bool isIncreasing = levels[1] > levels[0];
        for (int i = 1; i < levels.Count; i++)
        {
            int diff = levels[i] - levels[i - 1];
            int diffAbs = Math.Abs(diff);
            // if dif is not between 1 and 3 then soweiso negative
            if (diffAbs < 1 || diffAbs > 3)
                return false;

            // if increasing and but somwhere deff is negative then its not safe
            if (isIncreasing && diff < 0)
                return false;
            // ir def is postitive
            if (!isIncreasing && diff > 0)
                return false;
        }

        return true;
    }
}


