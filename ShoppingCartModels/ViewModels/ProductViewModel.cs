using ShoppingCartModels.DbModels;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCartModels.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; } = new Product();
        public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();
    }
}
