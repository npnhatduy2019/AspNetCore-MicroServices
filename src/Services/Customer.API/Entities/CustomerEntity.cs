using System.ComponentModel.DataAnnotations;
using Contracts.Domains;
using Contracts.Domains.Interfaces;

namespace Customer.API.Entities
{
    public class CustomerEntity : EntityBase<string>
    {
        [Required]
        public string UserName { get; set; }
        
        [StringLength(50)]
        public string FName { get; set; }

        [StringLength(50)]
        public string LName { get; set; }

        [Required]
        [EmailAddress]
        public string Email{get;set;}

        [MaxLength(255)]
        public string Address { get; set; }

        [Phone]
        public string  Phone { get; set; }

        public bool IsActive{get;set;}
        
    }
    
}