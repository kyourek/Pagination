using System;
using System.Linq;
using System.Collections.Generic;

using Moq;
using NUnit.Framework;

using Pagination.Models;
using Pagination.Abstractions;

namespace Pagination.Tests {

    [TestFixture]
    public class PageSourceFactoryTests {

        [Test]
        public void MaxItemsPerPage_CanBeSet() {
            var f = new PageSourceFactory();
            for (var i = 0; i < 10; i++) {
                f.MaxItemsPerPage = i;
                Assert.AreEqual(i, f.MaxItemsPerPage);
            }
        }

        [Test]
        public void DefaultItemsPerPage_CanBeSet() {
            var f = new PageSourceFactory();
            for (var i = 0; i < 10; i++) {
                f.DefaultItemsPerPage = i;
                Assert.AreEqual(i, f.DefaultItemsPerPage);
            }
        }

        [Test]
        public void MaxItemsPerPage_IsInitiallyMaxValue() {
            Assert.AreEqual(int.MaxValue, new PageSourceFactory().MaxItemsPerPage);
        }

        [Test]
        public void DefaultItemsPerPage_IsInitially50() {
            Assert.AreEqual(50, new PageSourceFactory().DefaultItemsPerPage);
        }

        [Test]
        public void CreateSource_CreatesSource_1() {
            var source = Enumerable.Range(1, 10).AsQueryable();
            var request = new PageRequestModel();

            var factory = new PageSourceFactory();
            var model = factory.CreateSource(source, request);

            Assert.AreSame(source, model.Source.Queryable);
            Assert.AreSame(request, model.Request);
        }

        [Test]
        public void CreateSource_CreatesOrderedSource_1() {
            var source = Enumerable.Range(1, 10).AsQueryable().OrderBy(i => i) as IQueryable<int>;
            var request = new PageRequestModel();

            var factory = new PageSourceFactory();
            var model = factory.CreateSource(source, request);

            Assert.AreSame(source, model.Source.Queryable);
            Assert.AreSame(request, model.Request);
            Assert.IsInstanceOf(typeof(IOrderedPageSourceModel<int, PageRequestModel>), model);
        }

        [Test]
        public void CreateSource_CreatesPageRequestModel() {
            var source = Enumerable.Range(1, 10).AsQueryable();
            var requestedPage = 3;
            var itemsPerPage = 2;
            var factory = new PageSourceFactory();
            var model = factory.CreateSource(source, requestedPage, itemsPerPage);
            Assert.IsInstanceOf(typeof(PageRequestModel), model.Request);
            Assert.AreEqual(requestedPage, model.Request.RequestedPage);
            Assert.AreEqual(itemsPerPage, model.Request.ItemsPerPage);
        }

        [Test]
        public void CreateSource_CreatesPageRequestModelWithDefaultItemsPerPage() {
            var source = Enumerable.Range(1, 100).AsQueryable();
            var requestedPage = 6;
            var defaultItemsPerPage = 5;
            var factory = new PageSourceFactory { DefaultItemsPerPage = defaultItemsPerPage };
            var model = factory.CreateSource(source, requestedPage);
            Assert.IsInstanceOf(typeof(PageRequestModel), model.Request);
            Assert.AreEqual(requestedPage, model.Request.RequestedPage);
            Assert.AreEqual(defaultItemsPerPage, model.Request.ItemsPerPage);
        }

        [Test]
        public void CreateSource_CreatesPageRequestModelWithRequestedPage0() {
            var source = Enumerable.Range(1, 35).AsQueryable();
            var factory = new PageSourceFactory { DefaultItemsPerPage = 3 };
            var model = factory.CreateSource(source);
            Assert.IsInstanceOf(typeof(PageRequestModel), model.Request);
            Assert.AreEqual(0, model.Request.RequestedPage);
            Assert.AreEqual(factory.DefaultItemsPerPage, model.Request.ItemsPerPage);
        }
    }
}
