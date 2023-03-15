using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartModels.DbModels
{
    public class AdminUser
    {
        [Key]
        public Guid UserId { get; set; }
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
        [Required]
        public bool IsApproved { get; set; }
        [Required]
        public bool IsDeleted { get; set; }

        public DateTime? DeleteddOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? DeletedBy { get; set; }
        public string? ModifiedBy { get; set; } = string.Empty;
        public string? CreatedBy { get; set; } = string.Empty;
    }
}
