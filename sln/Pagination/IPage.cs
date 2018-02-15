using System.Linq;

namespace Pagination {
    public interface IPage {
        int PageRequested { get; }
        int PageTotal { get; }
        int ItemsRequested { get; }
        int ItemsTotal { get; }
        IQueryable Items { get; }
        object Query { get; }
        object QueryPage(int? pageRequested = null);
    }

    public interface IPage<TItem> : IPage {
        new IQueryable<TItem> Items { get; }
    }

    public interface IPage<TItem, TQuery> : IPage<TItem> {
        new TQuery Query { get; }
    }
}
