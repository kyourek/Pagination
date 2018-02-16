using System.Linq;

using NUnit.Framework;

namespace Pagination.Linking.Tests {
    [TestFixture]
    public class DynamicLinkerTests {
        [Test]
        public void LinkPageBaseZeros_ReturnsLinkedPages() {
            var linker = new DynamicLinker { BaseOne = true };
            var page = new Page {
                PageBaseZero = 1,
                PageTotal = 56
            };

            var linked = linker.Links(page);
            Assert.IsNotNull(linked);

            var p0 = linked.ElementAt(0);
            Assert.AreEqual(0, p0.LinkPageBaseZero);
            Assert.AreEqual("1", p0.LinkText);
            Assert.AreEqual(p0.LinkPageBaseZero, p0.LowerPageBaseZero);
            Assert.AreEqual(p0.LinkPageBaseZero, p0.UpperPageBaseZero);
            Assert.IsFalse(p0.IsPageRange);
            Assert.AreSame(page, p0.Page);

            var p1 = linked.ElementAt(1);
            Assert.AreEqual(1, p1.LinkPageBaseZero);
            Assert.AreEqual("2", p1.LinkText);
            Assert.AreEqual(p1.LinkPageBaseZero, p1.LowerPageBaseZero);
            Assert.AreEqual(p1.LinkPageBaseZero, p1.UpperPageBaseZero);
            Assert.IsFalse(p1.IsPageRange);
            Assert.AreSame(page, p1.Page);

            var p2 = linked.ElementAt(2);
            Assert.AreEqual(2, p2.LinkPageBaseZero);
            Assert.AreEqual("3", p2.LinkText);
            Assert.AreEqual(p2.LinkPageBaseZero, p2.LowerPageBaseZero);
            Assert.AreEqual(p2.LinkPageBaseZero, p2.UpperPageBaseZero);
            Assert.IsFalse(p2.IsPageRange);
            Assert.AreSame(page, p2.Page);

            var p3 = linked.ElementAt(3);
            Assert.AreEqual(3, p3.LinkPageBaseZero);
            Assert.AreEqual("...", p3.LinkText);
            Assert.AreEqual(3, p3.LowerPageBaseZero);
            Assert.AreEqual(54, p3.UpperPageBaseZero);
            Assert.IsTrue(p3.IsPageRange);
            Assert.AreSame(page, p3.Page);

            var p4 = linked.ElementAt(4);
            Assert.AreEqual(55, p4.LinkPageBaseZero);
            Assert.AreEqual("56", p4.LinkText);
            Assert.AreEqual(p4.LinkPageBaseZero, p4.LowerPageBaseZero);
            Assert.AreEqual(p4.LinkPageBaseZero, p4.UpperPageBaseZero);
            Assert.IsFalse(p4.IsPageRange);
            Assert.AreSame(page, p4.Page);

            Assert.IsNull(linked.ElementAtOrDefault(5));
        }
    }
}
