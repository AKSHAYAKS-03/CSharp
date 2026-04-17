using Microsoft.EntityFrameworkCore;
using MiniBookService.Api.Models;

namespace MiniBookService.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
}
