using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApp.Core.ApplicationServices
{
    public interface IService<T>
    {
        IEnumerable<T> GetAll();
        T Add(T entity);
        T GetById(int id);
        T Delete(int id);
        T Update(T entity);
    }
}
