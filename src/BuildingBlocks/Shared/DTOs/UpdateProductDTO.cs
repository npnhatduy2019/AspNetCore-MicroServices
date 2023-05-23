using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs
{
    public class UpdateProductDTO
    {

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Summary { get; set; }


        public string Description { get; set; }

        
        public decimal Price { get; set; }
    }
}