using LibraryStorage.Repo;
using LibraryStorage.UI;

namespace LibraryStorage
{
    class Program
    {
        private static string URL = "https://librarywebapplication.azurewebsites.net/books";
        private static StorageController libs;
        private static MessagePublisher mPub;

        static void Main(string[] args)
        {
            libs = new StorageController();
            mPub = new MessagePublisher();
            libs.GetLibrary(URL);
            libs.CreateBook();
            mPub.MainAsync().GetAwaiter().GetResult();
        }
    }
}
