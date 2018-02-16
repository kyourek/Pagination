namespace Pagination {
    public class PageContext {
        PageRequest _Request;
        PageRequest Request {
            get { return _Request ?? (_Request = GetRequest()); }
            set { _Request = value; }
        }

        protected virtual PageRequest GetRequest() {
            return new PageRequest();
        }

        public PageSource Source {
            get {
                if (_Source == null) {
                    _Source = new PageSource {
                        ItemsPerPage = Request.ItemsPerPage,
                        PageBaseZero = Request.PageBaseZero
                    };
                }
                return _Source;
            }
            set {
                _Source = value;
            }
        }
        PageSource _Source;

        public PageContext ItemsPerPageDefault(int value) {
            return new PageContext {
                Request = Request,
                Source = new PageSource {
                    ItemsPerPage = Request.ItemsPerPage,
                    PageBaseZero = Request.PageBaseZero,
                    ItemsPerPageDefault = value,
                    ItemsPerPageMaximum = Source.ItemsPerPageMaximum
                }
            };
        }

        public PageContext ItemsPerPageMaximum(int value) {
            return new PageContext {
                Request = Request,
                Source = new PageSource {
                    ItemsPerPage = Request.ItemsPerPage,
                    PageBaseZero = Request.PageBaseZero,
                    ItemsPerPageDefault = Source.ItemsPerPageDefault,
                    ItemsPerPageMaximum = value
                }
            };
        }
    }
}
