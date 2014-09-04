using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

using Pagination.Models;
using Pagination.Abstractions;

namespace Pagination {

    /// <summary>
    /// Extension methods for instances of <see cref="T:UrlHelper"/> that
    /// privide interaction with Pagination models.
    /// </summary>
    public static class PaginatedUrl {

        /// <summary>
        /// Creates a URL that can be used to access the page represented
        /// by the instance of <see cref="T:IPageLinkModel"/>.
        /// </summary>
        /// <param name="urlHelper">
        /// The <see cref="T:UrlHelper"/> instance that this method extends.
        /// </param>
        /// <param name="model">
        /// The <see cref="T:IPageLinkModel"/> for which the URL is to be created.
        /// </param>
        /// <returns>
        /// A URL that can be used to access the page represented by the given <paramref name="model"/>.
        /// </returns>
        public static string Page(this UrlHelper urlHelper, IPageLinkModel model) {
            if (null == urlHelper) throw new ArgumentNullException("urlHelper");
            var actionName = Paginated.GetActionName(urlHelper);
            var routeValues = Paginated.GetRouteValues(model);
            return urlHelper.Action(actionName, routeValues);
        }
    }
}
