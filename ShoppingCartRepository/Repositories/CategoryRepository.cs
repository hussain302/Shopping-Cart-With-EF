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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {

        private ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
