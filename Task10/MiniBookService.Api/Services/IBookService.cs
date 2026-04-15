using MiniBookService.Api.Models;

namespace MiniBookService.Api.Services;

public interface IBookService
{
    Task<List<Book>> GetAllBooksAsync();

    Task<Book?> GetBookByIdAsync(int id);

    Task<Book> CreateBookAsync(Book book);

    Task<bool> UpdateBookAsync(int id, Book book);

    Task<bool> DeleteBookAsync(int id);
}
