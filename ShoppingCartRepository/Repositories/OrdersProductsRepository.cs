using Microsoft.EntityFrameworkCore;
using ShoppingCartDataAccessLayer.ShoppingCartContext;
using ShoppingCartInterfaces.IRepositories;
using ShoppingCartModels.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartRepository.Repositories
{
    public class OrdersProductsRepository : BaseRepository<OrderProduct>, IOrderProductRepository
    {
        private readonly ApplicationDbContext _context;

        public OrdersProductsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
