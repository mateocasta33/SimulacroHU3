using management.Domain.Entities;
using management.Domain.Interfaces;
using management.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace management.Infrastructure.Repositories;

public class UserRepository : IRepository<User>
{
    private readonly ILogger<User> _logger;
    private readonly AppDbContext _context;

    public UserRepository(ILogger<User> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        try
        {
            return await _context.Users.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<User> GetByIdAsync(int id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<User> UpdateAsync(User entity)
    {
        try
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (DbUpdateException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<User> CreateAsync(User entity)
    {
        try
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(User entity)
    {
        try
        {
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}