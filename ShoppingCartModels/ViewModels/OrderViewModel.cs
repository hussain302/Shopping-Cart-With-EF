using ShoppingCartModels.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartModels.ViewModels
{
    public class OrderViewModel
    {
        public Order Order { get; set; } = new Order();
        public IEnumerable<Order> Orders { get; set; } = Enumerable.Empty<Order>();
    }
}