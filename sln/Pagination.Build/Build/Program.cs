using System;
using System.Linq;

namespace Pagination.Build {
    using Actions;

    class Program {
        static string Arg(string[] args, string name) {
            return args?.FirstOrDefault(arg => arg.StartsWith("-" + name + "="))?.Split('=')[1];
        }

        static void Main(string[] args) {
            var debug = Arg(args, "Debug") == "yes";
            var stage = Arg(args, "Stage");
            var solutionDirectory = Arg(args, "SolutionDirectory");
            try {
                foreach (var action in new[] { new Bump() }) {
                    action.Log = s => Console.WriteLine(s);
                    action.Stage = stage;
                    action.SolutionDirectory = solutionDirectory;
                    action.Work();
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }

            if (debug) {
                Console.ReadLine();
            }
        }
    }
}
