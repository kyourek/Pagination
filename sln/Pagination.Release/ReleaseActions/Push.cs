using System.IO;

namespace Pagination.ReleaseActions {
    class Push : ReleaseAction {
        public override void Work() {
            foreach (var pkg in new[] { "Pagination", "Pagination.Web", "Pagination.Web.Mvc" }) {
                var pkgFile = Path.Combine(SolutionDirectory, $"{pkg}.{Context.Version.StagedVersion}.nupkg");
                Process("nuget", "push", $"\"{pkgFile}\"", "-Source", "nuget.org");
            }
        }
    }
}
