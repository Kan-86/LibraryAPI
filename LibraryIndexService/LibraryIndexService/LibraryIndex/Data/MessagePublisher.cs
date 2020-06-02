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

        public async Task MainAsync(string connectionString, string queue)
        {
            await SendMessagesAsync(connectionString, queue);
        }

        static async Task SendMessagesAsync(string connectionString, string queueName)
        {
            if (queueName == "libraryaddbookqueue")
            {
                CreateBook();
            }
            
            var sender = new MessageSender(connectionString, queueName);
            // send a message for each entry in the above array
            for (int i = 0; i < bookList.Count; i++)
            {
                var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(bookList[i])))
                {
                    ContentType = "application/json",
                    Label = "CreatingAbook",
                    MessageId = i.ToString(),
                    TimeToLive = TimeSpan.FromHours(2),
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
