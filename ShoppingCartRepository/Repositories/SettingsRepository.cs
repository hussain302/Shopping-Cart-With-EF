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
    public class SettingsRepository : BaseRepository<SetTrends>, ISettingsRepository
    {

        private ApplicationDbContext _context;
        public SettingsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<SetTrends> GetTrendsDays(int id)
        {
            try
            {
                var find = await _context.SetTrends.FindAsync(id);
                return find;                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateTrendsDays(SetTrends days)
        {
            try
            {
                _context.SetTrends.Update(days);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
