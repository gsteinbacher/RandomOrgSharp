using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp;
using Should.Fluent;

namespace RandomOrgSharp.UnitTest
{
    [TestClass]
    public class JsonHelperTest
    {
        [TestMethod]
        public void JsonToString_WhenUnknownNamePassed_ExpectNullReturned()
        {
            // Arrange
            var json = JObject.Parse(@"{ test: 'Value'}");
            var token = json.GetValue("unknownname");

            // Act
            var actual = JsonHelper.JsonToString(token);

            // Arrange
            actual.Should().Be.Null();
        }

        [TestMethod]
        public void JsonToString_WhenUnknownNameAndDefaultValuePassed_ExpectDefaultValueReturned()
        {
            // Arrange
            const string defaultValue = "default";
            var expected = defaultValue;
            var json = JObject.Parse(@"{ test: 'Value'}");
            var token = json.GetValue("unknownname");

            // Act
            var actual = JsonHelper.JsonToString(token, defaultValue);

            // Arrange
            actual.Should().Equal(expected);
        }


        [TestMethod]
        public void JsonToString_WhenKnownNamePassed_ExpectKnownValueReturned()
        {
            // Arrange
            var expected = "Gene";
            var json = JObject.Parse(@"{ test: '" + expected + "'}");
            var token = json.GetValue("test");

            // Act
            var actual = JsonHelper.JsonToString(token);

            // Arrange
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void JsonToInt_WhenUnknownNamePassed_Expect0Returned()
        {
            // Arrange
            const int expected = 0;
            var json = JObject.Parse(@"{ test: 1234}");
            var token = json.GetValue("unknownname");

            // Act
            var actual = JsonHelper.JsonToInt(token);

            // Arrange
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void JsonToInt_WhenUnknownNameAndDefaultValuePassed_ExpectDefaultValueReturned()
        {
            // Arrange
            const int defaultValue = 654;
            var expected = defaultValue;
            var json = JObject.Parse(@"{ test: 1234}");
            var token = json.GetValue("unknownname");

            // Act
            var actual = JsonHelper.JsonToInt(token, defaultValue);

            // Arrange
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void JsonToInt_WhenKnownNamePassed_ExpectKnownValueReturned()
        {
            // Arrange
            var expected = 1234;
            var json = JObject.Parse(@"{ test: '" + expected + "'}");
            var token = json.GetValue("test");

            // Act
            var actual = JsonHelper.JsonToInt(token);

            // Arrange
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void JsonToInt_WhenValueNotIntTypePassed_ExpectDefaultValueReturned()
        {
            // Arrange
            var defaultValue = 1234;
            var expected = defaultValue;
            var json = JObject.Parse(@"{ test: 'NotInt'}");
            var token = json.GetValue("test");

            // Act
            var actual = JsonHelper.JsonToInt(token, defaultValue);

            // Arrange
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void JsonToDateTime_WhenUnknownNamePassed_ExpectDateTimeMinValueReturned()
        {
            // Arrange
            var expected = DateTime.MinValue;
            var json = JObject.Parse(@"{ test: '2011-10-10 13:19:12Z'}");
            var token = json.GetValue("unknownname");

            // Act
            var actual = JsonHelper.JsonToDateTime(token);

            // Arrange
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void JsonToDateTime_WhenUnknownNameAndDefaultValuePassed_ExpectDefaultValueReturned()
        {
            // Arrange
            DateTime defaultValue = new DateTime(2015, 10, 15);
            var expected = defaultValue;
            var json = JObject.Parse(@"{ test: '2011-10-10 13:19:12Z'}");
            var token = json.GetValue("unknownname");

            // Act
            var actual = JsonHelper.JsonToDateTime(token, defaultValue);

            // Arrange
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void JsonToDateTime_WhenKnownNamePassed_ExpectKnownValueReturned()
        {
            // Arrange
            var expected = new DateTime(2015, 10, 10);
            var json = JObject.Parse(@"{ test: '2015-10-10 13:19:12Z'}");
            var token = json.GetValue("test");

            // Act
            var actual = JsonHelper.JsonToDateTime(token);

            // Arrange
            actual.Year.Should().Equal(expected.Year);
            actual.Month.Should().Equal(expected.Month);
            actual.Day.Should().Equal(expected.Day);
        }

        [TestMethod]
        public void JsonToInt_WhenValueNotDateTimeTypePassed_ExpectDefaultValueReturned()
        {
            // Arrange
            var defaultValue = new DateTime(2015, 10, 10);
            var expected = defaultValue;
            var json = JObject.Parse(@"{ test: 'NotDateTime'}");
            var token = json.GetValue("test");

            // Act
            var actual = JsonHelper.JsonToDateTime(token, defaultValue);

            // Arrange
            actual.Should().Equal(expected);
        }

    }
}
