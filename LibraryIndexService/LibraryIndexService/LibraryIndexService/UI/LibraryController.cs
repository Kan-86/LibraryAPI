using LibraryIndexService.Models;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace LibraryIndexService.UI
{
    public class LibraryController
    {
        public void GetLibrary(string URL)
        {
            GetLibraryBooksRequest(URL);
        }
        public static void GetLibraryBooksRequest(string apiUrl)
        {
            if (!string.IsNullOrEmpty(apiUrl))
            {
                WebClient client = new WebClient();
                client.Headers["Content-type"] = "application/json";
                client.Encoding = Encoding.UTF8;
                var json = client.DownloadData(apiUrl);
                if (json != null)
                {
                    DesierializeJson(json);
                }
                else
                    Console.WriteLine("Error while loading URL");
            }
        }

        private static void DesierializeJson(byte[] json)
        {
            string download = Encoding.ASCII.GetString(json);
            List<Books> foodType = (new JavaScriptSerializer()).Deserialize<List<Books>>(download);
            if (foodType.Count > 0)
            {
                foreach (Books food in foodType)
                {
                    Console.WriteLine($"Book Id: {food.Id}\n" +
                        $"Book title: {food.BookTitle}\n" +
                        $"Book author: {food.Author}\n" +
                        $"Book CurrentUser: {food.CurrentUser}\n" +
                        $"Book InRent: {food.InRent}\n" +
                        $"Book releasedDate: {food.Released}\n" +
                        $"Book rentedDate: {food.RentedDate}\n");
                }
            }
            else
            {
                Console.WriteLine("No records found.");
            }
        }
    }
}
