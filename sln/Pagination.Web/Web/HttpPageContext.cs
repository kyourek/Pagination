using System;
using System.Collections.Specialized;
using System.Web;

namespace Pagination.Web {
    public class HttpPageContext : PageContext {
        static int? GetValue(NameValueCollection collection, string name) {
            if (collection != null) {
                if (int.TryParse(collection[name], out int i)) {
                    return i;
                }
            }
            return null;
        }

        PageRequest FillRequest(NameValueCollection collection, PageRequest request = null) {
            request = request ?? new PageRequest();
            request.ItemsPerPage = GetValue(collection, Config.ItemsPerPageKey) ?? request.ItemsPerPage;
            request.PageBaseZero = GetValue(collection, Config.PageBaseZeroKey) ?? request.PageBaseZero;
            return request;
        }

        PageRequest GetRequestDefault() {
            var http = Http;
            var request = http?.Request;
            return
                FillRequest(request?.Form,
                FillRequest(request?.QueryString));
        }

        internal HttpContextBase Http {
            get => _Http ?? (_Http = new HttpContextWrapper(HttpContext.Current));
            set => _Http = value;
        }
        HttpContextBase _Http;

        Func<PageRequest> _GetRequest;
        internal override Func<PageRequest> GetRequest {
            get => _GetRequest ?? (_GetRequest = GetRequestDefault);
            set => _GetRequest = value;
        }
    }
}
