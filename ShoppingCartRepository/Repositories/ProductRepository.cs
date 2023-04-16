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

        public async Task<int> UpdateProducts(Product model)
        {
            if (model is null) return 0;

            try
            {
                Product? find = await (from p in _context.Products where p.Id == model.Id select p).FirstOrDefaultAsync();

                if(find != null)
                {
                    find.Name = model.Name;
                    find.Description = model.Description;
                    find.Description = model.Description;
                    find.ImageUrl = model.ImageUrl;
                    find.DisplayOrder = model.DisplayOrder;
                    find.Price = model.Price;
                    find.IsTrending = model.IsTrending;
                    find.Reviews = model.Reviews;
                    find.Colors = model.Colors;
                    find.Sizes = model.Sizes;
                    find.ProductType = model.ProductType;
                    find.CreatedOn = model.CreatedOn;
                    find.ModifiedOn = model.ModifiedOn;
                    find.ModifiedBy = model.ModifiedBy;
                    find.CreatedBy = model.CreatedBy;
                    find.CategoryId = model.CategoryId;
                    _context.Products.Update(find);
                    await _context.SaveChangesAsync();
                }
                return 1;

            }
            catch (Exception)
            {
                throw new ArgumentNullException("entity");
            }
        }
    }
}
