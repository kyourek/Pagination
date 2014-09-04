Pagination
==========
(formerly https://code.google.com/p/source-pagination/)

Pagination allows easy control and deferred execution of specific pages in a LINQ request.

First, we'll create a model that extends `Pagination.Models.PageRequestModel`. Extending this class gives our model properties like `RequestedPage` (the page index requested by the user) and `ItemsPerPage` (the number of items to show per page). Our model will also inherit the method `GetRequestValues`, which can serialize the simple properties of the model to an `IDictionary<string, object>` of key-value pairs.

This model adds a single property, `SearchText`.

    public class SearchModel : Pagination.Models.PageRequestModel {
        public string SearchText { get; set; }
    }

Next, we'll create a simple data set of strings. In real life, this `IQueryable<string>` could come from a database through an ORM like EntityFramework. For simplicity's sake, though, we'll get a collection of all the types loaded in our application.

    var types = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(s => s.GetTypes())
        .Select(t => t.FullName)
        .AsQueryable();

Now let's make an instance of our model (again, in real life this instance could come from an MVC controller action parameter). Then we apply a `Where` clause to our data set according to it's `SearchText` value.

    var model = new SearchModel { SearchText = 'Mvc' };
    var searchText = model.SearchText;
    if (!string.IsNullOrWhiteSpace(searchText)) types = types.Where(t => t.Contains(searchText));

We can use a `Pagination.PageSourceFactory` to create a `Pagination.Models.IPageSourceModel`. This is the object that will do the grunt work of paging the results of the request.

    var factory = new Pagination.PageSourceFactory {
        MaxItemsPerPage = 50,
        DefaultItemsPerPage = 20
    };
    var source = factory.CreateSource(types, model);

Now we can get to work. The results object has properties like `TotalPageCount` (the total number of pages available to the query) and `TotalItemCount` (the total number of items on all pages in the query). The `Results` property is an `IQueryable` representing the actual items on the requested page.

    var results = Model.OrderBy(s => s).Query();
    var items = results.Results.Select(r => new { TypeName = r });

To create a collection of `Pagination.Models.IPageLinkModel` (a class that can be used to create an HTML link to a specific page in the request), use the `LinkPages` method.

    var pages = results.LinkPages(PageLinker.CreateDynamic(true, "←", "→"));
