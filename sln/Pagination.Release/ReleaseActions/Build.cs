namespace Pagination.ReleaseActions {
    internal class Build : ReleaseAction {
        public override void Work() {
            Process("MSBuild",
                "-restore",
                "-t:Clean",
                "-t:Rebuild",
                "-t:pack",
                "-p:Configuration=Release",
                "-p:IncludeSymbols=true",
                $"\"{SolutionFilePath}\"");
        }
    }
}
