namespace AoC_2024;

public class Day_02 : BaseDay
{
    private readonly List<List<int>> _input;

    public Day_02()
    {
        _input = ParseInput().ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        var result = CheckSafety(static (_) => false);

        return new(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var result = CheckSafety(SubReport);

        return new(result.ToString());

        static bool SubReport(List<int> report)
        {
            for (int n = 0; n < report.Count; ++n)
            {
                var copy = report.ToList();
                copy.RemoveAt(n);

                bool safe = true;
                bool increasing = copy[1] > copy[0];

                for (int i = 1; i < copy.Count; ++i)
                {
                    var currentLevel = copy[i];
                    var previousLevel = copy[i - 1];

                    var delta = currentLevel - previousLevel;

                    if (increasing)
                    {
                        if (delta > 0 && delta <= 3)
                        {
                            continue;
                        }

                        safe = false;
                        break;
                    }
                    else
                    {
                        if (delta < 0 && delta >= -3)
                        {
                            continue;
                        }

                        safe = false;
                        break;
                    }
                }

                if (safe)
                {
                    return true;
                }
            }

            return false;
        }
    }

    private int CheckSafety(Func<List<int>, bool> predicate)
    {
        int result = 0;

        foreach (var report in _input)
        {
            bool safe = true;
            bool increasing = report[1] > report[0];

            for (int i = 1; i < report.Count; ++i)
            {
                var currentLevel = report[i];
                var previousLevel = report[i - 1];

                var delta = currentLevel - previousLevel;

                if (increasing)
                {
                    if (delta > 0 && delta <= 3)
                    {
                        continue;
                    }

                    safe = predicate(report);
                    break;
                }
                else
                {
                    if (delta < 0 && delta >= -3)
                    {
                        continue;
                    }

                    safe = predicate(report);
                    break;
                }
            }

            if (safe)
            {
                ++result;
            }
        }

        return result;
    }

    private IEnumerable<List<int>> ParseInput()
    {
        var file = new ParsedFile(InputFilePath);

        while (!file.Empty)
        {
            var line = file.NextLine();

            yield return line.ToList<int>();
        }
    }
}
