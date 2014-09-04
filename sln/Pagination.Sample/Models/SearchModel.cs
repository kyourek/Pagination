using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;

using Pagination.Models;

namespace Pagination.Sample.Models {

    public class SearchModel : PageRequestModel {
        public string SearchText { get; set; }
    }
}
