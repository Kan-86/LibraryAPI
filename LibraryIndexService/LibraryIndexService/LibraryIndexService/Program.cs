using LibraryStorage.Repo;
using LibraryStorage.UI;

namespace LibraryStorage
{
    class Program
    {
        private static string URL = "https://libtestapi.azurewebsites.net/books";
        private static LibraryController libs;
        private static MessagePublisher mPub;

        static void Main(string[] args)
        {
            libs = new LibraryController();
            mPub = new MessagePublisher();
            libs.GetLibrary(URL);
            libs.CreateBook();
            mPub.MainAsync().GetAwaiter().GetResult();
        }
    }
}
