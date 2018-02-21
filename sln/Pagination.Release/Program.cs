using System;
using System.Linq;

namespace Pagination {
    using ReleaseActions;

    class Program {
        static string Arg(string[] args, string name) {
            return args?.FirstOrDefault(arg => arg.StartsWith("-" + name + "="))?.Split('=')[1];
        }

        static void Main(string[] args) {
            var debug = Arg(args, "Debug") == "yes";
            var stage = Arg(args, "Stage");
            var solutionDirectory = Arg(args, "SolutionDirectory");
            var actions = new ReleaseAction[] {
                new Bump(),
                new Build(),
                new Pack()
            };

            try {
                foreach (var action in actions) {
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
