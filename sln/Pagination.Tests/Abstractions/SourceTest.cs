using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections;

using NUnit.Framework;

namespace Pagination.Abstractions.Tests {

    [TestFixture]
    public class SourceTest {

        [Test]
        public void Constructor_SetsProperties() {
            var queryable = new[] { "1", "3", "6", "2", "8" }.AsQueryable();
            var source = new Source<string>(queryable);
            Assert.AreSame(queryable, source.Queryable);
        }

        [Test]
        public void Take_TakesItems() {
            var queryable = new[] { "1", "3", "6", "2", "8" }.AsQueryable();
            var source = new Source<string>(queryable);
            var expected = queryable.Take(3).ToList();
            var actual = source.Take(3).ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void OrderBy_OrderBysItems() {
            var queryable = new[] { "1", "3", "6", "2", "8" }.AsQueryable();
            var source = new Source<string>(queryable);
            var expected = queryable.OrderBy(i => int.Parse(i)).ToList();
            var actual = source.OrderBy(i => int.Parse(i)).ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void OrderByDescending_OrderByDescendingsItems() {
            var queryable = new[] { "1", "3", "6", "2", "8" }.AsQueryable();
            var source = new Source<string>(queryable);
            var expected = queryable.OrderByDescending(i => int.Parse(i)).ToList();
            var actual = source.OrderByDescending(i => int.Parse(i)).ToList();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Count_CountsItems() {
            var queryable = new[] { "1", "3", "6", "2", "8" }.AsQueryable();
            var source = new Source<string>(queryable);
            Assert.AreEqual(queryable.Count(), source.Count());
        }
    }
}
