using App;

namespace AdventOfCode;
public class AdventOfCodeChallenge
{

    public static ISolve GetDayInstance(int dayNumber)
    {
        string className = $"Days.Day{dayNumber}";
        Type dayType = Type.GetType(className);
        if (dayType == null || !typeof(ISolve).IsAssignableFrom(dayType))
            return null;

        return (ISolve)Activator.CreateInstance(dayType);
    }


    public static void SolveEventChallenge(ISolve solution)
    {
        var day = solution.GetType().Name;
        string[] gamesinput;
        try
        {
            gamesinput = System.IO.File.ReadAllLines($"./inputs/{day}.txt");

        }
        catch (System.Exception)
        {
            Console.WriteLine($"\tNo Input is available");
            return;
        }

        Console.WriteLine($"Day: {day[3..]}");
        try
        {
            Console.WriteLine($"\tPart One: {solution.SolvePartOne(gamesinput)}");
            Console.WriteLine($"\tPart Two: {solution.SolvePartTwo(gamesinput)}");
        }
        catch (System.Exception)
        {
            Console.WriteLine($"\tNo Implementation is available");
        }

    }
}