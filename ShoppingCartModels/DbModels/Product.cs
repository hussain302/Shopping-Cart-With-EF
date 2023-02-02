using System.ComponentModel.DataAnnotations;

namespace ShoppingCartModels.DbModels
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;

        public int? DisplayOrder { get; set; }
        
        [Required]
        public double Price { get; set; }
        public int? Reviews { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public string? ModifiedBy { get; set; } = string.Empty;
        public string? CreatedBy { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = new Category();
    }
}
