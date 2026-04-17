using Microsoft.EntityFrameworkCore;
// DB operations
using MiniBookService.Api.Data;
// AppDbContext
using MiniBookService.Api.Models;

namespace MiniBookService.Api.Services;

public class BookService : IBookService
{
    private readonly AppDbContext _database;
    private readonly ILogger<BookService> _logger;

    public BookService(AppDbContext database, ILogger<BookService> logger)
    {
        _database = database;
        _logger = logger;
    }
    //class var = '_'
   // param/loc var = 'normal'
   
    public async Task<List<Book>> GetAllBooksAsync()
    {
        _logger.LogInformation("Getting all books");

        return await _database.Books.ToListAsync();
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        _logger.LogInformation("Getting book with id {BookId}", id);

        return await _database.Books.FindAsync(id);
    }

    public async Task<Book> CreateBookAsync(Book book)
    {
        _database.Books.Add(book);

        await _database.SaveChangesAsync();

        _logger.LogInformation("Created book with id {BookId}", book.Id);

        return book;
    }

    public async Task<bool> UpdateBookAsync(int id, Book book)
    {
        var existingBook = await _database.Books.FindAsync(id);

        if (existingBook is null)
        {
            return false;
        }

        existingBook.Title = book.Title;
        existingBook.Author = book.Author;
        existingBook.Price = book.Price;

        await _database.SaveChangesAsync();

        _logger.LogInformation("Updated book with id {BookId}", id);

        return true;
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await _database.Books.FindAsync(id);

        if (book is null)
        {
            return false;
        }

        _database.Books.Remove(book);

        await _database.SaveChangesAsync();

        _logger.LogInformation("Deleted book with id {BookId}", id);

        return true;
    }
}
