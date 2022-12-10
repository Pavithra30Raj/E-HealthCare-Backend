using System.ComponentModel.DataAnnotations;

namespace E_HealthCareApplication.Models
{
    public class Cart
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; } = 0;

        [Required]
        public int ProductId { get; set; } = 0;

        [Required]
        public int Quantity { get; set; } = 0;

        [Required]
        public double TotalPrice { get; set; } = 0;
    }
}
