namespace AoC_2024;

public class Day_01 : BaseDay
{
    private readonly (List<int> L1, List<int> L2) _input;

    public Day_01()
    {
        _input = ParseInputAsIntListList();
    }

    public override ValueTask<string> Solve_1()
    {
        var result = 0;
        var sortedL1 = _input.L1.Order().ToList();
        var sortedL2 = _input.L2.Order().ToList();

        for(int i = 0; i < sortedL1.Count; ++i)
        {
            result += Math.Abs(sortedL1[i] - sortedL2[i]);
        }

        return new(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var result = 0;

        for (int i = 0; i < _input.L1.Count; ++i)
        {
            result += _input.L1[i] *  _input.L2.Count(_input.L1[i].Equals);
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

        return (l1, l2);
    }
}
