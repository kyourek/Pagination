using System.Xml.Linq;

namespace Pagination.ReleaseActions {
    internal class Bump : ReleaseAction {
        public override void Work() {
            var propsPath = PropertiesFilePath;
            var propsXDoc = XDocument.Load(propsPath);
            var propertyGroup = propsXDoc.Root.Element("PropertyGroup");

            var fileVersion = propertyGroup.Element("FileVersion").Value;
            var fullVersion = ReleaseVersion.ParseFullVersion(fileVersion, Stage);
            var nextVersion = fullVersion.NextRevision();

            void set(string name, string value) => propertyGroup.Element(name).Value = value;

            set("VersionPrefix", nextVersion.VersionPrefix);
            set("VersionSuffix", nextVersion.VersionSuffix);
            set("AssemblyVersion", nextVersion.AssemblyVersion);
            set("InformationalVersion", nextVersion.InformationalVersion);
            set("FileVersion", nextVersion.FileVersion);
            set("PackageVersion", nextVersion.PackageVersion);
            propsXDoc.Save(propsPath);

            Context.Version = nextVersion;
        }
    }
}
