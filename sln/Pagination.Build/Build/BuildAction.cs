using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pagination.Build {
    abstract class BuildAction {
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

        public string Stage { get; set; }
        public Action<string> Log { get; set; }

        public abstract void Work();
    }
}
