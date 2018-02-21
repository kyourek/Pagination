using System.IO;

namespace Pagination.BuildActions {
    class Pack : BuildAction {
        public override void Work() {
            foreach (var nuspec in new[] { "Pagination", "Pagination.Web", "Pagination.Web.Mvc" }) {
                Log("Packing " + nuspec + "...");

                var nuspecFile = Path.Combine(SolutionDirectory, nuspec + ".nuspec");
                Log("Path: " + nuspecFile);

                Process("nuget", "pack", $"\"{nuspecFile}\"");
            }
        }
    }
}
