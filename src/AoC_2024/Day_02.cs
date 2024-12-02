using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AoC_2024;

public class Day_02 : BaseDay
{
    private readonly int[][] _input;

    public Day_02()
    {
        _input = ParseInput().ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        var result = CheckSafety(static (_) => false);

        return new(result.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var result = Solve_2_NoCopy();

        return new(result.ToString());
    }

    private int Solve_2_Original()
    {
        return CheckSafety(SubReport);

        static bool SubReport(Span<int> report)
        {
            for (int n = 0; n < report.Length; ++n)
            {
                var copy = new List<int>(report.Length);
                copy.AddRange(report);
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

    private int Solve_2_NoCopy()
    {
        return CheckSafety(SubReport);

        static bool SubReport(Span<int> report)
        {
            var oldNValue = report[0];
            for (int n = 0; n < report.Length; ++n)
            {
                if (n > 0)
                {
                    report[n - 1] = oldNValue;
                }
                oldNValue = report[n];
                report[n] = int.MinValue;

                bool safe = true;
                bool increasing = report[1] > report[0];

                for (int i = 1; i < report.Length; ++i)
                {
                    var currentLevel = report[i];
                    var previousLevel = report[i - 1];

                    #region The price to pay for not removing the element

                    if (currentLevel == int.MinValue)
                    {
                        continue;
                    }

                    if (previousLevel == int.MinValue)
                    {
                        if (i == 2)
                        {
                            increasing = report[i + 1] > currentLevel;
                        }
                        else if (i == 1)
                        {
                            increasing = report[i + 1] > currentLevel;
                            continue;
                        }

                        previousLevel = report[i - 2];
                    }

                    #endregion

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

    private int CheckSafety(Func<Span<int>, bool> predicate)
    {
        int result = 0;

        foreach (var report in _input)
        {
            bool safe = true;
            bool increasing = report[1] > report[0];

            for (int i = 1; i < report.Length; ++i)
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

    private IEnumerable<int[]> ParseInput()
    {
        var file = new ParsedFile(InputFilePath);

        while (!file.Empty)
        {
            var line = file.NextLine();

            yield return line.ToList<int>().ToArray();
        }
    }
}
