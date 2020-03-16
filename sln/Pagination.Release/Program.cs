using Domore.Conf;

namespace Pagination {
    internal class Program {
        private static void Main(string[] args) {
            Conf.ContentsProvider = new AppSettingsProvider();
            new Release(args);
        }
    }
}
