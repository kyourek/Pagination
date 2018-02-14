using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Pagination.Sample.Controllers {
    using Models.Home;

    public class HomeController : Controller {
        public ActionResult Index(QueryModel query) {
            var factory = new PageFactory {
                MaximumItemsRequested = 50,
                DefaultItemsRequested = 20
            };

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

            var page = factory.CreatePage(types.OrderBy(t => t), query);

            return View(new IndexModel {
                Page = page
            });
        }
    }
}
