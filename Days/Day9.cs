
using System;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using App;
using Microsoft.VisualBasic;
namespace Days;

public class Day9 : ISolve
{
    List<int> _filesBlockSizeList = new List<int>();
    List<int> FreeSpaceBlockSize = new List<int>();
    List<Block> _blocks = new List<Block>();

    public string SolvePartOne(string[] input)
    {
        ReadDiskMap(input[0]);
        WriteBlocks();
        var sortedBlock = SortedBlock();
        var sortedArray = SortedCharArray();
        var checkSum = GetCheckSum(sortedBlock);
        var checkSum1 = GetCheckSum(sortedArray);
        return $"{checkSum} - {checkSum1}";
    }

    private long GetCheckSum(List<Block> sortedBlock)
    {

        return sortedBlock.Select((x, Index) =>
        {
            if (long.TryParse(x.Value, out long value))
            {
                return value * Index;
            }
            return 0;

        }).Sum();
    }
    private long GetCheckSum(List<char> sortedBlock)
    {

        return sortedBlock.Select((x, Index) =>
        {
            if (long.TryParse(x.ToString(), out long value))
            {
                return value * Index;
            }
            return 0;

        }).Sum();
    }

    private List<Block> SortedBlock()
    {
        var tempBlock = new List<Block>();
        tempBlock.AddRange(_blocks);

        for (int i = 0; i < tempBlock.Count; i++)
        {
            //PrintLog(tempBlock.ToArray());
            if (tempBlock[i].Value == ".")
            {
                var lastdigit = FindLastDigitIndex(tempBlock);
                if (lastdigit == -1)
                {
                    break;
                }
                var n = tempBlock[lastdigit];
                tempBlock[i].Value = n.Value;
                tempBlock[lastdigit].Value = ".";
            }
        }
        //PrintLog(tempBlock.ToArray());
        return tempBlock;
    }

    private List<char> SortedCharArray()
    {
        var tempBlock = new List<char>();
        var BlocksCharArray = string.Join("", _blocks.Select(x => x.Value).ToArray()).ToCharArray();
        tempBlock.AddRange(BlocksCharArray);

        for (int i = 0; i < tempBlock.Count; i++)
        {
            //PrintLog(tempBlock.ToArray());
            if (tempBlock[i] == '.')
            {
                var lastdigit = FindLastDigitIndex(tempBlock);
                if (lastdigit == -1)
                {
                    break;
                }
                var n = tempBlock[lastdigit];
                tempBlock[i] = n;
                tempBlock[lastdigit] = '.';
            }
        }
        //PrintLog(tempBlock.ToArray());
        return tempBlock;
    }

    private int FindLastDigitIndex(List<char> input)
    {
        // the space should be last
        // fo its first index should be lower than last digit

        var firstFreeSpace = input.FindIndex(x => x == '.');
        // Iterate backward to find the last digit that occurs after "onlySpaceIndex"
        for (int i = input.Count - 1; i >= 0; i--)
        {
            if (i < firstFreeSpace)
            {
                return -1;
            }

            if (char.IsDigit(input[i]))
            {
                return i;
            }
        }
        return -1; // No digit found
    }
    private int FindLastDigitIndex(List<Block> input)
    {
        // the space should be last
        // fo its first index should be lower than last digit

        var firstFreeSpace = input.FindIndex(x => x.Value == ".");
        // Iterate backward to find the last digit that occurs after "onlySpaceIndex"
        for (int i = input.Count - 1; i >= 0; i--)
        {
            if (i < firstFreeSpace)
            {
                return -1;
            }

            if (!string.IsNullOrEmpty(input[i].Value) && char.IsDigit(input[i].Value[0]))
            {
                return i;
            }
        }
        return -1; // No digit found
    }

    private void WriteBlocks()
    {
        // one might be longer. so much we itterate
        var iterationLimit = Math.Max(_filesBlockSizeList.Count, FreeSpaceBlockSize.Count);
        for (int i = 0; i < iterationLimit; i++)
        {
            if (i < _filesBlockSizeList.Count)
            {
                string digitString = i.ToString();
                // Create unique instances of Blocks
                Block[] fileblock = Enumerable.Range(0, _filesBlockSizeList[i])
                                              .Select(_ => new Block { ID = i, Value = digitString })
                                              .ToArray();
                _blocks.AddRange(fileblock);
            }
            if (i < FreeSpaceBlockSize.Count)
            {
                // Create unique instances of Blocks
                Block[] freeblock = Enumerable.Range(0, FreeSpaceBlockSize[i])
                                              .Select(_ => new Block { Value = "." })
                                              .ToArray();
                _blocks.AddRange(freeblock);
            }
        }
    }

    private void ReadDiskMap(string v)
    {
        var map = v.ToCharArray();
        FreeSpaceBlockSize.Clear();
        _filesBlockSizeList.Clear();
        for (int i = 0; i < map.Length; i++)
        {

            int.TryParse(map[i].ToString(), out int size);
            // is file
            if (i % 2 == 0)
            {
                _filesBlockSizeList.Add(size);
            }
            else
            {
                // free space
                FreeSpaceBlockSize.Add(size);
            }
        }

    }

    static int FindLastDigitIndex(string input)
    {
        for (int i = input.Length - 1; i >= 0; i--)
        {
            if (char.IsDigit(input[i]))
            {
                return i;
            }
        }
        return -1; // No digit found
    }

    public string SolvePartTwo(string[] input)
    {

        // PrintLog(array);
        return $"{0}";
    }

    private void PrintLog(Block[] array)
    {
        using StreamWriter outputFile = new StreamWriter(Path.Combine("c:\\temp", "log.txt"));
        var sb = new StringBuilder();
        var listValues = array.Select(x => x.Value).ToArray();
        var t = sb.Append("d");
        outputFile.WriteLine(string.Join("", listValues));
        // for (int i = 0; i < array.Length; i++)
        // {


        //     sb.Clear();
        // }
    }
}

class Block
{
    public long ID { get; set; }
    public string Value { get; set; }
}