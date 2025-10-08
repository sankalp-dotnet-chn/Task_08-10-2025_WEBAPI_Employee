using System.ComponentModel.DataAnnotations;

namespace Task_08_10_2025_WEBAPI_Employee.Models
{
    public class Employee
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 30 characters.")]
        public string Name { get; set; }

        public string Department { get; set; }

        [Required]
        [Range(1000000000, 9999999999, ErrorMessage = "MobileNo must be exactly 10 digits.")]
        public long MobileNo { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email address.")]
        public string Email { get; set; }
    }
}
