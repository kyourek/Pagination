using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace Pagination.Models.Tests {

    [TestFixture]
    public class PageRequestModelTests {
 
        internal class MockPageRequesModel : PageRequestModel {
            public int IntValue { get; set; }
            public bool BoolValue { get; set; }
            public string StringValue { get; set; }
            public double DoubleValue { get; set; }
            public object ObjectValue { get; set; }

            public string GetOnlyValue { get { return "GetOnly"; } }
            public double SetOnlyValue { set { throw new NotSupportedException(); } }
        }

        [Test]
        public void ItemsPerPage_CanBeSet() {
            var m = new PageRequestModel();
            for (var i = 0; i < 10; i++) {
                m.ItemsPerPage = i;
                Assert.AreEqual(i, m.ItemsPerPage);
            }
        }

        [Test]
        public void RequestedPage_CanBeSet() {
            var m = new PageRequestModel();
            for (var i = 0; i < 10; i++) {
                m.RequestedPage = i;
                Assert.AreEqual(i, m.RequestedPage);
            }
        }

        [Test]
        public void RequestedPageBase0_ReturnsRequestedPage() {
            var m = new PageRequestModel();
            for (var i = 0; i < 10; i++) {
                m.RequestedPage = i;
                Assert.AreEqual(m.RequestedPage, m.RequestedPageBase0);
            }
        }

        [Test]
        public void RequestedPageBase1_ReturnsRequestedPagePlus1() {
            var m = new PageRequestModel();
            for (var i = 0; i < 10; i++) {
                m.RequestedPage = i;
                Assert.AreEqual(m.RequestedPage + 1, m.RequestedPageBase1);
            }
        }

        [Test]
        public void GetRequestValues_ReturnsPropertyValuesInDictionary() {
            var request = new MockPageRequesModel {
                IntValue = 56,
                BoolValue = true,
                DoubleValue = 1.23,
                ObjectValue = new object(),
                StringValue = "string value",
                ItemsPerPage = 88,
                RequestedPage = 21
            };

            var values = request.GetRequestValues();

            Assert.AreEqual(request.IntValue, values["IntValue"]);
            Assert.AreEqual(request.BoolValue, values["BoolValue"]);
            Assert.AreEqual(request.DoubleValue, values["DoubleValue"]);
            Assert.AreEqual(request.ObjectValue, values["ObjectValue"]);
            Assert.AreEqual(request.StringValue, values["StringValue"]);
            Assert.AreEqual(request.ItemsPerPage, values["ItemsPerPage"]);
            Assert.AreEqual(request.RequestedPage, values["RequestedPage"]);

            Assert.AreEqual(7, values.Count);
        }

        [Test]
        public void GetRequestValues_ReturnsPropertyValuesWithRequestedPage() {
            var request = new MockPageRequesModel {
                IntValue = 56,
                BoolValue = true,
                DoubleValue = 1.23,
                ObjectValue = new object(),
                StringValue = "string value",
                ItemsPerPage = 88,
                RequestedPage = 21
            };

            var page = 36;
            var values = request.GetRequestValues(page);

            Assert.AreEqual(request.IntValue, values["IntValue"]);
            Assert.AreEqual(request.BoolValue, values["BoolValue"]);
            Assert.AreEqual(request.DoubleValue, values["DoubleValue"]);
            Assert.AreEqual(request.ObjectValue, values["ObjectValue"]);
            Assert.AreEqual(request.StringValue, values["StringValue"]);
            Assert.AreEqual(request.ItemsPerPage, values["ItemsPerPage"]);

            Assert.AreEqual(7, values.Count);
            Assert.AreEqual(page, values["RequestedPage"]);
        }
    }
}
