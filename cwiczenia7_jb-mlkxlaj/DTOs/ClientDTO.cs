using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Zadanie7.DTOs
{
    public class ClientDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string telephone { get; set; }
        [Required]
        public string pesel { get; set; }
        [Required]
        public int idTrip { get; set; }
        [AllowNull]
        public DateTime? paymentDate { get; set; }
    }
}
