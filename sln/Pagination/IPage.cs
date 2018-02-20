using System.Linq;

namespace Pagination {
    public interface IPage {
        int PageBaseZero { get; }
        int PageBaseOne { get; }
        int PageTotal { get; }
        int ItemsPerPage { get; }
        int ItemsTotal { get; }
        IQueryable Items { get; }
        object Filter { get; }
        IPageConfig Config { get; }
        IPageRequest Request { get; }
    }

    public interface IPage<TItem> : IPage {
        new IQueryable<TItem> Items { get; }
    }

    public interface IPage<TItem, TFilter> : IPage<TItem> {
        new TFilter Filter { get; }
    }
}
