using System;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using Pagination.Models;
using Pagination.Sample.Models;

namespace Pagination.Sample.Controllers {
    public class HomeController : Controller {
        public ActionResult Index(SearchModel model) {

            var factory = new PageSourceFactory {
                MaxItemsPerPage = 50,
                DefaultItemsPerPage = 20
            };

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => {
                    try {
                        return s.GetTypes();
                    }
                    catch (ReflectionTypeLoadException ex) {
                        return ex.Types;
                    }
                })
                .Where(t => t != null)
                .Select(t => t.FullName)
                .AsQueryable();

            var searchText = model.SearchText;
            if (!string.IsNullOrWhiteSpace(searchText)) types = types.Where(t => t.Contains(searchText));

            var source = factory.CreateSource(types, model);

            return View(source);
        }
    }
}
