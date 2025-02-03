using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EDP_project.Models
{
    public class CateringOrder
    {
        [Required]
        public int CateringOrderID { get; set; } = 0;
        [Required]
        public int ForeignUserID { get; set; } = 0;
        [Required, MinLength(3), MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required, RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Invalid mobile number format.")]
        public string MobileNumber { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email {  get; set; } = string.Empty;
        [Required, RegularExpression("^[a-zA-Z]$")]
        public string CateringServiceType { get; set; } = string.Empty;
        [Required, MinLength(3), MaxLength(100)]
        public string Location { get; set; } = string.Empty;
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdatedAt { get; set; }
    }
}