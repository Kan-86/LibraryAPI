using LibraryIndex.Data;
using LibraryIndex.UI;
using System;

namespace LibraryIndex
{
    class Program
    {
        private static IndexController indexController;

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
            indexController = new IndexController();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("All the books available in the Library.");
                    indexController.GetAllBooks();
                    break;
                case "2":
                    indexController.CreateBook();
                    break;
                case "3":
                    Console.WriteLine("Select a user.");
                    indexController.SelectUsers();
                    break;
                default:
                    Console.WriteLine("Not in the list");
                    break;
            }
        }
    }
}
