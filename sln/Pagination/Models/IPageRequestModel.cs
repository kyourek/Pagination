using System;
using System.Linq;
using System.Collections.Generic;

namespace Pagination.Models {

    /// <summary>
    /// Interface for requests that can specify pagination options.
    /// </summary>
    public interface IPageRequestModel {

        /// <summary>
        /// Gets the requested number of items per page.
        /// </summary>
        int ItemsPerPage { get; }

        /// <summary>
        /// Gets the requested page.
        /// </summary>
        int RequestedPage { get; }

        /// <summary>
        /// Gets the requested page if zero (0) is the first page.
        /// </summary>
        int RequestedPageBase0 { get; }

        /// <summary>
        /// Gets the requested page if one (1) is the first page.
        /// </summary>
        int RequestedPageBase1 { get; }

        /// <summary>
        /// Gets a collection of the values necessary to duplicate this page request.
        /// </summary>
        /// <returns>The collection of values necessary to duplicate this page request.</returns>
        IDictionary<string, object> GetRequestValues();

        /// <summary>
        /// Gets a colletion of the values necessary to duplicate this page request for the specified <paramref name="requestedPage"/>.
        /// </summary>
        /// <param name="requestedPage">The page to which the request values direct.</param>
        /// <returns>The collection of values necessary to duplicate this page request for the specified <paramref name="requestedPage"/>.</returns>
        IDictionary<string, object> GetRequestValues(int requestedPage);
    }
}
