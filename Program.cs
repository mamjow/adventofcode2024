using AdventOfCode;
using App;

var mode = Environment.GetEnvironmentVariable("APP_MODE");
int dayNumber = 10;
var parsed = true;
var input = dayNumber.ToString();

while (true)
{
    if (mode != "DEBUG")
    {
        Console.WriteLine("Enter a day (1-25) to play, or 'x' to exit: ");
        parsed = int.TryParse(Console.ReadLine(), out dayNumber);
    }

    // Exit condition
    if (input?.ToLower() == "x")
    {
        Console.WriteLine("Exiting the program. Goodbye!");
        break;
    }

    parsed = int.TryParse(input, out dayNumber);

    if (parsed && dayNumber >= 1 && dayNumber <= 25)
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
        Console.WriteLine("Invalid input. Please enter a number between 1 and 29, or 'x' to exit.");
    }
    
    if (mode == "DEBUG")
    {
        break;
    }
}

// Console.ReadLine();