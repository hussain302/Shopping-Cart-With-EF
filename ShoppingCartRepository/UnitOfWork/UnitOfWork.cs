using ShoppingCartDataAccessLayer.ShoppingCartContext;
using ShoppingCartInterfaces.IRepositories;
using ShoppingCartInterfaces.IUnitOfWork;
using ShoppingCartRepository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartRepository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public IUserRepository User { get; private set; }
        public ISettingsRepository Settings { get; private set; }
        public IOrderProductRepository OrderProduct { get;private set; }
        public IOrderRepository Order { get; private set; }

        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(context);
            Product = new ProductRepository(context);
            User = new UserRepository(context);
            Settings = new SettingsRepository(context);
            OrderProduct = new OrdersProductsRepository(context);
            Order = new OrderRepository(context);
        }

        public async Task<int> Save()
        {
            int response = await _context.SaveChangesAsync();
            return response;
        }
    }
}
