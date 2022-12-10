using System.ComponentModel.DataAnnotations;

namespace E_HealthCareApplication.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; } = 0;

        [Required]
        public string ProductNames { get; set; } = string.Empty;

        [Required]
        public double TotalPrice { get; set; } = 0;

        [Required]
        public string Status { get; set; } = string.Empty;

        [Required]
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
