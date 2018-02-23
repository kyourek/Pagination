using System.Linq;

namespace Pagination {
    public interface IPage {
        int PageBaseZero { get; }
        int PageBaseOne { get; }
        int PagesTotal { get; }
        int ItemsPerPage { get; }
        int ItemsTotal { get; }
        IQueryable Items { get; }
        object State { get; }
        IPageConfig Config { get; }
        IPageLinker Linker();
    }

    public interface IPage<TItem> : IPage {
        new IQueryable<TItem> Items { get; }
    }

    public interface IPage<TItem, TState> : IPage<TItem> {
        new TState State { get; }
    }
}
