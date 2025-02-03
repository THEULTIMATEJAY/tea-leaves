using System.ComponentModel.DataAnnotations;

namespace tea_leaves.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        public string name { get; set; }=string.Empty;
        [EmailAddress]
        public string email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "Phone number must be exactly 8 digits.")]
        public string number { get; set; } = string.Empty;
        public string message { get; set; } = string.Empty;
        public string location { get; set; }
    }
}
