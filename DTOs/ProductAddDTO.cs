using System.ComponentModel.DataAnnotations;

namespace DapperWebApi.DTOs
{
    public class ProductAddDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}