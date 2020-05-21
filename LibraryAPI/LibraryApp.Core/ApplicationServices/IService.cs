using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Core.ApplicationServices
{
    public interface IService<T>
    {
        IEnumerable<T> GetAll();
        T Add(T entity);
        T GetById(int id);
        T GetByName(string name);
        T Delete(int id);
        T Update(T entity);
    }
}
