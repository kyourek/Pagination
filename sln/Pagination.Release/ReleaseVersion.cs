using System;

namespace Pagination {
    internal class ReleaseVersion {
        private ReleaseVersion() {
        }

        public int Major { get; private set; }
        public int Minor { get; private set; }
        public int Build { get; private set; }
        public string Stage { get; private set; }
        public int? Revision { get; private set; }

        public string FullVersion => (Revision ?? 0) == 0
            ? $"{Major}.{Minor}.{Build}"
            : $"{Major}.{Minor}.{Build}.{Revision}";

        public string StagedVersion => Stage == null
            ? $"{Major}.{Minor}.{Build}"
            : $"{Major}.{Minor}.{Build}-{Stage}{((Revision ?? 0) == 0 ? "" : $".{Revision}")}";

        public string VersionPrefix => $"{Major}.{Minor}.{Build}";
        public string VersionSuffix => Stage == null
            ? ""
            : $"{Stage}{((Revision ?? 0) == 0 ? "" : $".{Revision}")}";

        public string AssemblyVersion => VersionPrefix;
        public string InformationalVersion => StagedVersion;
        public string FileVersion => FullVersion;
        public string PackageVersion => StagedVersion;

        public static ReleaseVersion ParseFullVersion(string fullVersion, string stage) {
            fullVersion = fullVersion ?? throw new ArgumentNullException(nameof(fullVersion));
            fullVersion = fullVersion.Trim();

            var versionParts = fullVersion.Split('.');
            var versionMajor = versionParts.Length > 0 ? versionParts[0] : "0";
            var versionMinor = versionParts.Length > 1 ? versionParts[1] : "0";
            var versionBuild = versionParts.Length > 2 ? versionParts[2] : "0";
            var versionRevsn = versionParts.Length > 3 ? versionParts[3] : "";
            var versionStage = (stage ?? "").Trim();

            return new ReleaseVersion {
                Build = int.Parse(versionBuild),
                Major = int.Parse(versionMajor),
                Minor = int.Parse(versionMinor),
                Revision = versionRevsn == "" ? default(int?) : int.Parse(versionRevsn),
                Stage = versionStage == "" ? null : versionStage
            };
        }

        public ReleaseVersion NextRevision() =>
            new ReleaseVersion {
                Build = Build,
                Major = Major,
                Minor = Minor,
                Revision = (Revision ?? 0) + 1,
                Stage = Stage
            };
    }
}
