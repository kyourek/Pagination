using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace Pagination.Test {
    [TestFixture, TestOf(typeof(PageWrapper))]
    public class PageWrapperTest {
        Page<object, object> Page => _Page ?? (_Page = new Page<object, object>());
        Page<object, object> _Page;

        PageWrapper Subject0 => _Subject0 ?? (_Subject0 = new PageWrapper(Page));
        PageWrapper _Subject0;

        PageWrapper<object> Subject1 => _Subject1 ?? (_Subject1 = new PageWrapper<object>(Page));
        PageWrapper<object> _Subject1;

        PageWrapper<object, object> Subject2 => _Subject2 ?? (_Subject2 = new PageWrapper<object, object>(Page));
        PageWrapper<object, object> _Subject2;

        PageWrapper[] Subject => _Subject ?? (_Subject = new[] { Subject0, Subject1, Subject2 });
        PageWrapper[] _Subject;

        [SetUp]
        public void SetUp() {
            _Page = null;
            _Subject = null;
            _Subject0 = null;
            _Subject1 = null;
            _Subject2 = null;
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void Page_IsSameAsConstructorParameter(int i) {
            Assert.That(Subject[i].Page, Is.SameAs(Page));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void Config_IsSameAsPageConfig(int i) {
            Page.Config = new PageConfig();
            Assert.That(((IPage)Subject[i]).Config, Is.SameAs(Page.Config));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void Items_IsSameAsPageItems(int i) {
            Page.Items = new object[] { }.AsQueryable();
            Assert.That(((IPage)Subject[i]).Items, Is.SameAs(Page.Items));
        }

        [TestCase(0, 10)]
        [TestCase(0, 100)]
        [TestCase(1, 10)]
        [TestCase(1, 100)]
        [TestCase(2, 10)]
        [TestCase(2, 100)]
        public void ItemsPerPage_IsEqualToPageItemsPerPage(int i, int value) {
            Page.ItemsPerPage = value;
            Assert.That(((IPage)Subject[i]).ItemsPerPage, Is.EqualTo(Page.ItemsPerPage));
        }

        [TestCase(0, 5)]
        [TestCase(0, 25)]
        [TestCase(1, 5)]
        [TestCase(1, 25)]
        [TestCase(2, 5)]
        [TestCase(2, 25)]
        public void ItemsTotal_IsEqualToPageItemsTotal(int i, int value) {
            Page.ItemsTotal = value;
            Assert.That(((IPage)Subject[i]).ItemsTotal, Is.EqualTo(Page.ItemsTotal));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void Linker_CreatesInstance(int i) {
            Assert.That(((IPage)Subject[i]).Linker(), Is.Not.Null);
        }

        [TestCase(0, 12)]
        [TestCase(0, 14)]
        [TestCase(1, 12)]
        [TestCase(1, 14)]
        [TestCase(2, 12)]
        [TestCase(2, 14)]
        public void PageBaseZero_IsEqualToPagePageBaseZero(int i, int value) {
            Page.PageBaseZero = value;
            Assert.That(((IPage)Subject[i]).PageBaseZero, Is.EqualTo(Page.PageBaseZero));
        }

        [TestCase(0, 501)]
        [TestCase(0, 502)]
        [TestCase(1, 501)]
        [TestCase(1, 502)]
        [TestCase(2, 501)]
        [TestCase(2, 502)]
        public void PagesTotal_IsEqualToPagePagesTotal(int i, int value) {
            Page.PagesTotal = value;
            Assert.That(((IPage)Subject[i]).PagesTotal, Is.EqualTo(Page.PagesTotal));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void State_IsSameAsPageState(int i) {
            Page.State = new object();
            Assert.That(((IPage)Subject[i]).State, Is.SameAs(Page.State));
        }

        [Test]
        public void Constructor0_ThrowsArgumentNullExceptionIfPageIsNull() {
            Assert.That(() => new PageWrapper(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Constructor1_ThrowsArgumentNullExceptionIfPageIsNull() {
            Assert.That(() => new PageWrapper<object>(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Constructor2_ThrowsArgumentNullExceptionIfPageIsNull() {
            Assert.That(() => new PageWrapper<object, object>(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Constructor0_ArgumentNullExceptionHasNameOfArgument() {
            Assert.That(() => new PageWrapper(null), Throws.Exception.With.Property("ParamName").EqualTo("page"));
        }

        [Test]
        public void Constructor1_ArgumentNullExceptionHasNameOfArgument() {
            Assert.That(() => new PageWrapper<object>(null), Throws.Exception.With.Property("ParamName").EqualTo("page"));
        }

        [Test]
        public void Constructor2_ArgumentNullExceptionHasNameOfArgument() {
            Assert.That(() => new PageWrapper<object, object>(null), Throws.Exception.With.Property("ParamName").EqualTo("page"));
        }
    }
}
