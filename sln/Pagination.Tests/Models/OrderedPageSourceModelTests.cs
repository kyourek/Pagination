using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Moq;
using NUnit.Framework;
using Pagination.Abstractions;

namespace Pagination.Models.Tests {

    [TestFixture]
    public class OrderedPageSourceModelTests {

        [Test]
        public void ThenBy_ReturnsOrderedSourceThenBy() {
            var thenBy = new Mock<IOrderedSource<string>>();
            var orderedSource = new Mock<IOrderedSource<string>>(MockBehavior.Strict);
            Expression<Func<String, int>> keySelector = s => s.Length;

            orderedSource.Setup(s => s.ThenBy(keySelector)).Returns(thenBy.Object).Verifiable("The source was not ordered then by by the key selector.");

            var m1 = new OrderedPageSourceModel<string, PageRequestModel>(8, 7, orderedSource.Object, new PageRequestModel());
            var m2 = m1.ThenBy(keySelector);
            orderedSource.Verify();
            Assert.AreEqual(m1.GetMaxItemsPerPage(), m2.GetMaxItemsPerPage());
            Assert.AreEqual(m1.GetDefaultItemsPerPage(), m2.GetDefaultItemsPerPage());
            Assert.AreSame(thenBy.Object, m2.Source);
            Assert.AreSame(m1.Request, m2.Request);            
        }

        [Test]
        public void ThenByDescending_ReturnsOrderedSourceThenByDescending() {
            var thenBy = new Mock<IOrderedSource<string>>();
            var orderedSource = new Mock<IOrderedSource<string>>(MockBehavior.Strict);
            Expression<Func<String, int>> keySelector = s => s.Length;

            orderedSource.Setup(s => s.ThenByDescending(keySelector)).Returns(thenBy.Object).Verifiable("The source was not ordered then by descending by the key selector.");

            var m1 = new OrderedPageSourceModel<string, PageRequestModel>(8, 7, orderedSource.Object, new PageRequestModel());
            var m2 = m1.ThenByDescending(keySelector);
            orderedSource.Verify();
            Assert.AreEqual(m1.GetMaxItemsPerPage(), m2.GetMaxItemsPerPage());
            Assert.AreEqual(m1.GetDefaultItemsPerPage(), m2.GetDefaultItemsPerPage());
            Assert.AreSame(thenBy.Object, m2.Source);
            Assert.AreSame(m1.Request, m2.Request);            
        }

        [Test]
        public void Source_IsOrderedSource() {
            var orderedSource = new OrderedSource<string>(new[] { "hello", "world" }.AsQueryable().OrderBy(s => s));
            var orderedPageSource = new OrderedPageSourceModel<string, PageRequestModel>(5, 1, orderedSource, new PageRequestModel());
            var pageSource = (IPageSourceModel<string>)orderedPageSource;
            Assert.AreSame(orderedSource, pageSource.Source);
            Assert.AreSame(orderedPageSource.Source, pageSource.Source);

            var iorderedPageSource = (IOrderedPageSourceModel<string>)orderedPageSource;
            Assert.AreSame(orderedPageSource.Source, iorderedPageSource.Source);
        }

        [Test]
        public void MaxItemsPerPage_ReturnsOrderedPageSource() {
            var orderedSource = new OrderedSource<string>(new[] { "hello", "world" }.AsQueryable().OrderBy(s => s));
            var orderedPageSource = new OrderedPageSourceModel<string, PageRequestModel>(5, 1, orderedSource, new PageRequestModel());
            
            var newPageSource = orderedPageSource.MaxItemsPerPage(43);
            Assert.AreSame(orderedPageSource.Source, newPageSource.Source);
            Assert.AreSame(orderedPageSource.Request, newPageSource.Request);
            Assert.AreEqual(orderedPageSource.GetDefaultItemsPerPage(), newPageSource.GetDefaultItemsPerPage());
            Assert.AreEqual(43, newPageSource.GetMaxItemsPerPage());
            Assert.IsInstanceOf(typeof(OrderedPageSourceModel<string, PageRequestModel>), newPageSource);

            var newNewPageSource = ((IOrderedPageSourceModel<string, PageRequestModel>)orderedPageSource).MaxItemsPerPage(52);
            Assert.AreSame(orderedPageSource.Source, newNewPageSource.Source);
            Assert.AreSame(orderedPageSource.Request, newNewPageSource.Request);
            Assert.AreEqual(orderedPageSource.GetDefaultItemsPerPage(), newNewPageSource.GetDefaultItemsPerPage());
            Assert.AreEqual(52, newNewPageSource.GetMaxItemsPerPage());
            Assert.IsInstanceOf(typeof(OrderedPageSourceModel<string, PageRequestModel>), newNewPageSource);
        }

        [Test]
        public void DefaultItemsPerPage_ReturnsOrderedSource() {
            var orderedSource = new OrderedSource<string>(new[] { "hello", "world" }.AsQueryable().OrderBy(s => s));
            var orderedPageSource = new OrderedPageSourceModel<string, PageRequestModel>(5, 1, orderedSource, new PageRequestModel());

            var newPageSource = orderedPageSource.DefaultItemsPerPage(18);
            Assert.AreSame(orderedPageSource.Source, newPageSource.Source);
            Assert.AreSame(orderedPageSource.Request, newPageSource.Request);
            Assert.AreEqual(orderedPageSource.GetMaxItemsPerPage(), newPageSource.GetMaxItemsPerPage());
            Assert.AreEqual(18, newPageSource.GetDefaultItemsPerPage());
            Assert.IsInstanceOf(typeof(OrderedPageSourceModel<string, PageRequestModel>), newPageSource);

            var newNewPageSource = ((IOrderedPageSourceModel<string, PageRequestModel>)orderedPageSource).DefaultItemsPerPage(36);
            Assert.AreSame(orderedPageSource.Source, newNewPageSource.Source);
            Assert.AreSame(orderedPageSource.Request, newNewPageSource.Request);
            Assert.AreEqual(orderedPageSource.GetMaxItemsPerPage(), newNewPageSource.GetMaxItemsPerPage());
            Assert.AreEqual(36, newNewPageSource.GetDefaultItemsPerPage());
            Assert.IsInstanceOf(typeof(OrderedPageSourceModel<string, PageRequestModel>), newNewPageSource);
        }
    }
}
