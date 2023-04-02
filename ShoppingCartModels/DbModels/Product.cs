using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public bool? IsTrending { get; set; }      
        [Required]
        public double Price { get; set; }
        public int? Reviews { get; set; }
        
        public string? Colors { get; set; } = string.Empty;
        public string? Sizes { get; set; } = string.Empty;
        public string? ProductType { get; set; } = string.Empty;

        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; } = string.Empty;
        public string? CreatedBy { get; set; } = string.Empty;

        //[ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }


        public virtual ICollection<OrderProduct> OrderProduct { get; set; }


    }
}
