using AutoMapper;
using management.Application.DTOs;
using management.Application.Interfaces;
using management.Domain.Entities;
using management.Domain.Interfaces;

namespace management.Application.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _repository;
    private readonly IMapper _mapper;

    public UserService(IRepository<User> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    // Obtener todos los usuarios
    public async Task<IEnumerable<UserDto>> GetAllServiceAsync()
    {
        var users = await _repository.GetAllAsync();
        if (users == null || !users.Any())
            throw new KeyNotFoundException("No se encontraron usuarios");

        var usersDto = users.Select(u => _mapper.Map<UserDto>(u));
        return usersDto;
    }
    
    // Obtener usuarios por id
    public async Task<UserDto> GetByIdServiceAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        
        if (user == null)
            throw new KeyNotFoundException($"No se encontro el usuario con el id: {id}");

        var userDto = _mapper.Map<UserDto>(user);
        return userDto;
    }
}