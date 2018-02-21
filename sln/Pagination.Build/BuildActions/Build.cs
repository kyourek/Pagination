namespace Pagination.BuildActions {
    class Build : BuildAction {
        public override void Work() {
            var solutionPath = $"\"{SolutionFilePath}\"";
            Process("nuget", "restore", solutionPath);
            Process("MSBuild", "/t:Clean", "/t:Rebuild", "/p:Configuration=Release", solutionPath);
        }
    }
}
