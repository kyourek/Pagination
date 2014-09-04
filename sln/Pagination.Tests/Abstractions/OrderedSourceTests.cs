using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace Pagination.Abstractions.Tests {

    [TestFixture]
    public class OrderedSourceTests {
        
        [Test]
        public void Constructor_SetsProperties() {
            var orderedQueryable = new[] { 1, 3, 4, 2, -1 }.AsQueryable().OrderBy(i => i);
            var orderedSource = new OrderedSource<int>(orderedQueryable);
            Assert.AreSame(orderedQueryable, orderedSource.Queryable);
        }

        [Test]
        public void Queryable_IsOrderedQueryable() {
            var orderedQueryable = new[] { 1, 6, 7, 2, -5 }.AsQueryable().OrderBy(i => i);
            var orderedSource = new OrderedSource<int>(orderedQueryable);
            var source = (ISource<int>)orderedSource;
            Assert.AreSame(orderedQueryable, source.Queryable);
            Assert.AreSame(orderedSource.Queryable, source.Queryable);
        }

        [Test]
        public void Skip_SkipsItems() {
            var orderedQueryable = new[] { "1", "3", "6", "2", "8" }.AsQueryable().OrderBy(i => i);
            var orderedSource = new OrderedSource<string>(orderedQueryable);
            var expected = orderedQueryable.Skip(2).ToList();
            var actual = orderedSource.Skip(2).ToList();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [Test]
        public void ThenBy_ThenBysItems() {
            var items = Enumerable.Range(0, 10).Select(i => Tuple.Create(i % 2, i));
            var orderedQueryable = items.AsQueryable().OrderBy(i => i.Item1);
            var orderedSource = new OrderedSource<Tuple<int, int>>(orderedQueryable);
            var expected = orderedQueryable.ThenBy(i => i.Item2).ToList();
            var actual = orderedSource.ThenBy(i => i.Item2).ToList();
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [Test]
        public void ThenByDescending_ThenByDescendingsItems() {
            var items = Enumerable.Range(0, 10).Select(i => Tuple.Create(i % 2, i));
            var orderedQueryable = items.AsQueryable().OrderBy(i => i.Item1);
            var orderedSource = new OrderedSource<Tuple<int, int>>(orderedQueryable);
            var expected = orderedQueryable.ThenByDescending(i => i.Item2).ToList();
            var actual = orderedSource.ThenByDescending(i => i.Item2).ToList();
            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}
