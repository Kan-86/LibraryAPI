using System;
using UserService.Repo;
using UserService.UI;

namespace UserService
{
    class Program
    {
        private static string URL = "https://librarywebapplication.azurewebsites.net/users";
        private static UserController users;
        private static MessagePublisher mPub;
        static void Main(string[] args)
        {
            users = new UserController();
            mPub = new MessagePublisher();

            users.GetLibrary(URL);

            mPub.MainAsync().GetAwaiter().GetResult();

        }
    }
}
