using Bookstore.Communication.Requests;
using Bookstore.Models;
using Bookstore.Repositories.Interfaces;
using Bookstore.Services.Interfaces;

namespace Bookstore.Services.Implementation;

public class BookService(IBookRepository bookRepository) : IBookService
{
    private readonly IBookRepository _bookRepository = bookRepository;

    public Book? GetById(Guid bookId)
    {
        Book? book = null;

        if (bookId != Guid.Empty)
        {
            book = _bookRepository.Select(bookId);
        }

        return book;
    }

    public List<Book> GetAll()
    {
        return _bookRepository.Select();
    }

    public Book? Create(Book book)
    {
        if (book != null)
        {
            return _bookRepository.Insert(book);
        }

        return null;
    }

    public Book? Update(Guid id, RequestUpdateBookJson book)
    {
        if (GetById(id) is Book update)
        {
            update.Title = book.Title ?? update.Title;
            update.Author = book.Author ?? update.Author;
            update.Genre = book.Genre ?? update.Genre;
            update.Price = book.Price ?? update.Price;
            update.Quantity = book.Quantity ?? update.Quantity;

            return _bookRepository.Update(update);
        }

        return null;
    }

    public bool Delete(Guid id)
    {
        if (GetById(id) is not null)
        {
            return _bookRepository.Delete(id) > 0;
        }

        return false;
    }
}