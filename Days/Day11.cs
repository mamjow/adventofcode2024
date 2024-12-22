using App;

namespace Days;

public class Day11 : ISolve
{


    public string SolvePartOne(string[] input)
    {
        // Input has only one line
        var listStones = input[0].Split(" ").Select(long.Parse).ToArray();

        for (long i = 0; i < 25; i++)
        {
            listStones = Blink(listStones);
        }
        return $"{listStones.Count()}";
    }


    public string SolvePartTwo(string[] input)
    {
        var listStones = input[0].Split(" ").Select(long.Parse).ToArray();

        var count = 0;

        for (long i = 0; i < 75; i++)
        {
            listStones = Blink(listStones);
        }

        return $"{count}";
    }



    /// <summary>
    // If the stone is engraved with the number 0, it is replaced by a stone engraved with the number 1.
    // If the stone is engraved with a number that has an even number of digits, it is replaced by two stones. The left half of the digits are engraved on the new left stone, and the right half of the digits are engraved on the new right stone. (The new numbers don't keep extra leading zeroes: 1000 would become stones 10 and 0.)
    // If none of the other rules apply, the stone is replaced by a new stone; the old stone's number multiplied by 2024 is engraved on the new stone.
    /// </summary>
    public long[] Blink(long[] stones)
    {
        var newListOfStones = new List<long>();

        foreach (var stone in stones)
        {
            if (stone == 0)
            {
                newListOfStones.Add(1);
            }
            else if (stone.ToString().Length % 2 == 0)
            {
                var sizeOfDivision = stone.ToString().Length / 2;
                var left = long.Parse(stone.ToString().Substring(0, sizeOfDivision));
                var right = long.Parse(stone.ToString().Substring(sizeOfDivision));
                newListOfStones.Add(left);
                newListOfStones.Add(right);
            }
            else
            {
                newListOfStones.Add(stone * 2024);
            }
        }

        return newListOfStones.ToArray();
    }
}