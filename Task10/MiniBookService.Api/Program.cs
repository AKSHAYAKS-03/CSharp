using Microsoft.EntityFrameworkCore;
using MiniBookService.Api.Data;
using MiniBookService.Api.Middleware;
using MiniBookService.Api.Models;
using MiniBookService.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("BookStoreDb"));

builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

SeedSampleBooks(app);

app.Run();

static void SeedSampleBooks(WebApplication app)
{
    using var scope = app.Services.CreateScope();

    var database = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (database.Books.Any())
    {
        return;
    }

    database.Books.AddRange(
        new Book
        {
            Title = "Clean Code",
            Author = "Robert C. Martin",
            Price = 35
        },
        new Book
        {
            Title = "The Pragmatic Programmer",
            Author = "Andrew Hunt",
            Price = 40
        }
    );

    database.SaveChanges();
}
