using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Linq;
using System.Collections.Generic;

using Pagination.Models;
using Pagination.Abstractions;

namespace Pagination {
    
    /// <summary>
    /// Extension methods for instances of <see cref="T:HtmlHelper"/> that interact with Pagination models.
    /// </summary>
    public static class PaginatedHtml {

        /// <summary>
        /// Creates an HTML hyperlink that links to the page represented by the <paramref name="model"/>.
        /// </summary>
        /// <param name="htmlHelper">
        /// The instance of <see cref="T:HtmlHelper"/> that creates the hyperlink.
        /// </param>
        /// <param name="model">
        /// The <see cref="T:IPageLinkModel"/> that represents the page to be linked.
        /// </param>
        /// <param name="htmlAttributes">
        /// An object that contains the HTML attributes for the hyperlink. The attributes
        /// are retrieved through reflection by examining the properties of the object.
        /// The object is typically created by using object initializer syntax.
        /// </param>
        /// <returns>
        /// The HTML hyperlink.
        /// </returns>
        public static MvcHtmlString PageLink(this HtmlHelper htmlHelper, IPageLinkModel model, object htmlAttributes) {
            if (null == model) throw new ArgumentNullException("model");
            if (null == htmlHelper) throw new ArgumentNullException("htmlHelper");
            var actionName = Paginated.GetActionName(htmlHelper);
            var routeValues = Paginated.GetRouteValues(model);
            return htmlHelper.ActionLink(model.LinkText, actionName, routeValues, htmlAttributes);
        }

        /// <summary>
        /// Creates an HTML hyperlink that links to the page represented by the <paramref name="model"/>.
        /// </summary>
        /// <param name="htmlHelper">
        /// The instance of <see cref="T:HtmlHelper"/> that creates the hyperlink.
        /// </param>
        /// <param name="model">
        /// The <see cref="T:IPageLinkModel"/> that represents the page to be linked.
        /// </param>
        /// <param name="htmlAttributes">
        /// An object that contains the HTML attributes to set for the hyperlink.
        /// </param>
        /// <returns>
        /// The HTML hyperlink.
        /// </returns>
        public static MvcHtmlString PageLink(this HtmlHelper htmlHelper, IPageLinkModel model, IDictionary<string, object> htmlAttributes) {
            if (null == model) throw new ArgumentNullException("model");
            if (null == htmlHelper) throw new ArgumentNullException("htmlHelper");
            var actionName = Paginated.GetActionName(htmlHelper);
            var routeValues = Paginated.GetRouteValues(model);
            return htmlHelper.ActionLink(model.LinkText, actionName, routeValues, htmlAttributes);
        }

        /// <summary>
        /// Creates an HTML hyperlink that links to the page represented by the <paramref name="model"/>.
        /// </summary>
        /// <param name="htmlHelper">
        /// The instance of <see cref="T:HtmlHelper"/> that creates the hyperlink.
        /// </param>
        /// <param name="model">
        /// The <see cref="T:IPageLinkModel"/> that represents the page to be linked.
        /// </param>
        /// <returns>
        /// The HTML hyperlink.
        /// </returns>
        public static MvcHtmlString PageLink(this HtmlHelper htmlHelper, IPageLinkModel model) {
            if (null == model) throw new ArgumentNullException("model");
            if (null == htmlHelper) throw new ArgumentNullException("htmlHelper");
            var actionName = Paginated.GetActionName(htmlHelper);
            var routeValues = Paginated.GetRouteValues(model);
            return htmlHelper.ActionLink(model.LinkText, actionName, routeValues);
        }
    }
}
