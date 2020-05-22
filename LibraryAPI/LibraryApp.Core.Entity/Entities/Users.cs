using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApp.Core.Entity.Entities
{
    public class Users
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }
        public ICollection<Books> BooksRented { get; set; }
    }
}
