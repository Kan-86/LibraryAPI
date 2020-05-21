using LibraryApp.Core.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApp.Core.DomainServices
{
    public interface IRepositories<T>
    {
        IEnumerable<T> GetAll();
        T Add(T entity);
        T GetById(int id);
        T Delete(int id);
        T Update(T entity);
        T GetByName(string name);
    }
}
