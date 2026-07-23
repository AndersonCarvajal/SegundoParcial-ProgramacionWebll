using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductosAPI.Data;
using ProductosAPI.Models;

namespace ProductosAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductosController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
    {
        var productos = await _context.Productos
            .OrderBy(p => p.Nombre)
            .ToListAsync();

        return Ok(productos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Producto>> GetProducto(int id)
    {
        var producto = await _context.Productos.FindAsync(id);

        if (producto is null)
        {
            return NotFound(new { mensaje = $"No existe un producto con el id {id}." });
        }

        return Ok(producto);
    }

    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<Producto>>> Buscar([FromQuery] string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            return Ok(new List<Producto>());
        }

        var productos = await _context.Productos
            .Where(p => p.Nombre.Contains(nombre))
            .OrderBy(p => p.Nombre)
            .ToListAsync();

        return Ok(productos);
    }

    [HttpPost]
    public async Task<ActionResult<Producto>> PostProducto([FromBody] Producto producto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutProducto(int id, [FromBody] Producto producto)
    {
        if (id != producto.Id)
        {
            return BadRequest(new { mensaje = "El id del cuerpo no coincide con el de la URL." });
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var productoExistente = await _context.Productos.FindAsync(id);

        if (productoExistente is null)
        {
            return NotFound(new { mensaje = $"No existe un producto con el id {id}." });
        }

        productoExistente.Nombre = producto.Nombre;
        productoExistente.Descripcion = producto.Descripcion;
        productoExistente.Precio = producto.Precio;
        productoExistente.Stock = producto.Stock;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProducto(int id)
    {
        var producto = await _context.Productos.FindAsync(id);

        if (producto is null)
        {
            return NotFound(new { mensaje = $"No existe un producto con el id {id}." });
        }

        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
