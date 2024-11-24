namespace AoC_2024;

public class Day_01 : BaseDay
{
    private readonly string[] _input;

    public Day_01()
    {
        _input = File.ReadAllLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        return new("1");
    }

    public override ValueTask<string> Solve_2()
    {
        return new("2");
    }
}
