using Bookstore.Communication.Requests;
using Bookstore.Communication.Responses;
using Bookstore.Models;
using Bookstore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers;
public class BooksController(IBookService bookService) : APIControllerBase
{
    private readonly IBookService _bookService = bookService;

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseNotFoundJson), StatusCodes.Status404NotFound)]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var response = _bookService.GetById(id);

        if (response is null)
        {
            return NotFound
            (
                new ResponseNotFoundJson() 
                { 
                    Message = $"Book with id '{ id }' not found." 
                }
            );
        }

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Book>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseNotFoundJson), StatusCodes.Status404NotFound)]
    public IActionResult GetAll()
    {
        var response = _bookService.GetAll();

        if (response.Count == 0)
        {
            return NotFound
            (
                new ResponseNotFoundJson()
                {
                    Message = "No books found in database."
                }
            );
        }

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Book), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseBadRequestJson), StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] RequestCreateBookJson book)
    {
        if (book.Title is null || book.Title.Length == 0)
        {
            return BadRequest
            (
                new ResponseBadRequestJson()
                { 
                    Message = "Title is required."
                }
            );
        }

        if (book.Author is null || book.Author.Length == 0)
        {
            return BadRequest
            (
                new ResponseBadRequestJson()
                {
                    Message = "Author is required."
                }
            );
        }

        var newBook = new Book
        {
            Title = book.Title,
            Author = book.Author,
            Genre = book.Genre ?? "Others",
            Price = book.Price ?? 0.00,
            Quantity = book.Quantity ?? 0U
        };

        var uri = $"{GetControllerRoute()}/{newBook.Id}";
        var response = _bookService.Create(newBook);

        return Created(uri, response);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseBadRequestJson), StatusCodes.Status400BadRequest)]
    public IActionResult Update([FromRoute] Guid id, [FromBody] RequestUpdateBookJson book)
    {
        var response = _bookService.Update(id, book);

        if (response is null)
        {
            return BadRequest
            (
                new ResponseBadRequestJson()
                {
                    Message = $"Book with id '{id}' could not be updated."
                }
            );
        }

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseBadRequestJson), StatusCodes.Status400BadRequest)]
    public IActionResult Delete([FromRoute] Guid id)
    {
        return _bookService.Delete(id) ? 
            NoContent() : 
            BadRequest(
                    new ResponseBadRequestJson() 
                    { 
                        Message = $"Book with id '{id}' could not be deleted." 
                    }
                );
    }
}
