using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryIndex.Data
{
    public class MessageListener
    {
        private static Dictionary<int, object> dics;
        private static int key = 1;
        public async Task MainAsync(string queue, string connection)
        {
            dics = new Dictionary<int, object>();
            await PeekMessagesAsync(connection, queue);
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

                        var dynamicList = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(body));

                        if (!dics.ContainsKey(key))
                        {
                            dics.Add(key, dynamicList);
                        }
                        key+=1;
                        lock (Console.Out)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine(
                                "\t\t\t\tMessage peeked: \n\t\t\t\t\t\tMessageId = {0}, \n\t\t\t\t\t\tSequenceNumber = {1}, \n\t\t\t\t\t\tEnqueuedTimeUtc = {2}," +
                                "\n\t\t\t\t\t\tExpiresAtUtc = {5}, \n\t\t\t\t\t\tContentType = \"{3}\", \n\t\t\t\t\t\tSize = {4}, \n\t\t\t\t\t\tState = {6}, " +
                                "  \n\t\t\t\t\t\tContent: [ {7} ]",
                                message.MessageId,
                                message.SystemProperties.SequenceNumber,
                                message.SystemProperties.EnqueuedTimeUtc,
                                message.ContentType,
                                message.Size,
                                message.ExpiresAtUtc,
                                "", //message.SystemProperties.State,// TODO: Need to restore that property
                                dynamicList);
                            Console.ResetColor();
                        }
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

        public Dictionary<int, object> PeekQueue()
        {
            return dics;
        }
    }
}
