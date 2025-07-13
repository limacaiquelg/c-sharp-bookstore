using Bookstore.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Infrastrucure;

public class ApiContext: DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "BookstoreDB");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasKey(book => book.Id);
    }

    public DbSet<Book> Books {  get; set; }
}
