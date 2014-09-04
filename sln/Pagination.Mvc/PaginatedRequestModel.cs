using System;
using System.Web.Routing;
using System.Linq;
using System.Collections.Generic;

using Pagination.Models;

namespace Pagination {

    /// <summary>
    /// MVC extension methods for objects that implement the <see cref="T:IPageRequestModel"/> interface.
    /// </summary>
    public static class PaginatedRequestModel {

        /// <summary>
        /// Creates an instance of <see cref="T:RouteValueDictionary"/> that contains the route information
        /// necessary to duplicate the <paramref name="pageRequestModel"/>.
        /// </summary>
        /// <param name="pageRequestModel">The model for which the <see cref="T:RouteValueDictionary"/> is created.</param>
        /// <returns>
        /// A new instance of <see cref="T:RouteValueDictionary"/> with the route information necessary to
        /// duplicate the <paramref name="pageRequestModel"/>.
        /// </returns>
        public static RouteValueDictionary GetRouteValues(this IPageRequestModel pageRequestModel) {
            if (null == pageRequestModel) throw new ArgumentNullException("pageRequestModel");
            return new RouteValueDictionary(pageRequestModel.GetRequestValues());
        }

        /// <summary>
        /// Creates an instance of <see cref="T:RouteValueDictionary"/> that contains the route information
        /// necessary to duplicate the <paramref name="pageRequestModel"/> for the given <paramref name="requestedPage"/>.
        /// </summary>
        /// <param name="pageRequestModel">The model for which the <see cref="T:RouteValueDictionary"/> is created.</param>
        /// <param name="requestedPage">The requested page for which the <see cref="T:RouteValueDictionary"/> is created.</param>
        /// <returns>
        /// A new instance of <see cref="T:RouteValueDictionary"/> with the route information necessary to
        /// duplicate the <paramref name="pageRequestModel"/> for the given <paramref name="requestedPage"/>.
        /// </returns>
        public static RouteValueDictionary GetRouteValues(this IPageRequestModel pageRequestModel, int requestedPage) {
            if (null == pageRequestModel) throw new ArgumentNullException("pageRequestModel");
            return new RouteValueDictionary(pageRequestModel.GetRequestValues(requestedPage));
        }
    }
}
