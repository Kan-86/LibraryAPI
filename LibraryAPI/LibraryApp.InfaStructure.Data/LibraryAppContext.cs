using LibraryApp.Core.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.InfaStructure.Data
{
    public class LibraryAppContext : DbContext
    {
        public LibraryAppContext(DbContextOptions<LibraryAppContext> opt) : base(opt)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Users> User { get; set; }
        public DbSet<Books> Book { get; set; }
    }
}
