// DapperWebApi.Controllers.ProductsController.cs

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DapperWebApi.Models; // Replace with your actual namespace for models
using DapperWebApi.Repositories; // Replace with your actual namespace for repositories
using DapperWebApi.DTOs; // Replace with your actual namespace for DTOs

namespace DapperWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // POST api/products
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductAddDTO productAddDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to entity model (assuming you have an entity model)
            var product = new Product
            {
                Name = productAddDTO.Name,
                Price = productAddDTO.Price
                // You can map other properties as needed
            };

            // Call repository method to save to database
            var result = await _productRepository.AddProductAsync(product);

            // Assuming your repository returns the added product or an ID
            // You can adjust the response based on your repository's return type
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            //get products with all columns
           var products = await _productRepository.GetProductsAsync();
            //create list for ProductGetDTO
           var productDTOs = new List<ProductGetDTO>();
            //map products according to DTO
           foreach (var product in products)
           {
                productDTOs.Add(new ProductGetDTO
                {
                    Name = product.Name,
                    Price = product.Price
                });
           }
            //return the desired DTO
           return Ok(productDTOs);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            //get products with all columns
            var product = await _productRepository.GetProductByIdAsync(id);
            
            if(product == null){
                return NotFound("Product ID does not exist");
            }

            
            //create ProductGetDTO
           var productDTO = new ProductGetDTO
                {
                    Name = product.Name,
                    Price = product.Price
                };


           return Ok(productDTO);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id,[FromBody] ProductUpdateDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            

            if (existingProduct == null)
            {
                return NotFound("Product not found");
            }


            if (productDTO.Name != null)
            {
                existingProduct.Name = productDTO.Name;
            }

            decimal? nullablePrice = productDTO.Price;

            if (nullablePrice.HasValue)
            {
                existingProduct.Price = nullablePrice.Value;
            }
            
            var result = await _productRepository.UpdateProductAsync(existingProduct);

            
            return Ok(result);
        }
    }
}
