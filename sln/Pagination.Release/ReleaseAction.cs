using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pagination {
    abstract class ReleaseAction {
        protected void Process(string fileName, params string[] arguments) {
            var args = string.Join(" ", arguments);
            Log(fileName + " " + args);
            using (var process = new Process()) {
                process.StartInfo.FileName = fileName;
                process.StartInfo.Arguments = args;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.ErrorDataReceived += (s, e) => Log(e.Data);
                process.OutputDataReceived += (s, e) => Log(e.Data);
                process.Start();
                process.BeginErrorReadLine();
                process.BeginOutputReadLine();
                process.WaitForExit();

                var exitCode = process.ExitCode;
                if (exitCode != 0) throw new Exception("Process error (exit code '" + exitCode + "')");
            }
        }

        public string SolutionDirectory {
            get {
                if (_SolutionDirectory == null) {
                    _SolutionDirectory = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
                    while (Directory.GetFiles(_SolutionDirectory).Any(file => Path.GetFileName(file) == "Pagination.sln") == false) {
                        _SolutionDirectory = Path.GetDirectoryName(_SolutionDirectory);
                    }
                }
                return _SolutionDirectory;
            }
            set {
                _SolutionDirectory = value;
            }
        }
        string _SolutionDirectory;

        public string SolutionFilePath => Path.Combine(SolutionDirectory, "Pagination.sln");

        public string Stage { get; set; }
        public Action<string> Log { get; set; }

        public abstract void Work();
    }
}
