﻿using System;
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
            var context = new ReleaseContext();
            var actions = new ReleaseAction[] {
                new Bump(),
                new Build(),
                new Pack(),
                new Tag()
            };

            try {
                foreach (var action in actions) {
                    action.Context = context;
                    action.Log = s => Console.WriteLine((s ?? "").TrimEnd('\r', '\n'));
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
