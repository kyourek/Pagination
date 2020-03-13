using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;

#if NETCOREAPP
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using COLLECTION = System.Collections.Generic.Dictionary<string, Microsoft.Extensions.Primitives.StringValues>;
#else
using System.Web;
using HTTPREQUEST = System.Web.HttpRequestBase;
using COLLECTION = System.Collections.Specialized.NameValueCollection;
#endif

using NUnit.Framework;

namespace Pagination.Web {
    [TestFixture, TestOf(typeof(HttpPageContext))]
    public class HttpPageContextTest {
        private static readonly IOrderedQueryable<string> ItemsSourceEmpty = new string[] { }.AsQueryable().OrderBy(s => s);

#if NETCOREAPP
        private class QueryCollectionMock : IQueryCollection {
            public COLLECTION Collection { get; }

            public QueryCollectionMock(COLLECTION collection) {
                Collection = collection;
            }

            public StringValues this[string key] => Collection.ContainsKey(key) ? Collection[key] : default;

            public int Count => throw new NotImplementedException();

            public ICollection<string> Keys => throw new NotImplementedException();

            public bool ContainsKey(string key) {
                throw new NotImplementedException();
            }

            public IEnumerator<KeyValuePair<string, StringValues>> GetEnumerator() {
                throw new NotImplementedException();
            }

            public bool TryGetValue(string key, out StringValues value) {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator() {
                throw new NotImplementedException();
            }
        }

        private class HttpRequestMock : HttpRequest {
            public override IFormCollection Form { get; set; }
            public override IQueryCollection Query { get; set; }

            public HttpRequestMock(COLLECTION form, COLLECTION query) {
                Form = new FormCollection(form);
                Query = new QueryCollectionMock(query);
            }

            public override HttpContext HttpContext => throw new NotImplementedException();
            public override string Method { get; set; }
            public override string Scheme { get; set; }
            public override bool IsHttps { get; set; }
            public override HostString Host { get; set; }
            public override PathString PathBase { get; set; }
            public override PathString Path { get; set; }
            public override QueryString QueryString { get; set; }
            public override string Protocol { get; set; }
            public override IHeaderDictionary Headers => throw new NotImplementedException();
            public override IRequestCookieCollection Cookies { get; set; }
            public override long? ContentLength { get; set; }
            public override string ContentType { get; set; }
            public override Stream Body { get; set; }
            public override bool HasFormContentType => throw new NotImplementedException();
            public override Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();
        }
#else
        private class HttpRequestMock : HttpRequestBase {
            public HttpRequestMock(COLLECTION form, COLLECTION query) {
                Form = form;
                QueryString = query;
            }

            public override COLLECTION Form { get; }
            public override COLLECTION QueryString { get; }
        }
#endif

        private COLLECTION Form { get; } = new COLLECTION();
        private COLLECTION Query { get; } = new COLLECTION();

        private HttpPageContext Subject {
            get => _Subject ?? (_Subject = new HttpPageContext(new HttpRequestMock(Form, Query)));
            set => _Subject = value;
        }
        private HttpPageContext _Subject;

        [SetUp]
        public void SetUp() {
            Form.Clear();
            Query.Clear();
            Subject = null;
        }

#region GetSource
        [Test]
        public void GetSource_ReturnsSourceWithSameConfig() {
            var ps = Subject.GetSource(ItemsSourceEmpty);
            Assert.That(ps.Config, Is.SameAs(Subject.Config));
        }

        [Test]
        public void GetSource_ReturnsSourceWithItemsSource() {
            var itemsSource = new string[] { }.AsQueryable().OrderBy(s => s);
            var pageSource = Subject.GetSource(itemsSource);
            Assert.That(pageSource.ItemsSource, Is.SameAs(itemsSource));
        }

        [Test]
        public void GetSource_ReturnsSourceWithNullItemsPerPage() {
            Assert.That(
                Subject.GetSource(ItemsSourceEmpty).Request.ItemsPerPage,
                Is.Null);
        }

        [Test]
        public void GetSource_ReturnsSourceWithNullItemsPerPageWhenValuesAreNotIntegers() {
            var key = "ipp";
            Subject.SetItemsPerPageKey(key);
            Form[key] = "21.1";
            Query[key] = "23.3";
            Assert.That(
                Subject.GetSource(ItemsSourceEmpty).Request.ItemsPerPage,
                Is.Null);
        }

        [Test]
        public void GetSource_ReturnsSourceWithItemsPerPageSet() {
            Query["_p_ipp"] = "37";
            var pageSource = Subject.GetSource(ItemsSourceEmpty);
            Assert.That(pageSource.Request.ItemsPerPage, Is.EqualTo(37));
        }

        [Test]
        public void GetSource_FindsItemsPerPageBasedOnKey() {
            Subject.SetItemsPerPageKey("IPerPage");
            Query["IPerPage"] = "777";
            Assert.That(
                Subject.GetSource(ItemsSourceEmpty).Request.ItemsPerPage,
                Is.EqualTo(777));
        }

        [Test]
        public void GetSource_FindsItemsPerPageFromFormBeforeQueryString() {
            var key = "ipp";
            Subject.SetItemsPerPageKey(key);
            Form[key] = "21";
            Query[key] = "23";
            Assert.That(
                Subject.GetSource(ItemsSourceEmpty).Request.ItemsPerPage,
                Is.EqualTo(21));
        }

        [Test]
        public void GetSource_ReturnsSourceWithNullPageBaseZero() {
            Assert.That(
                Subject.GetSource(ItemsSourceEmpty).Request.PageBaseZero,
                Is.Null);
        }

        [Test]
        public void GetSource_ReturnsSourceWithNullPageBaseZeroWhenValuesAreNotIntegers() {
            var key = "pbz";
            Subject.SetPageBaseZeroKey(key);
            Form[key] = "not an int";
            Query[key] = "this neither";
            Assert.That(
                Subject.GetSource(ItemsSourceEmpty).Request.PageBaseZero,
                Is.Null);
        }

        [Test]
        public void GetSource_ReturnsSourceWithPageBaseZeroSet() {
            Query["_p_pbz"] = "401";
            var pageSource = Subject.GetSource(ItemsSourceEmpty);
            Assert.That(pageSource.Request.PageBaseZero, Is.EqualTo(401));
        }

        [Test]
        public void GetSource_FindsPageBaseZeroBasedOnKey() {
            Subject.SetPageBaseZeroKey("PBZero");
            Query["PBZero"] = "1024";
            Assert.That(
                Subject.GetSource(ItemsSourceEmpty).Request.PageBaseZero,
                Is.EqualTo(1024));
        }

        [Test]
        public void GetSource_FindsPageBaseZeroFromFormBeforeQueryString() {
            var key = "pbz";
            Subject.SetPageBaseZeroKey(key);
            Form[key] = "14";
            Query[key] = "12";
            Assert.That(
                Subject.GetSource(ItemsSourceEmpty).Request.PageBaseZero,
                Is.EqualTo(14));
        }
#endregion

#region SetItemsPerPageDefault
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(20)]
        [TestCase(100)]
        public void SetItemsPerPageDefault_SetsConfigValue(int value) {
            Subject.SetItemsPerPageDefault(value);
            Assert.That(Subject.Config.ItemsPerPageDefault, Is.EqualTo(value));
        }

        [Test]
        public void SetItemsPerPageDefault_ReturnsThisInstance() {
            Assert.That(Subject.SetItemsPerPageDefault(1), Is.SameAs(Subject));
        }
#endregion

#region SetItemsPerPageMaximum
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(20)]
        [TestCase(100)]
        public void SetItemsPerPageMaximum_SetsConfigValue(int value) {
            Subject.SetItemsPerPageMaximum(value);
            Assert.That(Subject.Config.ItemsPerPageMaximum, Is.EqualTo(value));
        }

        [Test]
        public void SetItemsPerPageMaximum_ReturnsThisInstance() {
            Assert.That(Subject.SetItemsPerPageMaximum(1), Is.SameAs(Subject));
        }
#endregion

#region SetPageBaseZeroKey
        [TestCase("pbz")]
        [TestCase("PageBaseZero")]
        public void SetPageBaseZeroKey_SetsConfigValue(string value) {
            Subject.SetPageBaseZeroKey(value);
            Assert.That(Subject.Config.PageBaseZeroKey, Is.EqualTo(value));
        }

        [Test]
        public void SetPageBaseZeroKey_ReturnsThisInstance() {
            Assert.That(Subject.SetPageBaseZeroKey("pbz"), Is.SameAs(Subject));
        }
#endregion

#region SetItemsPerPageKey
        [TestCase("ipp")]
        [TestCase("ItemsPerPage")]
        public void SetItemsPerPageKey_SetsConfigValue(string value) {
            Subject.SetItemsPerPageKey(value);
            Assert.That(Subject.Config.ItemsPerPageKey, Is.EqualTo(value));
        }

        [Test]
        public void SetItemsPerPageKey_ReturnsThisInstance() {
            Assert.That(Subject.SetItemsPerPageKey("ipp"), Is.SameAs(Subject));
        }
#endregion
    }
}
