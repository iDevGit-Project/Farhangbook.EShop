using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlaceEshop.Web.Areas.UserPanel.Views.ViewComponents
{
    public class UserPanel_SideBarViewComponent : ViewComponent
    {
        #region کامپوننت مربوط به منوی سایدبار پنل کاربری
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("UserPanel_SideBar");
        }
        #endregion
    }
}
