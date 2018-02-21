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
        public void GetSource_ReturnsSourceWithItemsPerPageSet() {
            Subject.Http.Request.QueryString["_p_ipp"] = "37";
            var pageSource = Subject.GetSource(ItemsSourceEmpty);
            Assert.That(pageSource.Request.ItemsPerPage, Is.EqualTo(37));
        }

        [Test]
        public void GetSource_ReturnsSourceWithPageBaseZeroSet() {
            Subject.Http.Request.QueryString["_p_pbz"] = "401";
            var pageSource = Subject.GetSource(ItemsSourceEmpty);
            Assert.That(pageSource.Request.PageBaseZero, Is.EqualTo(401));
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
