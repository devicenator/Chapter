//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

using System.Diagnostics;

// ReSharper disable MemberCanBePrivate.Global

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
            Restart(process, delay);
        }

        /// <summary>
        ///     Restarts the process by its process ID with a delay.
        /// </summary>
        /// <param name="processId">The ID of the process to restart.</param>
        /// <param name="delay">The delay in seconds when the process has to restart.</param>
        public static void Restart(int processId, int delay)
        {
            var process = Process.GetProcessById(processId);
            Restart(process, delay);
        }

        /// <summary>
        ///     Restarts all processes with a specific name.
        /// </summary>
        /// <param name="processName">The name of the processes to restart.</param>
        /// <param name="delay">The delay in seconds when the process has to restart.</param>
        public static void Restart(string processName, int delay = 2)
        {
            var processes = Process.GetProcessesByName(processName);
            foreach (var process in processes)
                Restart(process, delay);
        }

        /// <summary>
        ///     Restarts the given process with a delay.
        /// </summary>
        /// <param name="process">The process to restart.</param>
        /// <param name="delay">The delay in seconds when the process has to restart.</param>
        public static void Restart(Process process, int delay = 2)
        {
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