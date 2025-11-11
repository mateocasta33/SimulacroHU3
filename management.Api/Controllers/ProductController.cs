using management.Application.DTOs;
using management.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace management.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductService _services;

    public ProductController(IProductService service)
    {
        _services = service;
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        try
        {
            var products = await _services.ServiceGetAllAsync();
            return Ok(products);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = "Error interno del servidor", details = e.Message });
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        try
        {
            var product = await _services.ServiceGetByIdAsync(id);
            return Ok(product);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = "Error interno del servidor", details = e.Message });
        }
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Create(ProductCreateDto productCreateDto)
    {
        try
        {
            var product = await _services.ServiceCreatedAsync(productCreateDto);
            return Ok(product);
        }
        catch (ArgumentNullException e)
        {
            return BadRequest(new { message = e.Message });
        }
        catch (ArgumentException e)
        {
            return BadRequest(new { message = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = "Error interno del servidor", dtails = e.Message });
        }
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Update(int id, [FromBody] ProductUpdateDto productUpdateDto)
    { 
        if (productUpdateDto == null)
            return BadRequest(new { message = "El cuerpo no puede estar vacio" });
        try
        {
            var update = await _services.ServiceUpdatedAsync(id, productUpdateDto);
            return Ok(update);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = "Error interno del servidor", details = e.Message });
        }
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _services.ServiceDeletedAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch (Exception e)
        {
            return StatusCode(500, new { message = "Error interno del servidor", details = e.Message });
        }
    }
}