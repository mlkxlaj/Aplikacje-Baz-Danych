using System.ComponentModel.DataAnnotations;


namespace Zadanie5.DTOs
{
    public class ProductDTO
    {
        [Range(0, 30 , ErrorMessage = "Amount must be in range 0 - 10")]
        public int IdProduct { get; set; }
        public int Amount { get; set; }
        public int IdWarehouse { get; set; }
        public DateTime CreatedAt { get; set; }
        
        
    }
}
