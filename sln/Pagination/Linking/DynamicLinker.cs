using System;
using System.Linq;
using System.Collections.Generic;

using Pagination.Models;

namespace Pagination.Linking {
 
    internal class DynamicLinker : PageLinker {

        private class LinkFactory {

            public IPageRequestModel Request { get { return _Request; } }
            private readonly IPageRequestModel _Request;

            public bool IsBase1 { get { return _IsBase1; } }
            private readonly bool _IsBase1;

            public LinkFactory(IPageRequestModel request, bool isBase1) {
                if (null == request) throw new ArgumentNullException("request");
                _Request = request;
                _IsBase1 = isBase1;
            }

            public PageLinkModel CreateLink(int page, string text) {
                return new PageLinkModel(Request, page, text);
            }

            public PageLinkModel CreateLink(int page) {
                return CreateLink(page, IsBase1 ? (page + 1).ToString() : page.ToString());
            }

            public PageRangeModel CreateRange(int lower, int upper) {
                return new PageRangeModel(Request, lower, upper);
            }
        }

        public bool IsBase1 { get { return _IsBase1; } }
        private readonly bool _IsBase1;

        public DynamicLinker(bool isBase1) {
            _IsBase1 = isBase1;
        }

        public override IEnumerable<IPageLinkModel> LinkPages(IPageRequestModel request, IPageResultsModel results) {
            if (null == request) throw new ArgumentNullException("request");
            if (null == results) throw new ArgumentNullException("results");

            var list = new List<IPageLinkModel>();
            if (results.TotalPageCount > 1) {

                var requestedPage = request.RequestedPage;
                var totalPageCount = results.TotalPageCount;
                var factory = new LinkFactory(request, IsBase1);

                list.Add(factory.CreateLink(0));

                if (requestedPage > 1) {
                    var test = requestedPage == totalPageCount - 1 && totalPageCount > 3;
                    list.Add(factory.CreateRange(1, test ? requestedPage - 3 : requestedPage - 2));
                    if (test) {
                        list.Add(factory.CreateLink(requestedPage - 2));
                    }
                    list.Add(factory.CreateLink(requestedPage - 1));
                }

                if (requestedPage != 0 && requestedPage != totalPageCount - 1) {
                    list.Add(factory.CreateLink(requestedPage));
                }

                if (requestedPage < totalPageCount - 2) {
                    var test = requestedPage == 0 && totalPageCount > 3;
                    list.Add(factory.CreateLink(requestedPage + 1));
                    if (test) {
                        list.Add(factory.CreateLink(requestedPage + 2));
                    }
                    list.Add(factory.CreateRange(test ? requestedPage + 3 : requestedPage + 2, totalPageCount - 2));
                }

                list.Add(factory.CreateLink(totalPageCount - 1));
            }

            return list;
        }
    }
}
