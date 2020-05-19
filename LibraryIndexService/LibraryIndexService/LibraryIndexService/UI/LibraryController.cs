
using LibraryStorage.Models;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace LibraryStorage.UI
{
    public class LibraryController
    {
        private static List<Books> bookList;
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
            bookList = new JavaScriptSerializer().Deserialize<List<Books>>(download);
            if (bookList.Count > 0)
            {
                Console.WriteLine("Fetching data.");
            }
            else
            {
                Console.WriteLine("No records found.");
            }
        }

        public List<Books> GetList()
        {
            return bookList;
        }
    }
}
