using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs
{
    public class CreateProductDTO:UpdateProductDTO
    {
        [Required]
        [MaxLength(9)]
        public string No { get; set; }
    }

       
}