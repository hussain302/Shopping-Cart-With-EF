using System.ComponentModel.DataAnnotations;

namespace ShoppingCartModels.DbModels
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;
        public int? DisplayOrder { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public string? ModifiedBy { get; set; } = string.Empty;
        public string? CreatedBy { get; set; } = string.Empty;
        public virtual ICollection<Product> Products { get; set; }
    }
}