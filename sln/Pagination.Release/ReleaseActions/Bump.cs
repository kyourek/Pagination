using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Pagination.ReleaseActions {
    class Bump : ReleaseAction {
        public override void Work() {
            foreach (var assemblyFile in new[] { "AssemblyVersion" }) {
                Log("Bumping " + assemblyFile + "...");

                var path = Path.Combine(SolutionDirectory, assemblyFile + ".cs");
                Log("Path: " + path);

                var text = File.ReadAllText(path);
                var regex = new Regex(@"\d+(\.\d+)*");
                var match = regex.Match(text);

                var relVersion = ReleaseVersion.ParseFullVersion(match.Value, Stage);
                var nextVersion = relVersion.NextRevision();

                File.WriteAllText(path, text.Replace(match.Value, nextVersion.FullVersion));

                Context.Version = nextVersion;
            }

            var nuspecFiles = Directory.GetFiles(SolutionDirectory, "*.nuspec", SearchOption.TopDirectoryOnly);
            foreach (var nuspecFile in nuspecFiles) {
                Log("Bumping " + nuspecFile + "...");
                Log("Path: " + nuspecFile);

                var xdoc = XDocument.Load(nuspecFile);
                var xmln = "http://schemas.microsoft.com/packaging/2010/07/nuspec.xsd";
                var vers = xdoc.Descendants(XName.Get("version", xmln)).Single();
                vers.Value = Context.Version.StagedVersion;

                var deps = xdoc.Descendants(XName.Get("dependency", xmln)).Where(dep => dep.Attribute("id").Value.StartsWith("Pagination"));
                foreach (var dep in deps) {
                    dep.Attribute("version").Value = Context.Version.StagedVersion;
                }

                xdoc.Save(nuspecFile);
            }
        }
    }
}
