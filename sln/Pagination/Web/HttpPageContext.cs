using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace Pagination.Web {
    class HttpPageContext : PageContext {
        static void Copy(IDictionary<string, object> dict, NameValueCollection coll, HashSet<string> keys) {
            var names = coll?.AllKeys;
            if (names != null) {
                foreach (var name in names) {
                    if (keys.Contains(name)) {
                        keys.Remove(name);
                        dict[name] = coll[name];
                    }
                }
            }
        }

        protected override PageRequest GetRequest() {
            throw new NotImplementedException();
        }

        HttpContextWrapper _Http;
        public HttpContextWrapper Http {
            get { return _Http ?? (_Http = new HttpContextWrapper(HttpContext.Current)); }
            set { _Http = value; }
        }

        public IDictionary<string, object> GetValues(IEnumerable<string> keys) {
            var http = Http;
            var dict = new Dictionary<string, object>();
            var request = http.Request;
            var keyList = new HashSet<string>(keys, StringComparer.OrdinalIgnoreCase);

            Copy(dict, request?.Form, keyList);
            Copy(dict, request?.QueryString, keyList);

            return dict;
        }
    }
}
