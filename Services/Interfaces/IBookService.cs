using Bookstore.Communication.Requests;
using Bookstore.Models;

namespace Bookstore.Services.Interfaces;

public interface IBookService
{
    public Book? GetById(Guid bookId);
    public List<Book> GetAll();
    public Book? Create(Book book);
    public Book? Update(Guid id, RequestUpdateBookJson book);
    public bool Delete(Guid id);
}
