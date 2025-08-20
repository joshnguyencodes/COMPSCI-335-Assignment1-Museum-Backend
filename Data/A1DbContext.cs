using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class A1DbContext : DbContext
    {

        public A1DbContext(DbContextOptions<A1DbContext> options) : base(options){}

        public DbSet<Artefact> Artefacts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=A1Database.sqlite");
        }



    }
}
