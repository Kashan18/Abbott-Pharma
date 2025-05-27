using System.ComponentModel.DataAnnotations;

namespace Abbott_Pharma.Models
{
    public class Career
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression("^[0-9]{8,11}$", ErrorMessage = "Enter a valid phone number (8-11 digits)")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please select a position")]
        public string Position { get; set; }

        public string ResumePath { get; set; }
    }
}
