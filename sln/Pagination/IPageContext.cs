using System.Collections.Generic;

namespace Pagination {
    public interface IPageContext {
        IDictionary<string, object> GetValues(IEnumerable<string> keys);
    }
}