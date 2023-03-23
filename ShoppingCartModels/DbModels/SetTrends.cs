using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartModels.DbModels
{
    public class SetTrends
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Days { get; set; }
    }
}
