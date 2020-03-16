using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pagination {
    internal abstract class ReleaseAction {
        protected void Process(string fileName, params string[] arguments) {
            var args = string.Join(" ", arguments);
            Log($"{fileName} {args}");

            if (ProcessPath.TryGetValue(fileName, out string processPath)) {
                fileName = processPath;
                Log($"Using {fileName}");
            }

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
                    while (Directory.GetFiles(_SolutionDirectory).Any(file => Path.GetFileName(file) == SolutionFileName) == false) {
                        _SolutionDirectory = Path.GetDirectoryName(_SolutionDirectory);
                    }
                }
                return _SolutionDirectory;
            }
            set => _SolutionDirectory = value;
        }
        private string _SolutionDirectory;

        public string SolutionFileName { get; set; }
        public string SolutionFilePath => Path.Combine(SolutionDirectory, SolutionFileName);
        public string PropertiesFilePath => Path.Combine(SolutionDirectory, "Directory.Build.props");

        public string Stage { get; set; }
        public Action<string> Log { get; set; }

        public ReleaseContext Context {
            get => _Context ?? (_Context = new ReleaseContext());
            set => _Context = value;
        }
        private ReleaseContext _Context;

        public IDictionary<string, string> ProcessPath {
            get => _ProcessPath ?? (_ProcessPath = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase));
            set => _ProcessPath = value;
        }
        private IDictionary<string, string> _ProcessPath;

        public abstract void Work();
    }
}
