using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using MiniBookService.Api.Data;
using MiniBookService.Api.Models;
using MiniBookService.Api.Services;

namespace MiniBookService.Tests;

public class BookServiceTests
{
    [Fact]
    public async Task CreateBookAsync_AddsBookToDatabase()
    {
        var database = CreateDatabase();

        var service = new BookService(
            database,
            NullLogger<BookService>.Instance
        );

        var book = new Book
        {
            Title = "C# Basics",
            Author = "Beginner",
            Price = 20
        };

        var createdBook = await service.CreateBookAsync(book);

        Assert.True(createdBook.Id > 0);
        Assert.Single(await database.Books.ToListAsync());
    }

    private static AppDbContext CreateDatabase()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }
}
