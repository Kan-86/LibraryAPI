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
            return ctx.User
                .FirstOrDefault(b => b.Surname == name);
        }

        public Users Update(Users user)
        {
            var result = ctx.User.SingleOrDefault(b => b.Id == user.Id);
            result.BooksRented = user.BooksRented;
            ctx.SaveChanges();
            return user;
        }
    }
}
