using LibraryIndex.Data;
using LibraryIndex.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace LibraryIndex.UI
{
    public class IndexController
    {
        private static MessageListener mListener;
        private static int key;
        private static MessagePublisher mPublisher;
        private static dynamic value;
        // Connection String for the namespace can be obtained from the Azure portal under the 
        // 'Shared Access policies' section.
        const string serviceBusConnectionString = "Endpoint=sb://libraryapi.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=krLVo6p2VJLKy/AQyVLlAMCMukWw3uxiNWgyiDeGivs=";
        const string libraryIndexQueue = "libraryindexqueue";
        const string libraryAddQueue = "libraryaddbookqueue";
        const string userQueue = "userindexqueue";

        public void SelectUsers()
        {
            mListener = new MessageListener();
            mListener.MainAsync(userQueue, serviceBusConnectionString).GetAwaiter().GetResult();

            var q = mListener.PeekQueue();
            Console.WriteLine("Pick the user: " + "\n");
            
            if (q != null)
            {

                var pick = Console.ReadLine();
                foreach (var item in q)
                {
                    Console.WriteLine(item);

                    if (pick == item.Key.ToString())
                    {
                        Console.WriteLine("You choose: " + "\n" + item.Value);
                        value = item.Value;
                        key = item.Key;

                    }
                }
                if (value != null)
                {
                    WebClient Client = new WebClient();
                    string Json = Newtonsoft.Json.JsonConvert.SerializeObject(value);
                    dynamic jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject(Json);


                    jsonObject.BookRentedId = 2;

                    
                    string modifiedJsonString = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject);
                    Client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    Client.UploadString($"https://librarywebapplication.azurewebsites.net/users/{key}", "PUT", modifiedJsonString);
                }
            }
        }

        public void FindUserByName()
        {
            mPublisher = new MessagePublisher();
            mPublisher.MainAsync(serviceBusConnectionString, libraryIndexQueue).GetAwaiter().GetResult();
        }

        public void CreateBook()
        {
            mPublisher = new MessagePublisher();
            mPublisher.MainAsync(serviceBusConnectionString, libraryAddQueue).GetAwaiter().GetResult();
        }

        public void GetAllBooks()
        {
            mListener = new MessageListener();
            mListener.MainAsync(libraryIndexQueue, serviceBusConnectionString).GetAwaiter().GetResult();
        }
    }
}
