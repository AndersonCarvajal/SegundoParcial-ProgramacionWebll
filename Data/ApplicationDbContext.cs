<Project Sdk="Microsoft.NET.Sdk.Web">
using ProductosAPI.Models;

namespace ProductosAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Producto> Productos => Set<Producto>();
}
