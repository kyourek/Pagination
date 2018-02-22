using System;

namespace Pagination {
    class ReleaseVersion {
        ReleaseVersion() {
        }

        public int Major { get; private set; }
        public int Minor { get; private set; }
        public int Revision { get; private set; }
        public int Build { get; private set; }
        public string Stage { get; private set; }

        string _FullVersion;
        public string FullVersion => _FullVersion ?? (_FullVersion = 
            Revision == 0
                ? $"{Major}.{Minor}.{Build}"
                : $"{Major}.{Minor}.{Build}.{Revision}");

        string _StagedVersion;
        public string StagedVersion => _StagedVersion ?? (_StagedVersion =
            Stage == null
                ? FullVersion
                : $"{Major}.{Minor}.{Build}-{Stage}.{Revision}");

        public static ReleaseVersion ParseFullVersion(string fullVersion, string stage) {
            fullVersion = fullVersion ?? throw new ArgumentNullException(nameof(fullVersion));
            fullVersion = fullVersion.Trim();

            var versionParts = fullVersion.Split('.');
            var versionMajor = versionParts.Length > 0 ? versionParts[0] : "0";
            var versionMinor = versionParts.Length > 1 ? versionParts[1] : "0";
            var versionBuild = versionParts.Length > 2 ? versionParts[2] : "0";
            var versionRevsn = versionParts.Length > 3 ? versionParts[3] : "0";
            var versionStage = (stage ?? "").Trim();

            return new ReleaseVersion {
                Build = int.Parse(versionBuild),
                Major = int.Parse(versionMajor),
                Minor = int.Parse(versionMinor),
                Revision = int.Parse(versionRevsn),
                Stage = stage == "" ? null : stage
            };
        }

        public ReleaseVersion NextRevision() {
            return new ReleaseVersion {
                Build = Build,
                Major = Major,
                Minor = Minor,
                Revision = Revision + 1,
                Stage = Stage
            };
        }
    }
}
