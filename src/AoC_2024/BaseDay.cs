﻿using System.Reflection;

namespace AoC_2024;

public abstract class BaseDay : AoCHelper.BaseDay
{
    protected override string InputFileDirPath =>
        Path.Combine(
            Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!,
            base.InputFileDirPath);
}
