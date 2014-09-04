using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Pagination.Models {

    /// <summary>
    /// Interface for a collection of <see cref="T:IPageLinkModel"/>.
    /// </summary>
    public interface IPageChainModel : IEnumerable<IPageLinkModel> {

        /// <summary>
        /// The total number of items on all of the linked page objects combined.
        /// </summary>
        int TotalItemCount { get; }

        /// <summary>
        /// The total number of pages on all of the linked page objects combined.
        /// </summary>
        int TotalPageCount { get; }
    }
}
