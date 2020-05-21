using LibraryIndex.Data;
using System;

namespace LibraryIndex
{
    class Program
    {
        // Connection String for the namespace can be obtained from the Azure portal under the 
        // 'Shared Access policies' section.
        const string serviceBusConnectionString = "Endpoint=sb://libraryapi.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=krLVo6p2VJLKy/AQyVLlAMCMukWw3uxiNWgyiDeGivs=";
        const string libraryQueue = "libraryindexerqueue";
        const string userQueue = "userindexqueue";

        static void Main(string[] args)
        {
            UserChoice();
        }

        private static void UserChoice()
        {
            Console.WriteLine("Library Indexer, browse the library.\n" +
                        "\t 1. Get all the books. \n" +
                        "\t 2. Add a book to the Library. \n" +
                        "\t 3. Rent a book.");
            string choice = Console.ReadLine();
            LibraryIndexPicker(choice);
        }

        private static void LibraryIndexPicker(string choice)
        {
            MessageListener mListener = new MessageListener();
            MessagePublisher mPublisher = new MessagePublisher();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("All the books available in the Library.");
                    mListener.MainAsync(libraryQueue, serviceBusConnectionString).GetAwaiter().GetResult();
                    break;
                case "2":
                    mPublisher.MainAsync().GetAwaiter().GetResult();
                    break;
                case "3":
                    Console.WriteLine("Select a user.");
                    mListener.MainAsync(userQueue, serviceBusConnectionString).GetAwaiter().GetResult();
                    break;
                default:
                    Console.WriteLine("Not in the list");
                    break;
            }
        }
    }
}
