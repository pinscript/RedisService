using System;
using System.Diagnostics;
using System.IO;

namespace RedisService
{
    public class RedisServerWrapper
    {
        private readonly string _pathToRedisExecutable;
        private readonly string _pathToRedisDirectory;
        private Process _process;

        public RedisServerWrapper(string pathToRedisExecutable)
        {
            _pathToRedisExecutable = pathToRedisExecutable;
            _pathToRedisDirectory = new FileInfo(pathToRedisExecutable).DirectoryName;         
        }

        /// <summary>
        /// Called when service is started. Invokes redis-server.exe.
        /// </summary>
        public void Start()
        {
            var processStartInfo = new ProcessStartInfo(_pathToRedisExecutable);
            processStartInfo.WorkingDirectory = _pathToRedisDirectory;

            _process = Process.Start(processStartInfo);
        }

        /// <summary>
        /// Called when service is stopped. Kills all redis-serve.exe processes.
        /// </summary>
        public void Stop()
        {
            KillProcess(_process);

            // Try to check for redis-server.exe just in case
            var processes = Process.GetProcessesByName("redis-server.exe");
            if (processes.Length > 0)
            {
                foreach (var process in processes)
                {
                    KillProcess(process);
                }
            }
        }

        /// <summary>
        /// Kills a specific process.
        /// </summary>
        /// <param name="process">Process to kill</param>
        private void KillProcess(Process process)
        {
            if (process != null && !process.HasExited)
            {
                try
                {
                    process.CloseMainWindow();
                }
                catch (Exception)
                {
                    process.Kill();
                }
            }
        }
    }
}
