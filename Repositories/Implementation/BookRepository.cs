using Bookstore.Infrastrucure;
using Bookstore.Models;
using Bookstore.Repositories.Interfaces;

namespace Bookstore.Repositories.Implementation;

public class BookRepository : IBookRepository
{
    public List<Book> Select()
    {
        using var context = new ApiContext();
        return [.. context.Books];
    }

    public Book Select(Guid id)
    {
        using var context = new ApiContext();
        return context.Books.FirstOrDefault(book => book.Id == id) ?? throw new Exception("Book not found");
    }

    public Book Insert(Book book)
    {
        using var context = new ApiContext();

        context.Books.Add(book);

        if (context.SaveChanges() == 0)
        {
            throw new Exception("Error inserting book.");
        }

        return book;
    }

    public Book Update(Book book)
    {
        using var context = new ApiContext();

        context.Books.Update(book);

        if (context.SaveChanges() == 0)
        {
            throw new Exception("Error updating book.");
        }

        return book;
    }

    public int Delete(Guid id)
    {
        using var context = new ApiContext();

        context.Books.Remove(Select(id));
        return context.SaveChanges();
    }
}