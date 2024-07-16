using System.ComponentModel.DataAnnotations;

namespace DapperWebApi.DTOs
{
    public class ProductUpdateDTO
    {

        public string? Name { get; set; }

        public decimal? Price { get; set; }
    }
}