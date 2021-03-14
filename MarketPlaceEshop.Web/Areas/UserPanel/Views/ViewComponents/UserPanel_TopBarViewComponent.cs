using MarketPlaceEshop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlaceEshop.Web.Areas.UserPanel.Views.ViewComponents
{
    public class UserPanel_TopBarViewComponent : ViewComponent
    {
        private readonly IUserService _userService;
        public UserPanel_TopBarViewComponent(IUserService userService)
        {
            _userService = userService;
        }
        #region کامپوننت مربوط به تاپ هدر پنل کاربری
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // در صورتی که کاربر به سایت ورود نکرده بود!
            ViewBag.user = await _userService.GetUserByMobile(User.Identity.Name);

            // نمایش نام فرد ثبت نام کننده در پنل کاربری
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.user = await _userService.GetUserByMobile(User.Identity.Name);
            }

            return View("UserPanel_TopBar");
        }

        #endregion
    }
}
