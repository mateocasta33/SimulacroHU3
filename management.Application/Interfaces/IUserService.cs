using management.Application.DTOs;

namespace management.Application.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllServiceAsync();
    Task<UserDto> GetByIdServiceAsync(int id);
}