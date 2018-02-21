using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Pagination.Build.Actions {
    class Bump : BuildAction {
        public override void Work() {
            var version = "";
            var assemblyFiles = new[] { "AssemblyVersion" };
            foreach (var assemblyFile in assemblyFiles) {
                Log("Bumping " + assemblyFile + "...");

                var path = Path.Combine(SolutionDirectory, assemblyFile + ".cs");
                Log("Path: " + path);

                var text = File.ReadAllText(path);
                var regex = new Regex(@"\d+(\.\d+)*");
                var match = regex.Match(text);

                var vers = match.Value;
                var versParts = vers.Split('.').Select(versPart => int.Parse(versPart)).ToArray();
                versParts[3] += 1;

                vers = string.Join(".", versParts);
                text = text.Replace(match.Value, vers);
                File.WriteAllText(path, text);

                version = vers;
            }

            var nuspecFiles = Directory.GetFiles(SolutionDirectory, "*.nuspec", SearchOption.TopDirectoryOnly);
            foreach (var nuspecFile in nuspecFiles) {
                Log("Bumping " + nuspecFile + "...");
                Log("Path: " + nuspecFile);

                var xdoc = XDocument.Load(nuspecFile);
                var xmln = "http://schemas.microsoft.com/packaging/2010/07/nuspec.xsd";
                var vers = xdoc.Descendants(XName.Get("version", xmln)).Single();
                vers.Value = version + (Stage == null ? "" : ("-" + Stage));
                xdoc.Save(nuspecFile);
            }
        }
    }
}
