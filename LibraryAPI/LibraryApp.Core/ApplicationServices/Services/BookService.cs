using LibraryApp.Core.DomainServices;
using LibraryApp.Core.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApp.Core.ApplicationServices.Services
{
    public class BookService : IService<Books>
    {
        IRepositories<Books> repos;
        public BookService(IRepositories<Books> _repos)
        {
            repos = _repos;
        }
        public Books Add(Books entity)
        {
            if (string.IsNullOrEmpty(entity.Author))
            {
                throw new ArgumentException("Book needs an author.");
            }

            if (string.IsNullOrEmpty(entity.BookTitle))
            {
                throw new ArgumentException("Book needs a title.");
            }

            return repos.Add(entity);
        }

        public Books Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID requires to be greater than 0.");
            }
            return repos.Delete(id);
        }

        public IEnumerable<Books> GetAll()
        {
            return repos.GetAll();
        }

        public Books GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID requires to be greater than 0.");
            }
            return repos.GetById(id);
        }

        public Books GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Books Update(Books entity)
        {
            if (entity.Id <= 0)
            {
                throw new ArgumentException("ID requires to be greater than 0.");
            }
            return repos.Update(entity);
        }
    }
}
