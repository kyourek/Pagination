using System;
using System.Linq;
using System.Collections.Generic;

namespace Pagination.Models {

    /// <summary>
    /// Interface for a link in a chain of pages.
    /// </summary>
    public interface IPageLinkModel {

        /// <summary>
        /// The page to which this link points.
        /// </summary>
        int LinkPage { get; }

        /// <summary>
        /// The page to which this link points if the first page is zero (0).
        /// </summary>
        int LinkPageBase0 { get; }

        /// <summary>
        /// The page to which this link points if the first page is one (1).
        /// </summary>
        int LinkPageBase1 { get; }

        /// <summary>
        /// The lower page number of this link, or the page if this link
        /// is not a range of pages.
        /// </summary>
        int LowerPage { get; }

        /// <summary>
        /// The upper page number of this link, or the page if this link
        /// is not a range of pages.
        /// </summary>
        int UpperPage { get; }

        /// <summary>
        /// <c>true</c> if this link is a range of pages. Otherwise, <c>false</c>.
        /// </summary>
        bool IsPageRange { get; }

        /// <summary>
        /// <c>true</c> if this link represents the requested page number. Otherwise, <c>false</c>.
        /// </summary>
        bool IsRequestedPage { get; }

        /// <summary>
        /// The text to be displayed when communicating this page link.
        /// </summary>
        string LinkText { get; }

        /// <summary>
        /// The request made that caused this page link to be created.
        /// </summary>
        IPageRequestModel Request { get; }
    }
}
