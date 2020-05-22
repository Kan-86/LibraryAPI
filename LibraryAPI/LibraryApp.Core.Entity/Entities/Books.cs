using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryApp.Core.Entity.Entities
{
    public class Books
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public bool InRent { get; set; }
        public DateTime Released { get; set; }
        public DateTime RentedDate { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
    }
}
