using ShoppingCartModels.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartUtilities.WebUtils
{
    public static class FunctionForErrors
    {

        public static string RegisterPageErrorValidation(AdminUser user)
        {
            string err = null;
            if (string.IsNullOrWhiteSpace(user.Username)) err = "Username can't be empty";
            if (string.IsNullOrWhiteSpace(user.FullName)) err = "Full name can't be empty";
            if (string.IsNullOrWhiteSpace(user.Password)) err = "Password can't be empty";
            return err;
        }


    }
}
