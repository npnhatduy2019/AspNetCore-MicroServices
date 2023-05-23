namespace Shared.DTOs
{
    public class UpdateCustomerDTO
    {
         public string UserName { get; set; }
        
        public string FName { get; set; }

        public string LName { get; set; }
      
        public string Email{get;set;}

        public string Address { get; set; }

        public string  Phone { get; set; }

        public bool IsActive {get;set;}
    }
}