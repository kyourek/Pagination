using System.Collections.Generic;
using System.IO;

namespace Pagination.ReleaseActions {
    internal class Push : ReleaseAction {
        public IDictionary<string, string> Source { get; set; } = new Dictionary<string, string>();
        public IDictionary<string, string> Project { get; set; } = new Dictionary<string, string>();

        public override void Work() {
            foreach (var project in Project.Values) {
                var packageDir = Path.Combine(SolutionDirectory, project, "bin", "Release");
                var packageFile = Path.Combine(packageDir, $"{project}.{Context.Version.StagedVersion}.nupkg");

                foreach (var source in Source.Values) {
                    Process("nuget", "push", $"\"{packageFile}\"", "-Source", source);
                }
            }
        }
    }
}
