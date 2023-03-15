using ShoppingCartModels.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartInterfaces.IRepositories
{
    public interface IUserRepository : IBaseRepository<AdminUser>
    {
        Task<AdminUser> Login(string username, string password);
        Task<AdminUser> Validate(AdminUser adminUser);
        Task<AdminUser> CreateUser(AdminUser adminUser);
    }
}
