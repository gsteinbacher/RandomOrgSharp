﻿using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.RandomOrgSharp.Method;
using Obacher.RandomOrgSharp.Response;
using Should.Fluent;

namespace RandomOrgSharp.FunctionalTest
{
    [TestClass]
    public class UsageMethodTest
    {
        [TestMethod]
        public void UsageMethod_Execute_ShouldReturnValidUsageResponse()
        {
            // Arrange

            // Act
            var target = new UsageMethod();
            var results = target.GetUsage();

            // Assert
            TestResults(results);
        }


        [TestMethod]
        public async Task UsageMethodAsync_Execute_ShouldReturnValidUsageResponse()
        {
            // Arrange


            // Act
            var target = new UsageMethod();
            var results = await target.GetUsageAsync();

            // Assert
            TestResults(results);
        }


        private static void TestResults(UsageResponse results)
        {
            results.Should().Not.Be.Null();
        }
    }
}