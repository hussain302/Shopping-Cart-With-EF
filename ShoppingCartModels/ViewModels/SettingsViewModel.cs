using ShoppingCartModels.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartModels.ViewModels
{
    public class SettingsViewModel
    {
        public SetTrends  SetTrends { get; set; } = new SetTrends();        
    }
}
