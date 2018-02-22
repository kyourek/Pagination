namespace Pagination.ReleaseActions {
    class Tag : ReleaseAction {
        public override void Work() {
            var tag = Context.Version.StagedVersion;
            Process("git", "commit", "-a", "-m", $"\"(Auto-)Commit version '{tag}'.\"");
            Process("git", "tag", "-a", tag, "-m", $"\"(Auto-)Tag version '{tag}'.\"");
            Process("git", "push", "origin", tag);
        }
    }
}
