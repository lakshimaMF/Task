
using System.Collections.Generic;
using Task.Models;
using Microsoft.EntityFrameworkCore;


namespace Task.Models
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Task> Task { get; set; }



    }
}

