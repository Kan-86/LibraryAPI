using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }
        public List<Books> BooksRented { get; set; }
    }
}
