using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Pagination.Sample.Controllers {
    using Models.Home;
    using Web;

    public class HomeController : Controller {
        public ActionResult Index(QueryModel query) {
            var pageContext = new HttpPageContext()
                .SetItemsPerPageDefault(20)
                .SetItemsPerPageMaximum(50);
                
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => {
                    try {
                        return s.GetTypes();
                    }
                    catch (ReflectionTypeLoadException ex) {
                        return ex.Types;
                    } })
                .Where(t => t != null)
                .Select(t => t.FullName)
                .Distinct()
                .AsQueryable();

            var searchText = query.SearchText;
            if (!string.IsNullOrWhiteSpace(searchText)) types = types.Where(t => t.Contains(searchText));

            var page = pageContext
                .GetSource()
                .FindPage(types.OrderBy(t => t), query);

            return View(new IndexModel {
                Page = page
            });
        }
    }
}
