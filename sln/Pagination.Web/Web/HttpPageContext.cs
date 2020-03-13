using System;

#if NETCOREAPP
using HTTPREQUEST = Microsoft.AspNetCore.Http.HttpRequest;
#else
using System.Web;
using HTTPREQUEST = System.Web.HttpRequestBase;
#endif

namespace Pagination.Web {
    public class HttpPageContext : PageContext {
        private static int? GetValue(string s) =>
            int.TryParse(s, out var i)
                ? i
                : default(int?);

        private PageRequest PageRequestFactoryDefault() {
            var request = Request;
            var requestForm = request?.Form;
#if NETCOREAPP
            var requestQuery = request?.Query;
#else
            var requestQuery = request?.QueryString;
#endif
            var ipp = Config.ItemsPerPageKey;
            var pbz = Config.PageBaseZeroKey;

            return new PageRequest {
                ItemsPerPage = GetValue(requestForm?[ipp]) ?? GetValue(requestQuery?[ipp]),
                PageBaseZero = GetValue(requestForm?[pbz]) ?? GetValue(requestQuery?[pbz])
            };
        }

        internal override Func<PageRequest> PageRequestFactory {
            get => _PageRequestFactory ?? (_PageRequestFactory = PageRequestFactoryDefault);
            set => _PageRequestFactory = value;
        }
        private Func<PageRequest> _PageRequestFactory;

        public HTTPREQUEST Request { get; }

        public HttpPageContext(HTTPREQUEST request = null) {
            Request = request;
#if !NETCOREAPP
            Request = Request ?? new HttpRequestWrapper(HttpContext.Current.Request);
#endif
        }
    }
}
