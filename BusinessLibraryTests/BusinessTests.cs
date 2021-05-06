using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.Tests
{
    [TestClass()]
    public class BusinessTests
    {
        private Business _business;

        [TestInitialize]
        public void Initialize()
        {
            _business = new Business();
            _business._items = new Dictionary<string, List<string>>
            {
                { "key1", new List<string> { "value1", "value2"} },
                { "key2", new List<string> { "value1b", "value2b"} },
                { "key3", new List<string> { "value1c", "value2c"} },
                { "keya", new List<string> { "value"} },
            };
        }

        [TestMethod()]
        public void AddKeyTest()
        {
            var result = _business.AddKey("key4", "value1d");
            Assert.AreEqual(result.StatusCode, 201);
            Assert.IsTrue(_business._items.ContainsKey("key4"));

            var badResult = _business.AddKey(null, null);
            Assert.IsFalse(badResult.StatusCode == 200);

            badResult = _business.AddKey("key4", null);
            Assert.IsFalse(badResult.StatusCode == 200);

            badResult = _business.AddKey("", "value");
            Assert.IsFalse(badResult.StatusCode == 200);
        }

        [TestMethod()]
        public void AllMembersTest()
        {
            var result = _business.AllMembers();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == 200);
            Assert.IsTrue(result.Members.Count > 0);
        }

        [TestMethod()]
        public void ClearTest()
        {
            var result = _business.Clear();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == 200);
            Assert.IsTrue(_business._items.Count == 0);
        }

        [TestMethod()]
        public void GetKeysTest()
        {
            var result = _business.GetKeys();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count != 0);
        }

        [TestMethod()]
        public void GetMembersTest()
        {
            var result = _business.GetMembers("key1");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == 200);
            Assert.IsTrue(result.Members.Count > 0);

            var badResult = _business.GetMembers("key4");
            Assert.IsFalse(badResult.StatusCode == 200);

            badResult = _business.GetMembers("");
            Assert.IsFalse(badResult.StatusCode == 200);
        }

        [TestMethod()]
        public void ItemsTest()
        {
            var result = _business.Items();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == 200);
            Assert.IsTrue(result.Members.Count > 0);
        }

        [TestMethod()]
        public void KeyExistsTest()
        {
            var result = _business.KeyExists("key1");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == 200);
            Assert.AreEqual(result.Message, "True");

            result = _business.KeyExists("key4");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == 200);
            Assert.AreEqual(result.Message, "False");

            var badResult = _business.KeyExists("");
            Assert.IsFalse(badResult.StatusCode == 200);
        }

        [TestMethod()]
        public void RemoveByKeyValueTest()
        {
            var result = _business.Remove("keya", "value");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == 200);
            Assert.IsFalse(_business._items.ContainsKey("keya"));

            result = _business.Remove("key1", "value1");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == 200);
            Assert.IsFalse(_business._items["key1"].Contains("value1"));

            var badResult = _business.Remove("key4", "v");
            Assert.IsFalse(badResult.StatusCode == 200);

            badResult = _business.Remove("", "v");
            Assert.IsFalse(badResult.StatusCode == 200);

            badResult = _business.Remove("key1", "v");
            Assert.IsFalse(badResult.StatusCode == 200);
        }

        [TestMethod()]
        public void RemoveByKeyTest()
        {
            var result = _business.Remove("key1");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == 200);
            Assert.IsFalse(_business._items.ContainsKey("key1"));

            var badResult = _business.Remove("key4");
            Assert.IsFalse(badResult.StatusCode == 200);

            badResult = _business.Remove("");
            Assert.IsFalse(badResult.StatusCode == 200);
        }

        [TestMethod()]
        public void ValueExistsTest()
        {
            var result = _business.ValueExists("key1", "value1");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == 200);
            Assert.AreEqual(result.Message, "True");

            result = _business.ValueExists("key1", "1");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.StatusCode == 200);
            Assert.AreEqual(result.Message, "False");

            var badResult = _business.ValueExists("key1", "");
            Assert.IsFalse(badResult.StatusCode == 200);

            badResult = _business.ValueExists("", "value1");
            Assert.IsFalse(badResult.StatusCode == 200);
        }
    }
}