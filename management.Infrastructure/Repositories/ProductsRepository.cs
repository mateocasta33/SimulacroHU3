using management.Domain.Entities;
using management.Domain.Interfaces;
using management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace management.Infrastructure.Repositories;

public class ProductsRepository : IRepository<Product>
{
    private readonly AppDbContext _context;
    private readonly ILogger<Product> _logger;

    public ProductsRepository(AppDbContext context, ILogger<Product> logger)
    {
        _logger = logger;
        _context = context;
    }
    
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        try
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }
        catch (DbUpdateException e)
        { 
            _logger.LogError(e, "No se pudo obtener los productos");
            return null;
        }
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        try
        {
            var student = await _context.Products.FindAsync(id);
            return student;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<Product> UpdateAsync(Product entity)
    {
        try
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<Product> CreateAsync(Product entity)
    {
        try
        {
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Product entity)
    {
        try
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            _logger.LogInformation("El registro se elimino de forma exitosa");
            return false;
        }
    }
}