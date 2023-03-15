using ShoppingCartModels.DbModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartModels.ViewModels
{
    public class UserViewModel
    {
        public AdminUser User { get; set; } = new AdminUser();
        public IEnumerable<AdminUser> Users { get; set; } = Enumerable.Empty<AdminUser>();
    }
}