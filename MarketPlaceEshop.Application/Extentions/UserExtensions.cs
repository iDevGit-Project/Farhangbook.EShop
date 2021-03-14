using MarketPlaceEshop.DataAccessLayer.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceEshop.Application.Extentions
{
    public static class UserExtensions
    {
        public static string GetUserShowName(this User user)
        {
            if (!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName))
            {
                // نمایش نام و نام خانوادگی کاربر در پنل کاربری
                return $"{user.FirstName} {user.LastName}";
            }

            return user.Mobile;
        }
    }
}
