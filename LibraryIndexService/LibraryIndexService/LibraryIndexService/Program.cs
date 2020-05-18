using LibraryIndexService.UI;
using System;

namespace LibraryIndexService
{
    class Program
    {
        private static string URL = "https://libtestapi.azurewebsites.net/books";
        private static LibraryController libs;

        static void Main(string[] args)
        {
            libs = new LibraryController();
            libs.GetLibrary(URL);
        }
    }
}
