//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.Diagnostics;

namespace SniffCore
{
    /// <summary>
    ///     Brings the possibility to work with processes.
    /// </summary>
    public static class ProcessHandler
    {
        /// <summary>
        ///     Restarts the current process with a delay.
        /// </summary>
        /// <param name="delay">The delay in seconds when the process has to restart.</param>
        public static void Restart(int delay = 2)
        {
            var process = Process.GetCurrentProcess();
            var module = process.MainModule;
            if (module == null)
                return;

            var info = new ProcessStartInfo
            {
                Arguments = $"/C ping 127.0.0.1 -n {delay} && \"{module.FileName}\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            };
            Process.Start(info);
            process.Kill();
        }
    }
}