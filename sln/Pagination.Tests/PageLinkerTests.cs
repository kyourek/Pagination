using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using Pagination.Linking;

namespace Pagination.Tests {

    [TestFixture]
    public class PageLinkerTests {

        [Test]
        public void Default_CanBeSet() {
            var def = new NumberLinker(true);
            PageLinker.Default = def;
            Assert.AreSame(def, PageLinker.Default);
        }

        [Test]
        public void Create_CreatesNumberLinker() {
            var isBase1 = true;
            var prevText = "prv";
            var nextText = "nxt";
            var forcePrevNext = true;
            var linker = PageLinker.Create(isBase1, prevText, nextText, forcePrevNext);
            Assert.IsInstanceOf(typeof(PrevNextLinker), linker);
            
            var pnl = linker as PrevNextLinker;
            Assert.IsInstanceOf(typeof(NumberLinker), pnl.BaseLinker);

            Assert.AreEqual(forcePrevNext, pnl.ForcePrevNext);
            Assert.AreEqual(prevText, pnl.PrevText);
            Assert.AreEqual(nextText, pnl.NextText);
            Assert.AreEqual(isBase1, (pnl.BaseLinker as NumberLinker).IsBase1);            
        }

        [Test]
        public void Create_ForcePrevNextIsTrue() {
            var isBase1 = true;
            var prevText = "prv";
            var nextText = "nxt";
            var linker = PageLinker.Create(isBase1, prevText, nextText);
            Assert.IsInstanceOf(typeof(PrevNextLinker), linker);

            var pnl = linker as PrevNextLinker;
            Assert.IsInstanceOf(typeof(NumberLinker), pnl.BaseLinker);

            Assert.AreEqual(true, pnl.ForcePrevNext);
            Assert.AreEqual(prevText, pnl.PrevText);
            Assert.AreEqual(nextText, pnl.NextText);
            Assert.AreEqual(isBase1, (pnl.BaseLinker as NumberLinker).IsBase1);            
        }

        [Test]
        public void Create_IsBase1IsTrue() {
            var prevText = "prv";
            var nextText = "nxt";
            var linker = PageLinker.Create(prevText, nextText);
            Assert.IsInstanceOf(typeof(PrevNextLinker), linker);

            var pnl = linker as PrevNextLinker;
            Assert.IsInstanceOf(typeof(NumberLinker), pnl.BaseLinker);

            Assert.AreEqual(true, pnl.ForcePrevNext);
            Assert.AreEqual(prevText, pnl.PrevText);
            Assert.AreEqual(nextText, pnl.NextText);
            Assert.AreEqual(true, (pnl.BaseLinker as NumberLinker).IsBase1);            
        }

        [Test]
        public void CreateDynamic_CreatesDynamicLinker() {
            var linker = PageLinker.CreateDynamic() as DynamicLinker;
            Assert.IsNotNull(linker);
            Assert.IsTrue(linker.IsBase1);
        }

        [Test]
        public void CreateDynamic_CreatesDynamicLinkerWithSpecifiedBase() {
            var linker = PageLinker.CreateDynamic(true) as DynamicLinker;
            Assert.IsTrue(linker.IsBase1);
            linker = PageLinker.CreateDynamic(false) as DynamicLinker;
            Assert.IsFalse(linker.IsBase1);
        }

        [Test]
        public void CreateDynamic_CreatesPrevNextLinkerWithTexts() {
            var linker = PageLinker.CreateDynamic("p", "n") as PrevNextLinker;
            Assert.IsInstanceOf(typeof(DynamicLinker), linker.BaseLinker);
            Assert.AreEqual("p", linker.PrevText);
            Assert.AreEqual("n", linker.NextText);
            Assert.IsTrue((linker.BaseLinker as DynamicLinker).IsBase1);
        }

        [Test]
        public void CreateDynamic_CreatesPrevNextLinkerWithSpecifiedBase() {
            foreach (var isBase1 in new[] { false, true }) {
                var linker = PageLinker.CreateDynamic(isBase1, "prv", "nxt") as PrevNextLinker;
                Assert.IsInstanceOf(typeof(DynamicLinker), linker.BaseLinker);
                Assert.AreEqual("prv", linker.PrevText);
                Assert.AreEqual("nxt", linker.NextText);
                Assert.AreEqual(isBase1, (linker.BaseLinker as DynamicLinker).IsBase1);
            }
        }

        [Test]
        public void CreateDynamic_CreatesPrevNextLinkerWithForcedPrevNext() {
            foreach (var isBase1 in new[] { false, true }) {
                foreach (var forcePrevNext in new[] { false, true }) {
                    var linker = PageLinker.CreateDynamic(isBase1, "prev", "next", forcePrevNext) as PrevNextLinker;
                    Assert.IsInstanceOf(typeof(DynamicLinker), linker.BaseLinker);
                    Assert.AreEqual("prev", linker.PrevText);
                    Assert.AreEqual("next", linker.NextText);
                    Assert.AreEqual(isBase1, (linker.BaseLinker as DynamicLinker).IsBase1);
                    Assert.AreEqual(forcePrevNext, linker.ForcePrevNext);
                }
            }
        }

        [Test]
        public void CreatePrevNext_ReturnsInstanceOfPrevNextLinkerWithNullBaseLinker() {
            var prevText = "p";
            var nextText = "n";
            var forcePrevNext = false;
            var linker = PageLinker.CreatePrevNext(prevText, nextText, forcePrevNext);
            Assert.IsNotNull(linker);
            Assert.IsInstanceOf(typeof(PrevNextLinker), linker);

            var pnl = linker as PrevNextLinker;
            Assert.AreEqual(prevText, pnl.PrevText);
            Assert.AreEqual(nextText, pnl.NextText);
            Assert.AreEqual(forcePrevNext, pnl.ForcePrevNext);
            Assert.IsNull(pnl.BaseLinker);
        }

        [Test]
        public void CreatePrevNext_DefaultsToForcePrevNext() {
            var prevText = "p";
            var nextText = "n";
            var linker = PageLinker.CreatePrevNext(prevText, nextText) as PrevNextLinker;
            Assert.AreEqual(prevText, linker.PrevText);
            Assert.AreEqual(nextText, linker.NextText);
            Assert.IsTrue(linker.ForcePrevNext);
            Assert.IsNull(linker.BaseLinker);
        }
    }
}
