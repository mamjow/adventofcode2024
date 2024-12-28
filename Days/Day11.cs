using App;

namespace Days;

public class Day11 : ISolve
{

    public string SolvePartOne(string[] input)
    {
        //Input has only one line
        var listStones = input[0].Split(" ").Select(long.Parse).ToArray();
        var dicStones = new Dictionary<long, long>();
        foreach (var stone in listStones)
        {
            if (dicStones.ContainsKey(stone))
            {
                dicStones[stone] += 1;
            }
            else
            {
                dicStones[stone] = 1;
            }
        } 

        for (long j = 0; j < 25; j++)
        {
            dicStones = Blink(dicStones);
        }
        var count = dicStones.Values.Sum();;

        return $"{count}";
    }


    public string SolvePartTwo(string[] input)
    {

        //Input has only one line
        var listStones = input[0].Split(" ").Select(long.Parse).ToArray();
        var dicStones = new Dictionary<long, long>();
        foreach (var stone in listStones)
        {
            if (dicStones.ContainsKey(stone))
            {
                dicStones[stone] += 1;
            }
            else
            {
                dicStones[stone] = 1;
            }
        } 

        for (long j = 0; j < 75; j++)
        {
            dicStones = Blink(dicStones);
        }
        var count = dicStones.Values.Sum();;

        return $"{count}";
    }

    public Dictionary<long, long> Blink(Dictionary<long, long> stones)
    {
        var newStones = new Dictionary<long, long>();

        foreach (var stone in stones)
        {
            if (stone.Key == 0)
            {
                if (newStones.ContainsKey(1))
                {
                    newStones[1] += stone.Value;
                }
                else
                {
                    newStones[1] = stone.Value;
                }
            }
            else if (stone.Key.ToString().Length % 2 == 0)
            {
                var sizeOfDivision = stone.Key.ToString().Length / 2;
                var left = long.Parse(stone.Key.ToString().Substring(0, sizeOfDivision));
                var right = long.Parse(stone.Key.ToString().Substring(sizeOfDivision));

                if (newStones.ContainsKey(left))
                {
                    newStones[left] += stone.Value;
                }
                else
                {
                    newStones[left] = stone.Value;
                }

                if (newStones.ContainsKey(right))
                {
                    newStones[right] += stone.Value;
                }
                else
                {
                    newStones[right] = stone.Value;
                }
            }
            else
            {
                var newStone = stone.Key * 2024;
                if (newStones.ContainsKey(newStone))
                {
                    newStones[newStone] += stone.Value;
                }
                else
                {
                    newStones[newStone] = stone.Value;
                }
            }
        }

        return newStones;
    }

}