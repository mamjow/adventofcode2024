
using System;
using System.Numerics;
using System.Text.RegularExpressions;
using App;
namespace Days;

public class Day7 : ISolve
{
    private bool _addConcatenation = false;
    public string SolvePartOne(string[] input)
    {
        _addConcatenation = false;
        BigInteger countGoodOnes = 0;
        foreach (var line in input)
        {
            var numberString = line.Split(':')[0];
            var digits = line.Split(':')[1].Trim().Split(' ');
            _ = long.TryParse(numberString, out long number);
            var goodOrder = HavePossibleEquation(number, digits);
            if (goodOrder)
            {
                countGoodOnes += number;
            };
        }
        return $"{countGoodOnes}";
    }

    public string SolvePartTwo(string[] input)
    {
        _addConcatenation = true;
        BigInteger countGoodOnes = 0;
        foreach (var line in input)
        {
            var numberString = line.Split(':')[0];
            var digits = line.Split(':')[1].Trim().Split(' ');
            _ = long.TryParse(numberString, out long number);
            var goodOrder = HavePossibleEquation(number, digits);
            if (goodOrder)
            {
                countGoodOnes += number;
            };
        }
        return $"{countGoodOnes}";
    }
    private bool HavePossibleEquation(long number, string[] digitString)
    {
        var digits = digitString.Select(x =>
        {
            _ = long.TryParse(x, out long digit);
            return digit;
        }).ToList();

        if (digits == null || digits.Count == 0)
        {
            return false;
        }

        return EvaluateCombinations(number, digits, digits[0], 1);
    }


    private bool EvaluateCombinations(long target, List<long> numbers, long currentResult, int index)
    {
        // Base case: if we've used all numbers, check the result
        if (index == numbers.Count)
        {
            return currentResult == target;
        }

        // Try all operator combinations with the next number
        long nextNumber = numbers[index];

        // +
        if (EvaluateCombinations(target, numbers, currentResult + nextNumber, index + 1))
            return true;

        // *
        if (EvaluateCombinations(target, numbers, currentResult * nextNumber, index + 1))
            return true;


        if (_addConcatenation)
        {
            long.TryParse($"{currentResult}{nextNumber}", out long resultConcatenation);
            if (EvaluateCombinations(target, numbers, resultConcatenation, index + 1))
                return true;
        }

        // No combinations found, houdoe...
        return false;
    }
}


