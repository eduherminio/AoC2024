﻿namespace AoC_2024;

public class Day_01 : BaseDay
{
    private readonly (List<int> L1, List<int> L2) _input;

    public Day_01()
    {
        _input = ParseInputAsIntListList();
    }

    public override ValueTask<string> Solve_1()
    {
        // LINQ:
        // var result = _input.L1
        //    .Zip(_input.L2, (l1, l2) => Math.Abs(l1 - l2))
        //    .Sum()
        //    .ToString();

        var result = 0;

        for (int i = 0; i < _input.L1.Count; ++i)
        {
            result += Math.Abs(_input.L1[i] - _input.L2[i]);
        }

        return new(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var result = 0;
        var l1Index = 0;
        var l2Index = 0;

        while (l1Index != _input.L1.Count - 1)
        {
            var n = _input.L1[l1Index];
            var l1Counter = 1;
            var l2Counter = 0;

            for (int j = l1Index + 1; j < _input.L1.Count; ++j)
            {
                if (n == _input.L1[j])
                {
                    ++l1Counter;
                }
                else
                {
                    l1Index = j;
                    break;
                }
            }

            for (int k = l2Index; k < _input.L2.Count; ++k)
            {
                if (n == _input.L2[k])
                {
                    ++l2Counter;
                }
                else if (n < _input.L2[k])  // We don't stop the loop until the L2 element is bigger then the L1 one we're looking for
                {
                    l2Index = k;
                    break;
                }
            }

            result += n * l1Counter * l2Counter;
        }

        return new(result.ToString());
    }

    private (List<int> L1, List<int> L2) ParseInputAsIntListList()
    {
        var file = new ParsedFile(InputFilePath);
        var l1 = new List<int>(file.Count);
        var l2 = new List<int>(file.Count);

        while (!file.Empty)
        {
            var line = file.NextLine();

            l1.Add(line.NextElement<int>());
            l2.Add(line.NextElement<int>());
        }

        return (l1.Order().ToList(), l2.Order().ToList());
    }
}
