using System;
using System.Collections.Generic;
using System.Linq;

namespace Pagination {
    class PageContext : IPageContext {
        public IDictionary<string, object> AllValues { get; } =
            new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        public IDictionary<string, object> GetValues(IEnumerable<string> keys) {
            if (null == keys) throw new ArgumentNullException(nameof(keys));
            return keys
                .Where(key => AllValues.ContainsKey(key))
                .ToDictionary(
                    key => key,
                    key => AllValues[key]);
        }
    }
}
