namespace management.Application.DTOs;

public class ProductDto
{
    public int Id { get; set; }
        
    public string Name { get; set; } = string.Empty;
        
    public string Description { get; set; } = string.Empty;
        
    public decimal Price { get; set; }
        
    public int Stock { get; set; }
    
    public bool IsAvailable { get; set; } = true;
        
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
    public DateTime? UpdatedAt { get; set; }
}

public class ProductCreateDto
{
    public string Name { get; set; } = string.Empty;
        
    public string Description { get; set; } = string.Empty;
        
    public decimal Price { get; set; }
        
    public int Stock { get; set; }
    
    public bool IsAvailable { get; set; } = true;
}

public class ProductUpdateDto
{
    public string Name { get; set; } = string.Empty;
        
    public string Description { get; set; } = string.Empty;
        
    public decimal Price { get; set; }
        
    public int Stock { get; set; }
    
    public bool IsAvailable { get; set; } = true;
}