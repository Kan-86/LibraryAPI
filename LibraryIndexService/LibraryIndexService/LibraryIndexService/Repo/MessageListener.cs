using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibraryStorage.Repo
{
    public class MessageListener
    {
        // Connection String for the namespace can be obtained from the Azure portal under the 
        // 'Shared Access policies' section.
        const string ServiceBusConnectionString = "Endpoint=sb://libraryapi.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=krLVo6p2VJLKy/AQyVLlAMCMukWw3uxiNWgyiDeGivs=";
        const string BasicQueueName = "libraryaddbookqueue";

        public async Task MainAsync()
        {
            await PeekMessagesAsync(ServiceBusConnectionString, BasicQueueName);
        }

        static async Task PeekMessagesAsync(string connectionString, string queueName)
        {
            var receiver = new MessageReceiver(connectionString, queueName, ReceiveMode.PeekLock);

            Console.WriteLine("Browsing messages from Queue...");
            while (true)
            {
                try
                {
                    // Browse messages from queue
                    var message = await receiver.PeekAsync();
                    // If the returned message value is null, we have reached the bottom of the log
                    if (message != null)
                    {
                        var body = message.Body;

                        dynamic book = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(body));

                        WebClient Client = new WebClient();
                        string Json = Newtonsoft.Json.JsonConvert.SerializeObject(book);
                        Client.Headers[HttpRequestHeader.ContentType] = "application/json";
                        Client.UploadString("https://libtestapi.azurewebsites.net/books", "POST", Json);
                    }
                    else
                    {
                        // We have reached the end of the log.
                        break;
                    }
                }
                catch (ServiceBusException e)
                {
                    if (!e.IsTransient)
                    {
                        Console.WriteLine(e.Message);
                        throw;
                    }
                }
            }
            await receiver.CloseAsync();
        }
    }
}
