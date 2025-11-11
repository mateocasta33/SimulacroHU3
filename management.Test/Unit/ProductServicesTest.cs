using AutoMapper;
using management.Application.DTOs;
using management.Application.Interfaces;
using management.Application.Services;
using management.Domain.Entities;
using management.Domain.Interfaces;
using Moq;

namespace management.Test.Unit;

public class ProductServicesTest
{
    private readonly Mock<IRepository<Product>> _mock;
    private readonly IProductService _productService;
    private readonly Mock<IMapper> _mapper;

    public ProductServicesTest()
    {
        _mock = new Mock<IRepository<Product>>();
        _mapper = new Mock<IMapper>();
        _productService = new ProductService(_mock.Object, _mapper.Object);
    }
    
    // Se prueba que al crear los productos retorna un ProductDto
    [Fact]
    public async Task Return_ProductDto_When_Create()
    {
        // ARRANGE
        var productCreateDto = new ProductCreateDto
        {
            Name = "Producto Prueba",
            Description = "Descripción de prueba",
            Price = 20000,
            Stock = 20,
            IsAvailable = true
        };

        var productEntity = new Product
        {
            Id = 1,
            Name = "Producto Prueba",
            Description = "Descripción de prueba", 
            Price = 20000,
            Stock = 20,
            IsAvailable = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var productDto = new ProductDto
        {
            Id = 1,
            Name = "Producto Prueba",
            Description = "Descripción de prueba",
            Price = 20000,
            Stock = 20,
            IsAvailable = true
        };

        // Configurar los mocks
        _mapper.Setup(m => m.Map<Product>(productCreateDto))
            .Returns(productEntity);
                  
        _mock.Setup(r => r.CreateAsync(productEntity))
            .ReturnsAsync(productEntity);
                      
        _mapper.Setup(m => m.Map<ProductDto>(productEntity))
            .Returns(productDto);

        var result = await _productService.ServiceCreatedAsync(productCreateDto);

        Assert.NotNull(result);
        Assert.IsType<ProductDto>(result);
        Assert.Equal("Producto Prueba", result.Name);
        Assert.Equal(20000, result.Price);
        Assert.Equal(20, result.Stock);
        
        // Verificar que se llamaron los métodos
        _mapper.Verify(m => m.Map<Product>(productCreateDto), Times.Once);
        _mock.Verify(r => r.CreateAsync(productEntity), Times.Once);
        _mapper.Verify(m => m.Map<ProductDto>(productEntity), Times.Once);
    }
}