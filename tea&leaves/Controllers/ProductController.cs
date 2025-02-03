using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using tea_leaves.Models;

namespace tea_leaves.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController (MyDbContext context, IMapper mapper, ILogger<ProductController> logger) : ControllerBase
    {
        private readonly MyDbContext _context = context;
        private static List<Products> _products = new List<Products>();
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<ProductController> _logger = logger;
        [HttpPost]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
        public IActionResult CreateProduct(AddProductRequest productRequest)
        {
            try
            {
                

                //// Validate Temperature and IceLevel
                //if (productRequest.Temperature == "Cold" && string.IsNullOrEmpty(productRequest.IceLevel))
                //{
                //    return BadRequest("Ice level is required when temperature is 'Cold'.");
                //}

                //if (productRequest.Temperature == "Hot" && !string.IsNullOrEmpty(productRequest.IceLevel))
                //{
                //    return BadRequest("Ice level must not be specified for 'Hot' drinks.");
                //}

                // Prepare product for creation
                var now = DateTime.Now;
                var newProduct = new Products
                {
                    Prod_name = productRequest.Prod_name.Trim(),
                    Price = productRequest.Price,
                    Quantity = productRequest.Quantity ?? null,
                    Toppings = string.Join(", ", productRequest.Toppings),
                    IceLevel = productRequest.IceLevel,
                    Temperature = string.Join(", ", productRequest.Temperature),
                    Description = productRequest.Description?.Trim(),
                    Stock = productRequest.Stock,
                    ImageFile = productRequest.ImageFile
                };

                // Add and save the product
                _context.Products.Add(newProduct);
                _context.SaveChanges();

                // Retrieve the newly created product, including user info if necessary
                Products? createdProduct = _context.Products
                    .FirstOrDefault(p => p.Prod_id == newProduct.Prod_id);

                // Map to DTO for response
                ProductDTO productDTO = _mapper.Map<ProductDTO>(createdProduct);
                return Ok(productDTO);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error when creating product: {Message}", dbEx.Message);
                return StatusCode(500, $"Database error: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when creating product");
                return StatusCode(500, "An error occurred while creating the product.");
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Prod_id == id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }
            return Ok(product);
        }
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                var products = _context.Products.ToList();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching products from the database.");
                return StatusCode(500, "An error occurred while fetching products.");
            }
        }

        [HttpPut("{id}")]
        
        public IActionResult UpdateProduct(int id, UpdateProductRequest productRequest)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(p => p.Prod_id == id);
                if (product == null)
                {
                    return NotFound("Product not found.");
                }

                //// Validate if the product belongs to the current user
                //int productId = GetProdId(product); // Assuming GetUserId() is implemented to fetch the current user's ID
                //if (product.Prod_id != productId)
                //{
                //    return Forbid(); // User is not authorized to edit this product
                //}

                // Update the product details
                if (productRequest.Prod_name != null)
                {
                    product.Prod_name = productRequest.Prod_name.Trim();
                }
                if (productRequest.Price != null)
                {
                    product.Price = productRequest.Price;
                }
                if (productRequest.Toppings != null)
                {
                    product.Toppings = string.Join(", ", productRequest.Toppings);
                }
                if (productRequest.Temperature != null)
                {
                    product.Temperature = string.Join(", ", productRequest.Temperature);
                }
                if (productRequest.Description != null)
                {
                    product.Description = productRequest.Description?.Trim();
                }
                if (productRequest.Stock != null)
                {
                    product.Stock = productRequest.Stock;
                }
                // Save changes to the database
                _context.SaveChanges();

                return Ok("Product updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product.");
                return StatusCode(500, "An error occurred while updating the product.");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                // Find the product by ID
                var product = _context.Products.Find(id);
                if (product == null)
                {
                    return NotFound("Product not found.");
                }

                // Validate if the product ID matches using GetProdId
                int productId = GetProdId(product); // Assuming GetProdId is implemented
                if (product.Prod_id != productId)
                {
                    return Forbid(); // User is not authorized to delete this product
                }

                // Remove the product
                _context.Products.Remove(product);
                _context.SaveChanges();

                return Ok("Product deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting product.");
                return StatusCode(500, "An error occurred while deleting the product.");
            }
        }

        private int GetProdId(Products product)
        {
            // Assuming the product object has a UserId or similar property to identify ownership
            return product.Prod_id; // Replace this with the actual logic to retrieve the product ID
            //return Convert.ToInt32(Products.Claims
            //.Where(c => c.Type == ClaimTypes.NameIdentifier)
            //.Select(c => c.Value).SingleOrDefault());
        }




    }
}
