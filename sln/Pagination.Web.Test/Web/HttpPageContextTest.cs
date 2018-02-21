using System.Collections.Specialized;
using System.Linq;
using System.Web;

using NUnit.Framework;

namespace Pagination.Web {
    [TestFixture, TestOf(typeof(HttpPageContext))]
    public class HttpPageContextTest {
        static readonly IOrderedQueryable<string> ItemsSourceEmpty = new string[] { }.AsQueryable().OrderBy(s => s);

        class HttpRequest : HttpRequestBase {
            NameValueCollection _Form;
            NameValueCollection _QueryString;
            public override NameValueCollection Form => _Form ?? (_Form = new NameValueCollection());
            public override NameValueCollection QueryString => _QueryString ?? (_QueryString = new NameValueCollection());
        }

        class HttpContext : HttpContextBase {
            HttpRequestBase _Request;
            public override HttpRequestBase Request => _Request ?? (_Request = new HttpRequest());
        }

        HttpPageContext Subject {
            get => _Subject ?? (_Subject = new HttpPageContext {
                Http = new HttpContext()
            });
            set => _Subject = value;
        }
        HttpPageContext _Subject;

        [TearDown]
        public void TearDown() {
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
            Subject.Http.Request.Form[key] = "21.1";
            Subject.Http.Request.QueryString[key] = "23.3";
            Assert.That(
                Subject.GetSource(ItemsSourceEmpty).Request.ItemsPerPage,
                Is.Null);
        }

        [Test]
        public void GetSource_ReturnsSourceWithItemsPerPageSet() {
            Subject.Http.Request.QueryString["_p_ipp"] = "37";
            var pageSource = Subject.GetSource(ItemsSourceEmpty);
            Assert.That(pageSource.Request.ItemsPerPage, Is.EqualTo(37));
        }

        [Test]
        public void GetSource_FindsItemsPerPageBasedOnKey() {
            Subject.SetItemsPerPageKey("IPerPage");
            Subject.Http.Request.QueryString["IPerPage"] = "777";
            Assert.That(
                Subject.GetSource(ItemsSourceEmpty).Request.ItemsPerPage,
                Is.EqualTo(777));
        }

        [Test]
        public void GetSource_FindsItemsPerPageFromFormBeforeQueryString() {
            var key = "ipp";
            Subject.SetItemsPerPageKey(key);
            Subject.Http.Request.Form[key] = "21";
            Subject.Http.Request.QueryString[key] = "23";
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
            Subject.Http.Request.Form[key] = "not an int";
            Subject.Http.Request.QueryString[key] = "this neither";
            Assert.That(
                Subject.GetSource(ItemsSourceEmpty).Request.PageBaseZero,
                Is.Null);
        }

        [Test]
        public void GetSource_ReturnsSourceWithPageBaseZeroSet() {
            Subject.Http.Request.QueryString["_p_pbz"] = "401";
            var pageSource = Subject.GetSource(ItemsSourceEmpty);
            Assert.That(pageSource.Request.PageBaseZero, Is.EqualTo(401));
        }

        [Test]
        public void GetSource_FindsPageBaseZeroBasedOnKey() {
            Subject.SetPageBaseZeroKey("PBZero");
            Subject.Http.Request.QueryString["PBZero"] = "1024";
            Assert.That(
                Subject.GetSource(ItemsSourceEmpty).Request.PageBaseZero,
                Is.EqualTo(1024));
        }

        [Test]
        public void GetSource_FindsPageBaseZeroFromFormBeforeQueryString() {
            var key = "pbz";
            Subject.SetPageBaseZeroKey(key);
            Subject.Http.Request.Form[key] = "14";
            Subject.Http.Request.QueryString[key] = "12";
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
