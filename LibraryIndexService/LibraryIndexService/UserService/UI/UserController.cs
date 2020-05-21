using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UserService.Models;

namespace UserService.UI
{
    public class UserController
    {
        private static List<Users> userList;

        public void GetLibrary(string uRL)
        {
            GetLibraryBooksRequest(uRL);
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
            userList = new JavaScriptSerializer().Deserialize<List<Users>>(download);
            if (userList.Count > 0)
            {
                Console.WriteLine("Fetching data.");
            }
            else
            {
                Console.WriteLine("No records found.");
            }
        }

        public List<Users> GetList()
        {
            return userList;
        }
    }
}
