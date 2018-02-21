﻿using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Pagination.ReleaseActions {
    class Bump : ReleaseAction {
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

            Context.Version = version;
            Context.VersionStage = version + (string.IsNullOrWhiteSpace(Stage) 
                ? "" 
                : ("-" + Stage));

            var nuspecFiles = Directory.GetFiles(SolutionDirectory, "*.nuspec", SearchOption.TopDirectoryOnly);
            foreach (var nuspecFile in nuspecFiles) {
                Log("Bumping " + nuspecFile + "...");
                Log("Path: " + nuspecFile);

                var xdoc = XDocument.Load(nuspecFile);
                var xmln = "http://schemas.microsoft.com/packaging/2010/07/nuspec.xsd";
                var vers = xdoc.Descendants(XName.Get("version", xmln)).Single();
                vers.Value = Context.VersionStage;

                var deps = xdoc.Descendants(XName.Get("dependency", xmln)).Where(dep => dep.Attribute("id").Value.StartsWith("Pagination"));
                foreach (var dep in deps) {
                    dep.Attribute("version").Value = Context.VersionStage;
                }

                xdoc.Save(nuspecFile);
            }
        }
    }
}