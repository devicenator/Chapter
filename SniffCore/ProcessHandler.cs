using System.Diagnostics;

namespace SniffCore
{
    public static class ProcessHandler
    {
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