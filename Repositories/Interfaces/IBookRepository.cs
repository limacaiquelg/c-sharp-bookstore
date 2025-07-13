using Bookstore.Models;

namespace Bookstore.Repositories.Interfaces;

public interface IBookRepository
{
    public List<Book> Select();
    public Book Select(Guid id);
    public Book Insert(Book book);
    public Book Update(Book Book);
    public int Delete(Guid id);
}