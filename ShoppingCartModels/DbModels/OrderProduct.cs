using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartModels.DbModels
{
    public class OrderProduct
    {

        public OrderProduct()
        {

        }
        [Key]
        public int Id { get; set; }


        public int OrderId { get; set; }
        public virtual Order Order { get; set; }



        public int ProductId { get; set; }
        public virtual Product Product { get; set; }


        [Required]
        public int Quantity { get; set; }
    }
}
