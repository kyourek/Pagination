namespace Pagination {
    class PageContext : IPageContext {
        PageRequest _Request;
        public PageRequest Request {
            get { return _Request ?? (_Request = new PageRequest()); }
            set { _Request = value; }
        }
    }
}
