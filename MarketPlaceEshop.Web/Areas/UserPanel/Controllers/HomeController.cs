using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlaceEshop.Web.Areas.UserPanel.Controllers
{
    public class HomeController : UserBaseController
    {
        #region متد های سازنده
        [HttpGet("")]
        public async Task<IActionResult> Dashboard()
        {
            return View();
        }

        #endregion
    }
}
