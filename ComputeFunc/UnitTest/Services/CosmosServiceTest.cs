using ComputeFunc;
using ComputeFunc.Models;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace UnitTest.Services
{
    [TestFixture]
    public class CosmosServiceTest
    {
        [SetUp]
        public void Setup()
        {
            CosmosService.SetUp();
        }

        [Test]
        public async Task SendItemAsyncTest()
        {
            var post = new Post { PostId = 99999999 };

            await CosmosService.AddItemToContainerAsync(post);
        }

        [Test]
        public void SendItemAsyncTest_InvalidOperationException_Fail()
        {
            var post = new Post();

            Assert.ThrowsAsync<InvalidOperationException>(async () => { await CosmosService.AddItemToContainerAsync(post); });
        }

        [Test]
        public void SendItemAsyncTest_Fail()
        {
            Assert.ThrowsAsync<NullReferenceException>(async () => { await CosmosService.AddItemToContainerAsync(null); });
        }

        [Test]
        public async Task DeleteItemAsync()
        {
            var post = new Post { PostId = 99999999 };

            var id = await CosmosService.AddItemToContainerAsync(post);

            await CosmosService.DeleteItemAsync(id, post.PostId.Value.ToString());
        }
    }
}