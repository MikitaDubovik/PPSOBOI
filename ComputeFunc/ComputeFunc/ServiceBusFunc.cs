using System;
using System.Text;
using System.Threading.Tasks;
using ComputeFunc.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ComputeFunc
{
    public static class ServiceBusFunc
    {
        [FunctionName("ServiceBusFunc")]
        public static async Task Run([ServiceBusTrigger("dataqueue", Connection = "queueConnection")] string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
            try
            {
                var post = JsonConvert.DeserializeObject<Post>(myQueueItem);
                await CosmosService.AddItemToContainerAsync(post);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
            }
        }
    }
}
