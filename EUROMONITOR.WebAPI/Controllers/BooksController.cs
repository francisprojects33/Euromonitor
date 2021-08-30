using EUROMONITOR.Model;
using EUROMONITOR.Model.DTO;
using EUROMONITOR.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EUROMONITOR.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepositoryManager _repository;

        public BooksController(IRepositoryManager repository)
        {
            _repository = repository;
        }

        [HttpGet(Name = "GetBooks")]
        public async Task<IActionResult> GetBooks()
        {
            try 
            { 
                var books = await _repository.Book.GetAllBooksAsync(trackChanges: false);
                var booksDto = books.Select(b => new BookDto
                {
                    BookId = b.BookId,
                    Name = b.Name,
                    Text = b.Text,
                    Price = b.Price
                }).ToList();

                return Ok(booksDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "BookById")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            try
            {
                var book = await _repository.Book.GetBookAsync(id, trackChanges: false);
                if (book == null)
                {
                    return NotFound();
                }
                else
                {
                    var bookDto = new BookDto
                    {
                        BookId = book.BookId,
                        Name = book.Name,
                        Text = book.Text,
                        Price = book.Price
                    };

                    return Ok(bookDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(404, "Not Found");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookCreationDto book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest("BookCreationDto object is null");
                }
                var bookModel = new Book
                {
                    Name = book.Name,
                    Text = book.Text,
                    Price = book.Price
                };
            
                _repository.Book.CreateBook(bookModel);

                await _repository.SaveAsync();

                var bookToReturn = new { bookId = bookModel.BookId, name = bookModel.Name, text = bookModel.Text, price = bookModel.Price };

                return CreatedAtRoute("BookById", new { id = bookModel.BookId },
                bookToReturn);
            }
            catch (Exception)
            {
                return StatusCode(400, "Object is null");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            try
            {
                var book = await _repository.Book.GetBookAsync(id, trackChanges: false);
                if (book == null)
                {
                    return NotFound();
                }

                _repository.Book.DeleteBook(book);
                await _repository.SaveAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(404, "Not Found");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] BookUpdateDto book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest("EmployeeForUpdateDto object is null");
                }

                var bookToUpdate = await _repository.Book.GetBookAsync(id, trackChanges: false);

                if (bookToUpdate == null)
                {
                    return NotFound();
                }
                
                var bookUpdate = new Book
                {
                    BookId = id,
                    Name = book.Name,
                    Text = book.Text,
                    Price = book.Price
                };

                _repository.Book.UpdateBook(bookUpdate);
                await _repository.SaveAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(404, "Not Found");
            }
        }
    }
}
