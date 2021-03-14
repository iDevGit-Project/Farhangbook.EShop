using GoogleReCaptcha.V3.Interface;
using MarketPlaceEshop.Application.Services.Interfaces;
using MarketPlaceEshop.DataAccessLayer.Entities.SiteAPI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlaceEshop.Web.Controllers
{
    public class BannersController : SiteBaseController
    {

        private readonly IContactService _contactService;
        private readonly ICaptchaValidator _captchaValidator;
        private readonly ISiteService _siteService;

        public BannersController(IContactService contactService, ICaptchaValidator captchaValidator, ISiteService siteService)
        {
            _contactService = contactService;
            _captchaValidator = captchaValidator;
            _siteService = siteService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.banners = await _siteService
                .GetSiteBannersByPlacement(new List<BannerPlacement>
                {
                    BannerPlacement.Home_Top,
                    BannerPlacement.Home_Middle,
                    BannerPlacement.Home_Button
                });

            return View();
        }
    }
}
