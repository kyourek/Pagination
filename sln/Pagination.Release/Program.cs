using System;
using System.Linq;

namespace Pagination {
    using ReleaseActions;

    class Program {
        static string Arg(string[] args, string name) {
            return args?.FirstOrDefault(arg => arg.StartsWith("-" + name + "="))?.Split('=')[1];
        }

        static void Main(string[] args) {
            try {
                var stage = Arg(args, "Stage");
                var release = Arg(args, "Release") == "yes";
                if (release == false && string.IsNullOrWhiteSpace(stage)) {
                    throw new InvalidOperationException("Must either stage or release");
                }

                var solutionDirectory = Arg(args, "SolutionDirectory");
                var context = new ReleaseContext();
                var actions = new ReleaseAction[] {
                    new Bump(),
                    new Build(),
                    new Pack(),
                    new Tag(),
                    new Push()
                };

                foreach (var action in actions) {
                    if (Arg(args, action.GetType().Name) != "no") {
                        action.Context = context;
                        action.Log = s => Console.WriteLine(s);
                        action.Stage = stage;
                        action.SolutionDirectory = solutionDirectory;
                        action.Work();
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }

            if (Arg(args, "Debug") == "yes") {
                Console.ReadLine();
            }
        }
    }
}
