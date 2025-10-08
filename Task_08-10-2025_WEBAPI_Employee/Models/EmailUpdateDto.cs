using System.ComponentModel.DataAnnotations;

namespace Task_08_10_2025_WEBAPI_Employee.Models
{
    public class EmailUpdateDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email address.")]
        public string Email { get; set; }
    }
}
