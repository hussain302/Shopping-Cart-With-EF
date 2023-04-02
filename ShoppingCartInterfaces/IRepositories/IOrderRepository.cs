using ShoppingCartModels.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartInterfaces.IRepositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
    }
}
