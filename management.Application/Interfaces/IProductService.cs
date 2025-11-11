using management.Application.DTOs;

namespace management.Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> ServiceGetAllAsync();
    Task<ProductDto> ServiceGetByIdAsync(int id);
    Task<ProductDto> ServiceCreatedAsync(ProductCreateDto student);
    Task<ProductDto> ServiceUpdatedAsync(int id, ProductUpdateDto student);
    Task ServiceDeletedAsync(int id);
}