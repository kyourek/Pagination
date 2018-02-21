using System.IO;

namespace Pagination.ReleaseActions {
    class Pack : ReleaseAction {
        public override void Work() {
            foreach (var nuspec in new[] { "Pagination", "Pagination.Web", "Pagination.Web.Mvc" }) {
                Log("Packing " + nuspec + "...");

                var nuspecFile = Path.Combine(SolutionDirectory, nuspec + ".nuspec");
                Log("Path: " + nuspecFile);

                Process("nuget", "pack", $"\"{nuspecFile}\"", "-OutputDirectory", $"\"{SolutionDirectory}\"");
            }
        }
    }
}
