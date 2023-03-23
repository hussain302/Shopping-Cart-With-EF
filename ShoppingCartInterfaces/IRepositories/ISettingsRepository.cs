using ShoppingCartModels.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartInterfaces.IRepositories
{
    public interface ISettingsRepository : IBaseRepository<SetTrends>
    {
        Task<bool> UpdateTrendsDays(SetTrends days);
        Task<SetTrends> GetTrendsDays(int id);
    }
}
