using LibraryApp.Core.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApp.InfaStructure.Data
{
    public class DBInitializer 
    {
        public static void SeedDb(LibraryAppContext ctx)
        {
            List<Users> users = new List<Users>
            {
                new Users {
                    Surname = "Kristofer",
                    Lastname = "Andersen",
                },
                new Users {
                    Surname = "Alexander",
                    Lastname = "Peteresen",
                }
            };

            var book1 = ctx.Book.Add(new Books()
            {
                BookTitle = "Shining",
                Author = "Steven King",
                Released = DateTime.Now,
                RentedDate = DateTime.Now

            }).Entity;

            List<Books> books = new List<Books>
            {
                new Books {
                    BookTitle = "Shining",
                    Author = "Steven King",
                    Released = DateTime.Now,
                    RentedDate = DateTime.Now
                },
                new Books {
                    BookTitle = "H.P Lovecraft Short Stories",
                    Author = "H.P Lovecraft",
                    Released = DateTime.Now,
                    RentedDate = DateTime.Now
                },
                new Books {
                    BookTitle = "The Last Wish",
                    Author = "Andrzej Sapkowski",
                    Released = DateTime.Now,
                    RentedDate = DateTime.Now
                }
            };

            ctx.Book.AddRange(books);
            ctx.User.AddRange(users);
            ctx.SaveChanges();
        }
    }
}
