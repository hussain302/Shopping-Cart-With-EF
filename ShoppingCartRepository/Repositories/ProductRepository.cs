using Microsoft.EntityFrameworkCore;
using ShoppingCartDataAccessLayer.ShoppingCartContext;
using ShoppingCartInterfaces.IRepositories;
using ShoppingCartModels.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartRepository.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {

        private ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            try{
               return await _context.Products.Include(x => x.Category).ToListAsync();
            }
            catch{
                return Enumerable.Empty<Product>(); 
            }
        }
    }
}
