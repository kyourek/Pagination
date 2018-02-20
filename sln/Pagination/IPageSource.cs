using System.Linq;

namespace Pagination {
    public interface IPageSource {
        IPageConfig Config { get; }
        IPageRequest Request { get; }
    }

    public interface IPageSource<TItem> : IPageSource {
        IOrderedQueryable<TItem> Query { get; }
        IPage<TItem> FindPage();
    }

    public interface IPageSource<TItem, TFilter> : IPageSource<TItem> {
        TFilter Filter { get; }
        new IPage<TItem, TFilter> FindPage();
    }
}
