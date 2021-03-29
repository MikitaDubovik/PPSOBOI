using Azure.Messaging.ServiceBus;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ServiceBusConnector : IServiceBusConnector
    {
        public async Task<bool> SendImage(BllPost post, int number = 1)
        {
            var connectionString = ConfigurationManager.AppSettings["serviceBusConnection"];
            var queueName = ConfigurationManager.AppSettings["queue"];

            if (post.PostId == null)
            {
                throw new ArgumentNullException("PostId is null");
            }

            if (string.IsNullOrEmpty(post.Name))
            {
                throw new ArgumentNullException("Name is null");
            }

            if (string.IsNullOrEmpty(post.Description))
            {
                throw new ArgumentNullException("Description is null");
            }

            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                // create a sender for the queue 
                ServiceBusSender sender = client.CreateSender(queueName);
                var to = number == 1 ? 10000 : number;
                for (var i = 1; i < to; i++)
                {
                    post.PostId = i * 11111;
                    post.Name += (i * 10000).ToString();
                    post.Description += (i * 10000).ToString();

                    string messageBody = JsonConvert.SerializeObject(post);

                    // create a message
                    ServiceBusMessage message = new ServiceBusMessage(messageBody);

                    // send the message
                    await sender.SendMessageAsync(message);
                }
            }

            return true;
        }
    }
}
