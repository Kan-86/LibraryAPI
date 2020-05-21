using LibraryIndex.Data;
using LibraryIndex.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryIndex.UI
{
    public class IndexController
    {
        private static MessageListener mListener;
        private static MessagePublisher mPublisher;
        // Connection String for the namespace can be obtained from the Azure portal under the 
        // 'Shared Access policies' section.
        const string serviceBusConnectionString = "Endpoint=sb://libraryapi.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=krLVo6p2VJLKy/AQyVLlAMCMukWw3uxiNWgyiDeGivs=";
        const string libraryQueue = "libraryindexqueue";
        const string userQueue = "userindexqueue";


        public void SelectUsers()
        {
            mListener = new MessageListener();
            mListener.MainAsync(userQueue, serviceBusConnectionString).GetAwaiter().GetResult();
        }

        public void FindUserByName()
        {
            mPublisher = new MessagePublisher();
            mPublisher.MainAsync(serviceBusConnectionString, libraryQueue).GetAwaiter().GetResult();
        }

        public void CreateBook()
        {
            mPublisher = new MessagePublisher();
            mPublisher.MainAsync(serviceBusConnectionString, libraryQueue).GetAwaiter().GetResult();
        }

        public void GetAllBooks()
        {
            mListener = new MessageListener();
            mListener.MainAsync(libraryQueue, serviceBusConnectionString).GetAwaiter().GetResult();
        }
    }
}
