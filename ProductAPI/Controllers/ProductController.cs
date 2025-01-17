﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.ModelsDto;
using Shared.Services;

namespace ProductAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create(CreateProductDto entity)
    {
        try
        {
            var productDto = await _productService.Create(entity);
            return CreatedAtAction(nameof(GetByID), new { id = productDto.ProductID }, productDto);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<ProductDto>>> GetAll()
    {
        try
        {
            var products = await _productService.GetAll();
            return Ok(products);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetByID(int id)
    {
        try
        {
            var product = await _productService.GetByID(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }

    [HttpGet("search")]
    public async Task<ActionResult<IReadOnlyCollection<ProductDto>>> SearchProducts([FromQuery] string? name, [FromQuery] int? categoryID)
    {
        try
        {
            if (!string.IsNullOrEmpty(name))
            {
                var products = await _productService.GetAllByName(name);
                return Ok(products);
            }
            else if (categoryID.HasValue)
            {
                var products = await _productService.GetAllByCategory(categoryID.Value);
                return Ok(products);
            }
            else
            {
                return BadRequest("Either name or categoryID must be provided.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        try
        {
            await _productService.Remove(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateProductDto entity)
    {
        try
        {
            await _productService.Update(entity);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred: {ex.Message}");
        }
    }
}
