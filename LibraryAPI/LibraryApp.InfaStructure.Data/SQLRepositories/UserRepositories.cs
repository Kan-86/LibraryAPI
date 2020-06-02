using LibraryApp.Core.DomainServices;
using LibraryApp.Core.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryApp.InfaStructure.Data.SQLRepositories
{

    public class UserRepositories : IRepositories<Users>
    {
        readonly LibraryAppContext ctx;
        public UserRepositories(LibraryAppContext _ctx)
        {
            ctx = _ctx;
        }
        public Users Add(Users user)
        {
            ctx.Attach(user).State = EntityState.Added;
            ctx.SaveChanges();
            return user;
        }

        public Users Delete(int id)
        {
            var mainUserDelete = ctx.User.ToList().FirstOrDefault(b => b.Id == id);
            ctx.User.Remove(mainUserDelete);
            ctx.SaveChanges();
            return mainUserDelete;
        }

        public IEnumerable<Users> GetAll()
        {
            return ctx.User;
        }

        public Users GetById(int id)
        {
            return ctx.User
                .FirstOrDefault(b => b.Id == id);
        }

        public Users GetByName(string name)
        {
            var result = ctx.User
                .Include(s => s.BooksRented)
                .FirstOrDefault(b => b.Surname == name);

            return result;
        }

        public Users Update(Users user)
        {
            var usr = ctx.User
                .Include(s => s.BooksRented)
                 .FirstOrDefault(b => b.Id == user.Id);

            var book = ctx.Book
                    .FirstOrDefault(b => b.Id == user.BookRentedId);

            //book.CurrentUser = usr;
            book.UserId = user.Id;
            book.InRent = true;
            book.RentedDate = DateTime.Now;

            usr.BooksRented.Add(book);

            ctx.Entry(book).State = EntityState.Modified;


            ctx.Entry(usr).State = EntityState.Modified;
            ctx.SaveChanges();
            return usr;
        }
    }
}
