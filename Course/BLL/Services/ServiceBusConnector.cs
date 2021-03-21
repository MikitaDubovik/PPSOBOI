using Azure.Messaging.ServiceBus;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using Newtonsoft.Json;
using System.Configuration;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ServiceBusConnector : IServiceBusConnector
    {
        public async Task SendImage(BllPost post)
        {
            var connectionString = ConfigurationManager.AppSettings["serviceBusConnection"];
            var queueName = ConfigurationManager.AppSettings["queue"];

            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                // create a sender for the queue 
                ServiceBusSender sender = client.CreateSender(queueName);

                string messageBody = JsonConvert.SerializeObject(post);

                // create a message that we can send
                ServiceBusMessage message = new ServiceBusMessage(messageBody);

                // send the message
                await sender.SendMessageAsync(message);
            }
        }
    }
}
