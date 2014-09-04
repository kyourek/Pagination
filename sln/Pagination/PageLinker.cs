using System;
using System.Linq;
using System.Collections.Generic;

using Pagination.Models;
using Pagination.Linking;

namespace Pagination {

    /// <summary>
    /// An object that can create a chain of linked page objects.
    /// </summary>
    public abstract class PageLinker {

        /// <summary>
        /// Gets or sets the default <see cref="T:PageLinker"/> used to create chains of page objects.
        /// </summary>
        public static PageLinker Default {
            get {
                if (null == _Default) _Default = Create();
                return _Default;
            }
            set {
                if (null == value) throw new ArgumentNullException("Default");
                _Default = value;
            }
        }
        private static PageLinker _Default;

        /// <summary>
        /// Creates a collection of <see cref="T:IPageLinkModel"/> objects from the specified <paramref name="request"/> and <paramref name="results"/>.
        /// </summary>
        /// <param name="request">The request for which the collection of <see cref="T:IPageLinkModel"/> objects is to be created.</param>
        /// <param name="results">The results for which the collection of <see cref="T:IPageLinkModel"/> objects is to be created.</param>
        /// <returns>A collection of <see cref="T:IPageLinkModel"/> objects from the specified <paramref name="request"/> and <paramref name="results"/>.</returns>
        public abstract IEnumerable<IPageLinkModel> LinkPages(IPageRequestModel request, IPageResultsModel results);

        /// <summary>
        /// Creates a <see cref="T:PageLinker"/> using the specified parameters.
        /// </summary>
        /// <param name="isBase1"><c>true</c> to use base-1 page numbers. Otherwise, <c>false</c>.</param>
        /// <param name="prevText">The text to be displayed for the previous page.</param>
        /// <param name="nextText">The text to be displayed for the next page.</param>
        /// <param name="forcePrevNext"><c>true</c> to always add previous- and next-page links. Otherwise, <c>false</c>.</param>
        /// <returns>A <see cref="T:PageLinker"/> that will link pages using the specified parameters.</returns>
        public static PageLinker Create(bool isBase1, string prevText, string nextText, bool forcePrevNext) {
            var baseLinker = new NumberLinker(isBase1);
            if (string.IsNullOrEmpty(prevText) && string.IsNullOrEmpty(nextText)) {
                return baseLinker;
            }
            return new PrevNextLinker(baseLinker, prevText, nextText, forcePrevNext);
        }

        /// <summary>
        /// Creates a <see cref="T:PageLinker"/> using the specified parameters.
        /// </summary>
        /// <param name="isBase1"><c>true</c> to use base-1 page numbers. Otherwise, <c>false</c>.</param>
        /// <param name="prevText">The text to be displayed for the previous page.</param>
        /// <param name="nextText">The text to be displayed for the next page.</param>
        /// <returns>A <see cref="T:PageLinker"/> that will link pages using the specified parameters.</returns>
        public static PageLinker Create(bool isBase1, string prevText, string nextText) {
            return Create(isBase1, prevText, nextText, true);
        }

        /// <summary>
        /// Creates a <see cref="T:PageLinker"/> using the specified parameters.
        /// </summary>
        /// <param name="prevText">The text to be displayed for the previous page.</param>
        /// <param name="nextText">The text to be displayed for the next page.</param>
        /// <returns>A <see cref="T:PageLinker"/> that will link pages using the specified parameters.</returns>
        public static PageLinker Create(string prevText, string nextText) {
            return Create(true, prevText, nextText);
        }

        /// <summary>
        /// Creates a <see cref="T:PageLinker"/> using the specified parameters.
        /// </summary>
        /// <param name="isBase1"><c>true</c> to use base-1 page numbers. Otherwise, <c>false</c>.</param>
        /// <returns>A <see cref="T:PageLinker"/> that will link pages using the specified parameters.</returns>
        public static PageLinker Create(bool isBase1) {
            return Create(isBase1, null, null, false);
        }

        /// <summary>
        /// Creates a <see cref="T:PageLinker"/>.
        /// </summary>
        /// <returns>A <see cref="T:PageLinker"/>.</returns>
        public static PageLinker Create() {
            return Create(true);
        }

        /// <summary>
        /// Creates a dynamic <see cref="T:PageLinker"/> using the specified parameters.
        /// </summary>
        /// <param name="isBase1"><c>true</c> to use base-1 page numbers. Otherwise, <c>false</c>.</param>
        /// <param name="prevText">The text to be displayed for the previous page.</param>
        /// <param name="nextText">The text to be displayed for the next page.</param>
        /// <param name="forcePrevNext"><c>true</c> to always add previous- and next-page links. Otherwise, <c>false</c>.</param>
        /// <returns>A dynamic <see cref="T:PageLinker"/> that will link pages using the specified parameters.</returns>
        public static PageLinker CreateDynamic(bool isBase1, string prevText, string nextText, bool forcePrevNext) {
            var baseLinker = new DynamicLinker(isBase1);
            if (string.IsNullOrEmpty(prevText) && string.IsNullOrEmpty(nextText)) {
                return baseLinker;
            }
            return new PrevNextLinker(baseLinker, prevText, nextText, forcePrevNext);
        }

        /// <summary>
        /// Creates a dynamic <see cref="T:PageLinker"/> using the specified parameters.
        /// </summary>
        /// <param name="isBase1"><c>true</c> to use base-1 page numbers. Otherwise, <c>false</c>.</param>
        /// <param name="prevText">The text to be displayed for the previous page.</param>
        /// <param name="nextText">The text to be displayed for the next page.</param>
        /// <returns>A dynamic <see cref="T:PageLinker"/> that will link pages using the specified parameters.</returns>
        public static PageLinker CreateDynamic(bool isBase1, string prevText, string nextText) {
            return CreateDynamic(isBase1, prevText, nextText, true);
        }

        /// <summary>
        /// Creates a dynamic <see cref="T:PageLinker"/> using the specified parameters.
        /// </summary>
        /// <param name="prevText">The text to be displayed for the previous page.</param>
        /// <param name="nextText">The text to be displayed for the next page.</param>
        /// <returns>A dynamic <see cref="T:PageLinker"/> that will link pages using the specified parameters.</returns>
        public static PageLinker CreateDynamic(string prevText, string nextText) {
            return CreateDynamic(true, prevText, nextText);
        }

        /// <summary>
        /// Creates a dynamic <see cref="T:PageLinker"/> using the specified parameters.
        /// </summary>
        /// <param name="isBase1"><c>true</c> to use base-1 page numbers. Otherwise, <c>false</c>.</param>
        /// <returns>A dynamic <see cref="T:PageLinker"/> that will link pages using the specified parameters.</returns>
        public static PageLinker CreateDynamic(bool isBase1) {
            return CreateDynamic(isBase1, null, null, false);
        }

        /// <summary>
        /// Creates a dynamic <see cref="T:PageLinker"/>.
        /// </summary>
        /// <returns>A dynamic <see cref="T:PageLinker"/>.</returns>
        public static PageLinker CreateDynamic() {
            return CreateDynamic(true);
        }

        /// <summary>
        /// Creates a <see cref="T:PageLinker"/> that only displays previous- and next-page options (no numbers are shown).
        /// </summary>
        /// <param name="prevText">The text to be displayed for the previous page.</param>
        /// <param name="nextText">The text to be displayed for the next page.</param>
        /// <param name="forcePrevNext"><c>true</c> to always add previous- and next-page links. Otherwise, <c>false</c>.</param>
        /// <returns></returns>
        public static PageLinker CreatePrevNext(string prevText, string nextText, bool forcePrevNext) {
            return new PrevNextLinker(null, prevText, nextText, forcePrevNext);
        }

        /// <summary>
        /// Creates a <see cref="T:PageLinker"/> that only displays previous- and next-page options (no numbers are shown).
        /// </summary>
        /// <param name="prevText">The text to be displayed for the previous page.</param>
        /// <param name="nextText">The text to be displayed for the next page.</param>
        /// <returns></returns>
        public static PageLinker CreatePrevNext(string prevText, string nextText) {
            return CreatePrevNext(prevText, nextText, true);
        }
    }
}
