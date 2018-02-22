using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Pagination.Sample.Controllers {
    using Models.Home;
    using Web;

    public class HomeController : Controller {
        public ActionResult Index(FilterModel filter) {
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

            var searchText = filter.SearchText;
            if (!string.IsNullOrWhiteSpace(searchText)) types = types.Where(t => t.Contains(searchText));

            var page = new HttpPageContext().FindPage(types.OrderBy(type => type), filter);

            return View(new IndexModel {
                Page = page
            });
        }
    }
}
