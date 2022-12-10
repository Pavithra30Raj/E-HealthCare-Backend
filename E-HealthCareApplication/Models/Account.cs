using System.ComponentModel.DataAnnotations;

namespace E_HealthCareApplication.Models
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [StringLength(60, MinimumLength = 5)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public int IsAdmin { get; set; } = 0;

    }
}
