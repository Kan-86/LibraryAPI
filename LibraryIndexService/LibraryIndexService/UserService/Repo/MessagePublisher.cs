using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserService.Models;
using UserService.UI;

namespace UserService.Repo
{
    public class MessagePublisher
    {
        private static UserController usr;
        const string ServiceBusConnectionString = "Endpoint=sb://libraryapi.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=krLVo6p2VJLKy/AQyVLlAMCMukWw3uxiNWgyiDeGivs=";
        const string BasicQueueName = "userindexqueue";
        public async Task MainAsync()
        {
            usr = new UserController();
            await SendMessagesAsync(ServiceBusConnectionString, BasicQueueName);
        }

        static async Task SendMessagesAsync(string connectionString, string queueName)
        {
            var sender = new MessageSender(connectionString, queueName);

            Console.WriteLine("Sending messages to Queue...");

            List<Users> data = usr.GetList();

            // send a message for each entry in the above array
            for (int i = 0; i < data.Count; i++)
            {
                var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data[i])))
                {
                    ContentType = "application/json",
                    Label = "Scientist",
                    MessageId = i.ToString(),
                    TimeToLive = TimeSpan.FromMinutes(2)
                };

                await sender.SendAsync(message);
                lock (Console.Out)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Message sent: Id = {0}", message.MessageId);
                    Console.ResetColor();
                }
            }
        }
    }
}
