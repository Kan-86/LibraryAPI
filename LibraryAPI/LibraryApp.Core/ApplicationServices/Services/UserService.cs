using LibraryApp.Core.DomainServices;
using LibraryApp.Core.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApp.Core.ApplicationServices.Services
{
    public class UserService : IService<Users>
    {
        private IRepositories<Users> repos;
        public UserService(IRepositories<Users> _repos)
        {
            repos = _repos;
        }

        public Users Add(Users user)
        {
            if (string.IsNullOrEmpty(user.Surname))
            {
                throw new ArgumentException("Requires a surname.");
            }
            if (string.IsNullOrEmpty(user.Lastname))
            {
                throw new ArgumentException("Requires a lastname.");
            }
            return repos.Add(user);
        }

        public Users Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID requires to be greater than 0.");
            }
            return repos.Delete(id);
        }

        public IEnumerable<Users> GetAll()
        {
            return repos.GetAll();
        }

        public Users GetById(int id)
        {
            return repos.GetById(id);
        }

        public Users GetByName(string name)
        {
            return repos.GetByName(name);
        }

        public Users Update(Users user)
        {
            return repos.Update(user);
        }
    }
}
