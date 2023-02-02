using ShoppingCartModels.DbModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartModels.ViewModels
{
    public class CategoryViewModel
    {
        public Category Category { get; set; } = new Category();
        public IEnumerable<Category> Categories { get; set; } = Enumerable.Empty<Category>();
    }
}