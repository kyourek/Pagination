using System.Collections.Specialized;
using System.Web;

namespace Pagination.Web {
    class HttpPageContext : PageContext {
        static int? GetValue(NameValueCollection collection, string name) {
            if (collection != null) {
                if (int.TryParse(collection[name], out int i)) {
                    return i;
                }
            }
            return null;
        }

        private PageRequest FillRequest(NameValueCollection collection, PageRequest request = null) {
            request = request ?? new PageRequest();
            request.ItemsPerPage = GetValue(collection, ItemsPerPageKey) ?? request.ItemsPerPage;
            request.PageBaseZero = GetValue(collection, PageBaseZeroKey) ?? request.PageBaseZero;
            return request;
        }

        protected override PageRequest GetRequest() {
            var http = Http;
            var request = http?.Request;
            return 
                FillRequest(request?.Form,
                FillRequest(request?.QueryString));
        }

        public string ItemsPerPageKey {
            get => _ItemsPerPageKey ?? (_ItemsPerPageKey = HttpPageConfig.Default.ItemsPerPageKey);
            set => _ItemsPerPageKey = value;
        }
        string _ItemsPerPageKey;

        public string PageBaseZeroKey {
            get => _PageBaseZeroKey ?? (_PageBaseZeroKey = HttpPageConfig.Default.PageBaseZeroKey);
            set => _PageBaseZeroKey = value;
        }
        string _PageBaseZeroKey;

        public HttpContextWrapper Http {
            get => _Http ?? (_Http = new HttpContextWrapper(HttpContext.Current));
            set => _Http = value;
        }
        HttpContextWrapper _Http;
    }
}
