// 
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
// 

// ReSharper disable once CheckNamespace

namespace SniffCore;

/// <summary>
///     Contains the process execution result data used by
///     <see cref="ProcessHandler.ExecuteSilentlyAndWait(string, string)" />.
/// </summary>
public sealed class ProcessResult
{
    internal ProcessResult(int exitCode, string output)
    {
        ExitCode = exitCode;
        Output = output;
    }

    /// <summary>
    ///     Gets the process exit code.
    /// </summary>
    public int ExitCode { get; }

    /// <summary>
    ///     Gets the output the process created at execution time.
    /// </summary>
    public string Output { get; }
}