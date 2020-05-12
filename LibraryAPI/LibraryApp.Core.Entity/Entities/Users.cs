using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApp.Core.Entity.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }
        public List<Books> BooksRented { get; set; }
    }
}
