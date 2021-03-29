using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace BLL.Services.Tests
{
    [TestClass()]
    public class ServiceBusConnectorTests
    {
        [TestMethod()]
        public async Task SendImageTest()
        {
            var post = new BllPost { PostId = 100000, Name = "TestName", Description = "TestDescription" };

            var serviceBusConnector = new ServiceBusConnector();
            var result = await serviceBusConnector.SendImage(post, 2);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public async Task SendImageTest_PostIdIsNull_Fail()
        {
            var post = new BllPost { Name = "TestName", Description = "TestDescription" };

            var serviceBusConnector = new ServiceBusConnector();

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => { await serviceBusConnector.SendImage(post, 2); });
        }

        [TestMethod()]
        public async Task SendImageTest_NameIsNull_Fail()
        {
            var post = new BllPost { PostId = 100000, Description = "TestDescription" };

            var serviceBusConnector = new ServiceBusConnector();

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => { await serviceBusConnector.SendImage(post, 2); });
        }

        [TestMethod()]
        public async Task SendImageTest_DescriptionIsNull_Fail()
        {
            var post = new BllPost { PostId = 100000, Name = "TestName" };

            var serviceBusConnector = new ServiceBusConnector();

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => { await serviceBusConnector.SendImage(post, 2); });
        }
    }
}