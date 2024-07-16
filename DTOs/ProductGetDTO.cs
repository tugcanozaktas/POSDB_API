using System.ComponentModel.DataAnnotations;

namespace DapperWebApi.DTOs
{
    public class ProductGetDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}