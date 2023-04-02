using ShoppingCartModels.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartModels.ViewModels
{
    public class CartViewModel
    {
        public List<Product> Products { get; set; }
        public double TotalPrice { get; set; }
    }
}
