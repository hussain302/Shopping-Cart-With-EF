using ShoppingCartInterfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartInterfaces.IUnitOfWork
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        IUserRepository User { get; }
        ISettingsRepository Settings { get; }
        IOrderProductRepository OrderProduct { get; }
        IOrderRepository Order { get; }
        Task<int> Save();
    }
}