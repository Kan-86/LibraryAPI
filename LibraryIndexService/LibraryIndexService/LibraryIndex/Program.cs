using LibraryIndex.Data;
using System;

namespace LibraryIndex
{
    class Program
    {
        static void Main(string[] args)
        {
            UserChoice();
        }

        private static void UserChoice()
        {
            Console.WriteLine("Library Indexer, browse the library.\n" +
                        "\t 1. Get all the books. \n" +
                        "\t 2. Something else.");
            string choice = Console.ReadLine();
            LibraryIndexPicker(choice);
        }

        private static void LibraryIndexPicker(string choice)
        {
            MessageListener mListener = new MessageListener();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("All the books available in the Library.");
                    mListener.MainAsync().GetAwaiter().GetResult();
                    break;
                case "2":
                    Console.WriteLine("Case 2");
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }
    }
}
