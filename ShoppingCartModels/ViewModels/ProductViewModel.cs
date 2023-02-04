using Microsoft.AspNetCore.Http;
using ShoppingCartModels.DbModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.WebPages.Html;

namespace ShoppingCartModels.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; } = new Product();
        public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();
        public IEnumerable<SelectListItem> Categories { get; set; } //= new[] { new SelectListItem() };
        //public string? Image_Path_URL { get; set; }
        
        public IFormFile? PhotoFile { get; set; }

    }
}
