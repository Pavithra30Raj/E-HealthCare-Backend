using System.ComponentModel.DataAnnotations;

namespace E_HealthCareApplication.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; } = string.Empty;

        [StringLength(10)]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        public double Price { get; set; } = 0;

        [Required]
        public int Quantity { get; set; } = 0;

        public string Description { get; set; } = string.Empty;

        [Required]
        public string ExpireDate { get; set; } = string.Empty;
    }
}
