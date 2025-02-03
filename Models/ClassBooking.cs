using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EDP_project.Models
{
    public class ClassBooking
    {
        [Required]
        public int ClassBookingID { get; set; } = 0;
        [Required]
        public int ForeignUserID { get; set; } = 0;
        [Required, MinLength(3), MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required, RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Invalid mobile number format.")]
        public string MobileNumber { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, RegularExpression("^[a-zA-Z]$")]
        public string ClassType { get; set; } = string.Empty;
        [Required]
        public string Session {  get; set; } = string.Empty;
        public Boolean BYOC { get; set; } = false;
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdatedAt { get; set; }
    }
}
