using AutoMapper;
using management.Application.DTOs;
using management.Application.Interfaces;
using management.Domain.Entities;
using management.Domain.Interfaces;

namespace management.Application.Services;

public class ProductService : IProductService
{
    private readonly IRepository<Product> _repository;
    private readonly IMapper _mapper;

    public ProductService(IRepository<Product> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> ServiceGetAllAsync()
    {
        var products = await _repository.GetAllAsync();
        if (products == null || !products.Any())
            throw new KeyNotFoundException("No se encontraron productos");

        var productsDto = products.Select(student => _mapper.Map<ProductDto>(student));
        return productsDto;
    }

    public async Task<ProductDto> ServiceGetByIdAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null)
            throw new KeyNotFoundException($"No se encontro un producto con el id: {id}");
        
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> ServiceCreatedAsync(ProductCreateDto productCreateDto)
    {
        if(productCreateDto == null)
            throw new ArgumentNullException(nameof(productCreateDto),"El objeto no puede ser nulo");

        if(string.IsNullOrWhiteSpace(productCreateDto.Name))
            throw new ArgumentException(productCreateDto.Name,"El nombre del producto no puede estar vacio");

        if(string.IsNullOrWhiteSpace(productCreateDto.Description))
            throw new ArgumentNullException(productCreateDto.Description, "La descripcion del producto no puede estar nulo");

        var product = _mapper.Map<Product>(productCreateDto);
        
        product.CreatedAt = DateTime.Now;
        product.UpdatedAt = DateTime.Now;
        
        var create = await _repository.CreateAsync(product);
        return _mapper.Map<ProductDto>(create);
    }

    public async Task<ProductDto> ServiceUpdatedAsync(int id, ProductUpdateDto productUpdateDto)
    {
        var exist = await _repository.GetByIdAsync(id);

        if (exist is null)
            throw new KeyNotFoundException($"No se encontro el producto con id: {id}");

        var product = _mapper.Map(productUpdateDto, exist);
        product.UpdatedAt = DateTime.Now;
        
        await _repository.UpdateAsync(product);
        return _mapper.Map<ProductDto>(product);
    }

    public async Task ServiceDeletedAsync(int id)
    {
        var exist = await _repository.GetByIdAsync(id);

        if (exist == null)
            throw new KeyNotFoundException($"No se encontro el producto con id: {id}");

        await _repository.DeleteAsync(exist);
    }
}