using System;
using System.Collections.Specialized;
using System.Web;

namespace Pagination {
    public class PageContext {
        static string GetValue(string name, NameValueCollection collection) {
            var keys = collection?.AllKeys;
            if (keys != null) {
                foreach (var key in keys) {
                    if (string.Equals(key, name, StringComparison.OrdinalIgnoreCase)) {
                        return collection[key];
                    }
                }
            }
            return null;
        }

        static int? GetInteger(string name, NameValueCollection collection) {
            var value = GetValue(name, collection);
            return int.TryParse(value, out int i)
                ? i
                : default(int?);
        }

        string _PageRequestedKey;
        public string PageRequestedKey {
            get { return _PageRequestedKey ?? (_PageRequestedKey = "_PageRequested"); }
            set { _PageRequestedKey = value; }
        }

        string _ItemsRequestedKey;
        public string ItemsRequestedKey {
            get { return _ItemsRequestedKey ?? (_ItemsRequestedKey = "_ItemsRequested"); }
            set { _ItemsRequestedKey = value; }
        }

        HttpContextWrapper _Http;
        public HttpContextWrapper Http {
            get { return _Http ?? (_Http = new HttpContextWrapper(HttpContext.Current)); }
            set { _Http = value; }
        }

        public int? GetPageRequested() {
            var key = PageRequestedKey;
            var request = Http?.Request;
            return
                GetInteger(key, request?.Form) ??
                GetInteger(key, request?.QueryString);
        }

        public int? GetItemsRequested() {
            var key = ItemsRequestedKey;
            var request = Http?.Request;
            return
                GetInteger(key, request?.Form) ??
                GetInteger(key, request?.QueryString);
        }
    }
}
