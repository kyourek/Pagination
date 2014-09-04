using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Pagination.Models {

    /// <summary>
    /// An object representing a request for a specific page of a data set.
    /// </summary>
    public class PageRequestModel : IPageRequestModel {

        /// <summary>
        /// Gets or sets the requested number of items per page.
        /// </summary>
        public virtual int ItemsPerPage { get; set; }

        /// <summary>
        /// Gets or sets the requested page.
        /// </summary>
        public virtual int RequestedPage { get; set; }

        /// <summary>
        /// Gets the requested page if zero (0) is the first page.
        /// </summary>
        public virtual int RequestedPageBase0 { get { return RequestedPage; } }

        /// <summary>
        /// Gets the requested page if one (1) is the first page.
        /// </summary>
        public virtual int RequestedPageBase1 { get { return RequestedPage + 1; } }

        /// <summary>
        /// Gets a dictionary that contains the values necessary to duplicate this request.
        /// </summary>
        /// <returns>A dictionary that contains the values necessary to duplicate this request.</returns>
        public virtual IDictionary<string, object> GetRequestValues() {
            var type = GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var routeValues = new Dictionary<string, object>();

            foreach (var property in properties) {
                if (property.CanRead) {
                    if (property.CanWrite) {
                        if (property.GetIndexParameters().Length == 0) {
                            routeValues.Add(property.Name, property.GetValue(this, null));
                        }
                    }
                }
            }

            return routeValues;
        }

        /// <summary>
        /// Gets a dictionary that contains the values necessary to duplicate this request for the specified <paramref name="requestedPage"/>.
        /// </summary>
        /// <param name="requestedPage">The requested page of the request.</param>
        /// <returns>A dictionary that contains the values necessary to duplicate this request for the specified <paramref name="requestedPage"/>.</returns>
        public virtual IDictionary<string, object> GetRequestValues(int requestedPage) {

            var routeValues = new Dictionary<string, object>();

            var childRouteValues = GetRequestValues();
            if (childRouteValues != null) {
                foreach (var routeValue in childRouteValues) {
                    routeValues.Add(routeValue.Key, routeValue.Value);
                }
            }

            routeValues["RequestedPage"] = requestedPage;

            return routeValues;
        }
    }
}
