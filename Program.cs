using AdventOfCode;
using App;

var mode = Environment.GetEnvironmentVariable("APP_MODE");
var dayNumber = 4;
var parsed = true;

if (mode != "DEBUG")
{
    Console.WriteLine("Enter a day (1-29): ");
    parsed = int.TryParse(Console.ReadLine(), out dayNumber);
}

if (parsed && dayNumber >= 1 && dayNumber <= 29)
{
    Console.WriteLine($"Presenting day {dayNumber} challenge.");
    ISolve solution = AdventOfCodeChallenge.GetDayInstance(dayNumber);

    if (solution != null)
    {
        AdventOfCodeChallenge.SolveEventChallenge(solution);
    }
    else
    {
        Console.WriteLine("Solution not found for the entered day.");
    }
}
else
{
    Console.WriteLine("Invalid input. Please enter a number between 1 and 29.");
}

Console.ReadLine();