using LibraryIndex.Models;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryIndex.Data
{
    public class MessagePublisher
    {
        private static List<Books> bookList;
        // Connection String for the namespace can be obtained from the Azure portal under the 
        // 'Shared Access policies' section.
        const string ServiceBusConnectionString = "Endpoint=sb://libraryapi.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=krLVo6p2VJLKy/AQyVLlAMCMukWw3uxiNWgyiDeGivs=";
        const string BasicQueueName = "libraryaddbookqueue";
        public async Task MainAsync()
        {
            await SendMessagesAsync(ServiceBusConnectionString, BasicQueueName);
        }

        static async Task SendMessagesAsync(string connectionString, string queueName)
        {
            CreateBook();
            var sender = new MessageSender(connectionString, queueName);
            // send a message for each entry in the above array
            for (int i = 0; i < bookList.Count; i++)
            {
                var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(bookList[i])))
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

        private static void CreateBook()
        {
            Console.WriteLine("Please enter the title: ");
            var bookTitle = Console.ReadLine();

            Console.WriteLine("Please enter the book author: ");
            var bookAuthor = Console.ReadLine();

            bookList = new List<Books>()
            {
                new Books
                {
                    BookTitle = bookTitle,
                    Author = bookAuthor,
                    Released = DateTime.Now,
                    InRent = false,
                    CurrentUser = null,
                    RentedDate = DateTime.Now
                }
            };
        }

        public List<Books> GetList()
        {
            return bookList;
        }
    }
}
