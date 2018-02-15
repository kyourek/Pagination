using System.Linq;

namespace Pagination {
    public interface IPage {
        int PageBaseZero { get; }
        int PageTotal { get; }
        int ItemsPerPage { get; }
        int ItemsTotal { get; }
        IQueryable Items { get; }
        object Query { get; }
    }

    public interface IPage<TItem> : IPage {
        new IQueryable<TItem> Items { get; }
    }

    public interface IPage<TItem, TQuery> : IPage<TItem> {
        new TQuery Query { get; }
    }
}
