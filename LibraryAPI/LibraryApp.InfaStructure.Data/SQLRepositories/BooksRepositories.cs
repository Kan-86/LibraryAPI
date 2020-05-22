using LibraryApp.Core.DomainServices;
using LibraryApp.Core.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryApp.InfaStructure.Data.SQLRepositories
{
    public class BooksRepositories : IRepositories<Books>
    {
        readonly LibraryAppContext ctx;

        public BooksRepositories(LibraryAppContext _ctx)
        {
            ctx = _ctx;
        }

        public Books Add(Books book)
        {
            ctx.Attach(book).State = EntityState.Added;
            ctx.SaveChanges();
            return book;
        }

        public Books Delete(int id)
        {
            var mainBookDelete = ctx.Book.ToList().FirstOrDefault(b => b.Id == id);
            ctx.Book.Remove(mainBookDelete);
            ctx.SaveChanges();
            return mainBookDelete;
        }

        public IEnumerable<Books> GetAll()
        {
            var result = ctx.Book.ToList();
            return result;
        }

        public Books GetById(int id)
        {
            return ctx.Book
                .FirstOrDefault(b => b.Id == id);
        }

        public Books GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Books Update(Books book)
        {
            //var result = ctx.Book.SingleOrDefault(b => b.Id == book.Id);
            //result.RentedDate = DateTime.Now;
            //ctx.SaveChanges();
            //return book;
            return null;
        }
    }
}
